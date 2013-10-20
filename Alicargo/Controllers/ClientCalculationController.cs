using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;

namespace Alicargo.Controllers
{
	public partial class ClientCalculationController : Controller
    {
		private readonly IClientCalculationPresenter _presenter;

		public ClientCalculationController(IClientCalculationPresenter presenter)
	    {
		    _presenter = presenter;
	    }

	    [Access(RoleType.Admin, RoleType.Client), HttpGet]
		public virtual ActionResult Index()
		{
			return View();
		}

		[Access(RoleType.Admin, RoleType.Client), HttpPost, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, long skip)
		{
			var data = _presenter.List(take, skip);

			return Json(data);
		}

    }
}
