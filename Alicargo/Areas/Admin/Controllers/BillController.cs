using System.Web.Mvc;

namespace Alicargo.Areas.Admin.Controllers
{
	public partial class BillController : Controller
    {
		public virtual ActionResult Preview(long applicationId)
        {
            return View();
        }
	}
}