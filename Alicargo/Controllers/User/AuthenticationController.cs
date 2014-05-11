using System.Web.Mvc;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;
using Resources;

namespace Alicargo.Controllers.User
{
	public partial class AuthenticationController : Controller
	{
		private readonly IAuthenticationService _authentication;
		private readonly IClientRepository _clients;

		public AuthenticationController(
			IClientRepository clients,
			IAuthenticationService authentication)
		{
			_clients = clients;
			_authentication = authentication;
		}

		[ChildActionOnly]
		public virtual PartialViewResult Client(long? clientId)
		{
			ViewData.TemplateInfo.HtmlFieldPrefix = "Authentication";

			if(!clientId.HasValue) return PartialView();

			var data = _clients.Get(clientId.Value);

			var model = new AuthenticationModel(data.Login);

			return PartialView(model);
		}

		[HttpGet]
		public virtual ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public virtual ActionResult Login(SignIdModel user)
		{
			if(!ModelState.IsValid) return View(user);

			if(!_authentication.Authenticate(user))
			{
				ModelState.AddModelError("Login", Validation.WrongLoginOrPassword);
				return View(user);
			}

			return RedirectToAction(MVC.Home.Index());
		}

		[HttpGet]
		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ActionResult LoginAsClient(int id)
		{
			var userId = _clients.Get(id).UserId;
			_authentication.AuthenticateForce(userId, false);

			return RedirectToAction(MVC.Home.Index());
		}

		[HttpGet]
		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ActionResult LoginAsUser(int id)
		{
			_authentication.AuthenticateForce(id, false);

			return RedirectToAction(MVC.Home.Index());
		}

		[HttpGet]
		public virtual RedirectToRouteResult SignOut()
		{
			_authentication.SignOut();

			return RedirectToAction(MVC.Home.Index());
		}
	}
}