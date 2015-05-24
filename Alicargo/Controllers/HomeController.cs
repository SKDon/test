using System.Web.Mvc;
using System.Web.SessionState;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.Controllers
{
	[SessionState(SessionStateBehavior.Disabled)]
	public partial class HomeController : Controller
	{
		private readonly IIdentityService _identityService;

		public HomeController(IIdentityService identityService)
		{
			_identityService = identityService;
		}

		public virtual ActionResult Index()
		{
			if(_identityService.IsAuthenticated)
			{
				if(_identityService.IsInRole(RoleType.Broker))
				{
					return RedirectToAction(MVC.AirWaybill.Index());
				}

				if(_identityService.IsInRole(RoleType.Forwarder))
				{
					return RedirectToAction(MVC.Forwarder.Applications.Index());
				}

				return RedirectToAction(MVC.ApplicationList.Index());
			}

			return Redirect("/index.html");
		}

		public virtual ActionResult Culture(string id, string returnUrl)
		{
			_identityService.SetLanguage(id);

			return Redirect(returnUrl);
		}
	}
}