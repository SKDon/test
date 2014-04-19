using System.Web.Mvc;
using Alicargo.Areas.Admin.Models;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.MvcHelpers.Filters;

namespace Alicargo.Areas.Admin.Controllers
{
	[Access(RoleType.Admin)]
	public partial class BillSettingsController : Controller
	{
		[HttpGet]
		public virtual ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public virtual ActionResult Index(BillSettingsModel model)
		{
			return RedirectToAction(MVC.Admin.BillSettings.Index());
		}
	}
}