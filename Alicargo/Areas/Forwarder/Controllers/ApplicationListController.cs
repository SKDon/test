using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.Services.Application;

namespace Alicargo.Areas.Forwarder.Controllers
{
	[Access(RoleType.Forwarder)]
	public partial class ApplicationListController : Controller
	{
		private readonly IForwarderRepository _forwarders;
		private readonly IIdentityService _identity;
		private readonly IApplicationListPresenter _presenter;

		public ApplicationListController(
			IApplicationListPresenter presenter,
			IIdentityService identity,
			IForwarderRepository forwarders)
		{
			_presenter = presenter;
			_identity = identity;
			_forwarders = forwarders;
		}

		public virtual ViewResult Index()
		{
			return View();
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, int skip, Dictionary<string, string>[] group)
		{
			var orders = OrderHelper.Get(group);

			Debug.Assert(_identity.Id != null);

			var forwarderId = _forwarders.GetByUserId(_identity.Id.Value);

			Debug.Assert(forwarderId != null);

			var data = _presenter.List(_identity.Language, take, skip, orders, null, null, forwarderId.Value);

			return Json(data);
		}
	}
}