using System.Web.Mvc;
using Alicargo.Contracts.Repositories.User;
using Resources;

namespace Alicargo.Controllers.User
{
	public partial class RestorePasswordController : Controller
	{
		private readonly IUserRepository _users;

		public RestorePasswordController(IUserRepository users)
		{
			_users = users;
		}

		[HttpGet]
		public virtual ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public virtual ViewResult Finish()
		{
			return View();
		}

		[HttpPost]
		public virtual ActionResult Index(string email)
		{
			var id = _users.GetUserIdByEmail(email);

			if (!id.HasValue)
			{
				ModelState.AddModelError("email", Validation.UserNotFound);
				return View();
			}

			return RedirectToAction(MVC.RestorePassword.Finish());
		}

	}
}
