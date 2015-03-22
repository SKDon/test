using System.Web.Mvc;
using System.Web.Routing;

namespace Alicargo.Mvc
{
	internal sealed class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"AirWaybill_Create",
				"AirWaybill/Create/{applicationId}",
				new { controller = "AirWaybill", action = "Create" },
				new[] { "Alicargo.Controllers.Awb" }
				);

			routes.MapRoute(
				"Application_Create",
				"Application/Create/{clientId}",
				new { controller = "Application", action = "Create", clientId = UrlParameter.Optional },
				new[] { "Alicargo.Controllers.Application" }
				);

			routes.MapRoute(
				"User",
				"User/{action}/{roleType}/{id}",
				new { controller = "User", action = "Index", id = UrlParameter.Optional },
				new[] { "Alicargo.Controllers" }
				);

			routes.MapRoute(
				"Balance",
				"Balance/{action}/{clientId}",
				new { controller = "Balance" },
				new[] { "Alicargo.Controllers.User" }
				);

			routes.MapRoute(
				"RestorePassword",
				"RestorePassword/NewPassword/{id}/{key}",
				new { controller = "RestorePassword", action = "NewPassword" },
				new[] { "Alicargo.Controllers.User" }
				);

			routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				new[] { "Alicargo.Controllers" }
				);
		}
	}
}