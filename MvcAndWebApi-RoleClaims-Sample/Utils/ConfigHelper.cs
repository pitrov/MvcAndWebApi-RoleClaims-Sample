using System;
using System.Globalization;
using Microsoft.Azure;

namespace MvcAndWebApi_RoleClaims_Sample.Utils
{
	public class ConfigHelper
	{
		// The AAD Instance is the instance of Azure, for example public Azure or Azure China.
		// The Client ID is used by the application to uniquely identify itself to Azure AD.
		// The App Key is a credential used to authenticate the application to Azure AD.  Azure AD supports password and certificate credentials.
		// The GraphResourceId the resource ID of the AAD Graph API.  We'll need this to request a token to call the Graph API.
		// The GraphApiVersion specifies which version of the AAD Graph API to call.
		// The Post Logout Redirect Uri is the URL where the user will be redirected after they sign out.
		// The Authority is the sign-in URL of the tenant.

		private static readonly string aadInstance = CloudConfigurationManager.GetSetting("ida:AADInstance");
		private static readonly string clientId = CloudConfigurationManager.GetSetting("ida:ClientId");
		private static readonly string appKey = CloudConfigurationManager.GetSetting("ida:AppKey");
		private static readonly string graphResourceId = CloudConfigurationManager.GetSetting("ida:GraphUrl");
		private static readonly string appTenant = CloudConfigurationManager.GetSetting("ida:Tenant");
		private static readonly string graphApiVersion = CloudConfigurationManager.GetSetting("ida:GraphApiVersion");
		private static readonly string postLogoutRedirectUri = CloudConfigurationManager.GetSetting("ida:PostLogoutRedirectUri");
		private static readonly string commonAuthority = String.Format(CultureInfo.InvariantCulture, aadInstance, "common/");

		public static string ClientId { get { return clientId; } }
		internal static string AppKey { get { return appKey; } }
		internal static string GraphResourceId { get { return graphResourceId; } }
		internal static string GraphApiVersion { get { return graphApiVersion; } }
		internal static string AadInstance { get { return aadInstance; } }
		internal static string PostLogoutRedirectUri { get { return postLogoutRedirectUri; } }
		internal static string CommonAuthority { get { return commonAuthority; } }
		internal static string Tenant { get { return appTenant; } }
	}
}