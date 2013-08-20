using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Resources;

namespace Alicargo.Controllers
{
	public partial class AuthenticationController : Controller
    {
		private readonly IAuthenticationService _authentication;
		private readonly IIdentityService _identity;
		private readonly IAuthenticationRepository _authenticationRepository;

		public AuthenticationController(
			IIdentityService identity, 
			IAuthenticationRepository authenticationRepository,
			IAuthenticationService authentication)
	    {
		    _identity = identity;
		    _authenticationRepository = authenticationRepository;
		    _authentication = authentication;
	    }

	    #region Authentication

		[HttpGet]
		public virtual ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public virtual ActionResult Login(SignIdModel user)
		{
			if (!ModelState.IsValid) return View(user);

			if (!_authentication.Authenticate(user))
			{
				ModelState.AddModelError("Login", Validation.WrongLoginOrPassword);
				return View(user);
			}

			if (_identity.IsInRole(RoleType.Admin))
				return RedirectToAction(MVC.Home.Index());

			return RedirectToAction(MVC.Home.Index());
		}

		#endregion

		[ChildActionOnly]
		public virtual PartialViewResult Client(long? clientId)
		{
			ViewData.TemplateInfo.HtmlFieldPrefix = "Authentication";

			if (!clientId.HasValue) return PartialView();

			var data = _authenticationRepository.GetByClientId(clientId.Value);

			var model = new AuthenticationModel(data.Login);

			return PartialView(model);
		}
    }
}
