using System.Collections.Generic;
using System.Web.Mvc;
using Alicargo.Core.Enums;
using Alicargo.Core.Helpers;
using Alicargo.Helpers;
using Alicargo.Services.Abstract;

namespace Alicargo.Controllers
{
	public partial class ApplicationListController : Controller
    {
		private readonly IApplicationPresenter _applicationPresenter;

	    public ApplicationListController(IApplicationPresenter applicationPresenter)
	    {
		    _applicationPresenter = applicationPresenter;
	    }

	    [HttpPost]
		[Access(RoleType.Admin, RoleType.Client, RoleType.Forwarder, RoleType.Sender)]
		public virtual JsonResult List(int take, int skip, int page, int pageSize, Dictionary<string, string>[] group)
		{
			// todo: use model binder for Order
			var orders = Order.Get(group);

			var data = _applicationPresenter.List(take, skip, orders);

			return Json(data);
		}

		[Access(RoleType.Admin, RoleType.Client, RoleType.Forwarder, RoleType.Sender)]
		public virtual ViewResult Index()
		{
			var model = _applicationPresenter.GetApplicationIndexModel();

			return View(model);
		}
    }
}
