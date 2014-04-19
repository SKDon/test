using System.Web.Mvc;

namespace Alicargo.Areas.Admin.Controllers
{
	public partial class BillSettingsController : Controller
	{
		[HttpGet]
		public virtual ActionResult Index()
		{
			ViewBag.NextBillNumber = 1;

			return View();
		}
	}
}