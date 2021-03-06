﻿using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using MvcAndWebApi_RoleClaims_Sample.Utils;
using Owin;

namespace MvcAndWebApi_RoleClaims_Sample
{
	public partial class Startup
	{
		/// <summary>
		/// Configures OpenIDConnect Authentication & Adds Custom Application Authorization Logic on User Login.
		/// </summary>
		/// <param name="app">The application represented by a <see cref="IAppBuilder"/> object.</param>
		public void ConfigureAuth(IAppBuilder app)
		{
			app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

			app.UseCookieAuthentication(new CookieAuthenticationOptions());


			//Configure OpenIDConnect, register callbacks for OpenIDConnect Notifications
			app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
			{
				ClientId = ConfigHelper.ClientId,
				Authority = String.Format(CultureInfo.InvariantCulture, ConfigHelper.AadInstance, ConfigHelper.Tenant), // For Single-Tenant
				//Authority = ConfigHelper.CommonAuthority, // For Multi-Tenant
				PostLogoutRedirectUri = ConfigHelper.PostLogoutRedirectUri,

				// Here, we've disabled issuer validation for the multi-tenant sample.  This enables users
				// from ANY tenant to sign into the application (solely for the purposes of allowing the sample
				// to be run out-of-the-box.  For a real multi-tenant app, reference the issuer validation in 
				// WebApp-MultiTenant-OpenIDConnect-DotNet.  If you're running this sample as a single-tenant
				// app, you can delete the ValidateIssuer property below.
				TokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters
				{
					//ValidateIssuer = false, // For Multi-Tenant Only
					RoleClaimType = "roles",
				},

				Notifications = new OpenIdConnectAuthenticationNotifications
				{
					RedirectToIdentityProvider = context =>
					{
						if (IsAjaxRequest(context.Request) || IsJsonRequest(context.Request))
							context.HandleResponse();
						return Task.FromResult(0);
					},

					AuthorizationCodeReceived = context =>
					{
						// Get Access Token for User's Directory
						// var userObjectId = context.AuthenticationTicket.Identity.FindFirst(Globals.ObjectIdClaimType).Value;
						var tenantId = context.AuthenticationTicket.Identity.FindFirst(Globals.TenantIdClaimType).Value;
						var credential = new ClientCredential(ConfigHelper.ClientId, ConfigHelper.AppKey);

						//							var authContext = new AuthenticationContext(
						//								String.Format(CultureInfo.InvariantCulture, ConfigHelper.AadInstance, tenantId),
						//								new TokenDbCache(userObjectId));

						var authContext = new AuthenticationContext(
							String.Format(CultureInfo.InvariantCulture, ConfigHelper.AadInstance, tenantId));

						AuthenticationResult result = authContext.AcquireTokenByAuthorizationCode(
							context.Code, new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path)),
							credential, ConfigHelper.GraphResourceId);

						return Task.FromResult(0);
					},

					AuthenticationFailed = context =>
					{
						context.HandleResponse();
						context.Response.Redirect("/Error/ShowError?signIn=true&errorMessage=" + context.Exception.Message);
						return Task.FromResult(0);
					}
				}
			});
		}

		private static bool IsAjaxRequest(IOwinRequest request)
		{
			//doesn't detect if it is an ajax request.
			IReadableStringCollection query = request.Query;
			if ((query != null) && (query["X-Requested-With"] == "XMLHttpRequest"))
			{
				return true;
			}
			IHeaderDictionary headers = request.Headers;
			return ((headers != null) && (headers["X-Requested-With"] == "XMLHttpRequest"));
		}

		private static bool IsJsonRequest(IOwinRequest request)
		{
			IHeaderDictionary headers = request.Headers;
			return ((headers != null) && (headers["Content-Type"] == "application/json"));
		}
	}
}