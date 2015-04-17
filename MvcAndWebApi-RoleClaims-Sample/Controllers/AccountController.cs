using System;
using System.Globalization;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using MvcAndWebApi_RoleClaims_Sample.Utils;

namespace MvcAndWebApi_RoleClaims_Sample.Controllers
{
	public class AccountController : Controller
	{
		/// <summary>
		/// Signs the user out and clears the cache of access tokens.
		/// </summary>
		public void SignOut()
		{
			// Remove all cache entries for this user and send an OpenID Connect sign-out request.
			if (Request.IsAuthenticated)
			{
				Claim userObjectIdClaim = ClaimsPrincipal.Current.FindFirst(Globals.ObjectIdClaimType);
				Claim tenantIdClaim = ClaimsPrincipal.Current.FindFirst(Globals.TenantIdClaimType);
				if (userObjectIdClaim != null && tenantIdClaim != null)
				{
//                    var authContext = new AuthenticationContext(
//                        String.Format(CultureInfo.InvariantCulture, ConfigHelper.AadInstance, tenantIdClaim.Value),
//                        new TokenDbCache(userObjectIdClaim.Value));

					var authContext = new AuthenticationContext(
						String.Format(CultureInfo.InvariantCulture, ConfigHelper.AadInstance, tenantIdClaim.Value));

					authContext.TokenCache.Clear();
				}

				HttpContext.GetOwinContext().Authentication.SignOut(
					OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
			}
		}
	}
}