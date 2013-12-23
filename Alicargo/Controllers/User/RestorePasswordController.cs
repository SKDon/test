using System.Web.Mvc;

namespace Alicargo.Controllers.User
{
	public partial class RestorePasswordController : Controller
	{
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
		public virtual RedirectToRouteResult Index(string email)
		{
			return RedirectToAction(MVC.RestorePassword.Finish());
		}

	}
}
