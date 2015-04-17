using System.Web.Mvc;

namespace MvcAndWebApi_RoleClaims_Sample.Controllers
{
	[AuthorizeMvc(Roles = "User")]
    public class UserController : Controller
    {
		public ActionResult Index()
		{
			return View();
		}
    }
}
