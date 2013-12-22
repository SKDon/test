using System.Web.Mvc;
using System.Web.SessionState;
using Alicargo.Services.Abstract;

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
            return View();
        }

		public virtual ActionResult Culture(string id, string returnUrl)
		{
			_identityService.SetLanguage(id);

			return Redirect(returnUrl);
		}
    }
}
