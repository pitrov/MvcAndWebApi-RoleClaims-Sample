
using System.Web.Mvc;

namespace MvcAndWebApi_RoleClaims_Sample.Controllers
{
	[AuthorizeMvc(Roles = "Admin")]
    public class AdminController : Controller
    {
		public ActionResult Index()
		{
			return View();
		}
    }
}
