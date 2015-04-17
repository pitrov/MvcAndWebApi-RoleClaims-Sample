using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace MvcAndWebApi_RoleClaims_Sample
{
	public class AuthorizeApiAttribute : AuthorizeAttribute
	{
		public bool IsAuthenticationRequired { get; set; }

		public AuthorizeApiAttribute()
		{
			IsAuthenticationRequired = true;
		}

		protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
		{
			IPrincipal user = actionContext.ControllerContext.RequestContext.Principal;
			if (user == null || !user.Identity.IsAuthenticated)
			{
				if (IsAuthenticationRequired)
				{
					actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
						"The requested resource requires user authentication.");
				}
			}
			else
			{
				actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden,
					"Forbidden access to this resource.");
			}
		}
	}
}