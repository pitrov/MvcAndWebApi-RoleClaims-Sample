using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;

namespace MvcAndWebApi_RoleClaims_Sample.Controllers
{
    public class HomeController : Controller
    {
	    public ActionResult Index()
	    {
			var appRoles = new List<String>();

		    if (Request.IsAuthenticated)
		    {
			    var claimsId = ClaimsPrincipal.Current.Identity as ClaimsIdentity;
			    foreach (Claim claim in ClaimsPrincipal.Current.FindAll(claimsId.RoleClaimType))
				    appRoles.Add(claim.Value);
		    }

		    ViewData["appRoles"] = string.Join(",", appRoles.ToArray());
		    return View();
	    }
    }
}
