namespace MvcAndWebApi_RoleClaims_Sample.Utils
{
	public static class Globals
	{
		private const string objectIdClaimType = "http://schemas.microsoft.com/identity/claims/objectidentifier";
		private const string tenantIdClaimType = "http://schemas.microsoft.com/identity/claims/tenantid";

		internal static string ObjectIdClaimType { get { return objectIdClaimType; } }
		internal static string TenantIdClaimType { get { return tenantIdClaimType; } }
	}
}