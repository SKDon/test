using System.Web.Mvc;

namespace Alicargo.Controllers
{
	public partial class ScriptController : Controller
    {
        //
        // GET: /Script/

		public virtual ActionResult Index()
        {
            return View();
        }

    }
}
