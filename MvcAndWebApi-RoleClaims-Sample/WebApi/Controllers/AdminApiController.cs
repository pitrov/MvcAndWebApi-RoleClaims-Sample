using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcAndWebApi_RoleClaims_Sample.WebApi.Models;

namespace MvcAndWebApi_RoleClaims_Sample.WebApi.Controllers
{
	[RoutePrefix("api/v1")]
	[AuthorizeApi(Roles = "Admin")]
    public class AdminApiController : ApiController
    {
		[Route("users")]
		[HttpGet]
		public HttpResponseMessage GetUsers()
		{
			return Request.CreateResponse(HttpStatusCode.OK, new List<UserViewModel>()
			{
				new UserViewModel{ Name = "User1" },
				new UserViewModel{ Name = "User2" }
			});
		}
    }
}
