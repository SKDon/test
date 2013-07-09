using System.Web.Mvc;
using Alicargo.Core.Enums;
using Alicargo.Helpers;
using Alicargo.Services;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Resources;

namespace Alicargo.Controllers
{
	public partial class UserController : Controller
	{
		private readonly IAuthenticationService _authentication;
		private readonly IIdentityService _identity;
		private readonly IUserService _userService;

		public UserController(IAuthenticationService authentication, IIdentityService identity, IUserService userService)
		{
			_authentication = authentication;
			_identity = identity;
			_userService = userService;
		}

		#region Authentication

		[HttpGet]
		public virtual ActionResult Login()
		{
			return View();
		}

		// todo: test
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

		#region List

		[Access(RoleType.Admin), HttpGet]
		public virtual ViewResult Index(RoleType roleType)
		{
			return View(roleType);
		}

		[Access(RoleType.Admin), HttpPost]
		public virtual JsonResult List(RoleType roleType)
		{
			var list = _userService.List(roleType);

			return Json(list);
		}

		#endregion

		#region Edit/Create

		[Access(RoleType.Admin), HttpGet]
		public virtual ViewResult Edit(RoleType roleType, long id)
		{
			var model = _userService.Get(roleType, id);

			return View(model);
		}

		[Access(RoleType.Admin), HttpGet]
		public virtual ViewResult Create(RoleType roleType)
		{
			return View();
		}

		[Access(RoleType.Admin), HttpPost]
		public virtual ActionResult Edit(UserModel model)
		{
			if (!ModelState.IsValid) return View();

			_userService.Update(model);

			return RedirectToAction(MVC.User.Index(model.RoleType));
		}

		[Access(RoleType.Admin), HttpPost]
		public virtual ActionResult Create(UserModel model)
		{
			if (string.IsNullOrWhiteSpace(model.Authentication.NewPassword))
				ModelState.AddModelError("NewPassword", Validation.EmplyPassword);

			if (!ModelState.IsValid) return View();

			_userService.Add(model);

			return RedirectToAction(MVC.User.Index(model.RoleType));
		}

		#endregion
	}
}
