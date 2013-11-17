using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.MvcHelpers.Filters;

namespace Alicargo.Controllers
{
	public partial class StateController : Controller
    {
		[Access(RoleType.Admin)]
		public virtual ActionResult Index()
        {
            return View();
        }

		[HttpPost, Access(RoleType.Admin),
		 OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, int skip)
		{
			//var collection = _clientPresenter.GetList(take, skip);

			//return Json(collection);
			return null;
		}
    }
}
