using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MvcAndWebApi_RoleClaims_Sample.Startup))]

namespace MvcAndWebApi_RoleClaims_Sample
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}
