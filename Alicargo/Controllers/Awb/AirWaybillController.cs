using System.Linq;
using System.Net;
using System.Web.Mvc;
using Alicargo.Core.Contracts.AirWaybill;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Microsoft.Ajax.Utilities;

namespace Alicargo.Controllers.Awb
{
	public partial class AirWaybillController : Controller
	{
		private readonly IApplicationAwbManager _applicationAwbManager;
		private readonly IAwbManager _awbManager;
		private readonly IAwbPresenter _awbPresenter;
		private readonly IAwbStateManager _awbStateManager;
		private readonly IAwbRepository _awbs;
		private readonly IBrokerRepository _brokers;
		private readonly IStateConfig _config;
		private readonly IIdentityService _identity;

		public AirWaybillController(
			IAwbPresenter awbPresenter,
			IApplicationAwbManager applicationAwbManager,
			IAwbManager awbManager,
			IStateConfig config,
			IAwbRepository awbs,
			IAwbStateManager awbStateManager,
			IBrokerRepository brokers,
			IIdentityService identity)
		{
			_awbPresenter = awbPresenter;
			_brokers = brokers;
			_applicationAwbManager = applicationAwbManager;
			_awbManager = awbManager;
			_config = config;
			_awbs = awbs;
			_awbStateManager = awbStateManager;
			_brokers = brokers;
			_identity = identity;
		}

	

		#region List

		[Access(RoleType.Admin, RoleType.Manager, RoleType.Broker, RoleType.Sender)]
		[HttpGet]
		public virtual ViewResult Index()
		{
			return View();
		}

		[Access(RoleType.Admin, RoleType.Manager, RoleType.Broker, RoleType.Sender)]
		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, int skip)
		{
			long? brokerId = null;
			if(_identity.IsInRole(RoleType.Broker) && _identity.Id.HasValue)
			{
				var broker = _brokers.GetByUserId(_identity.Id.Value);
				brokerId = broker.Id;
			}

			var list = _awbPresenter.List(take, skip, brokerId, _identity.Language);

			return Json(list);
		}

		#endregion

		#region Actions

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Manager, RoleType.Broker)]
		public virtual HttpStatusCodeResult CargoIsCustomsCleared(long id)
		{
			var data = _awbs.Get(id).First();
			if(data.GTD.IsNullOrWhiteSpace())
			{
				throw new InvalidLogicException("GTD must be definded to set the CargoIsCustomsCleared state");
			}

			_awbStateManager.SetState(id, _config.CargoIsCustomsClearedStateId);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[ChildActionOnly]
		public virtual PartialViewResult CargoIsCustomsClearedButton(long id)
		{
			var data = _awbs.Get(id).First();

			var model = new CargoIsCustomsClearedButtonModel
			{
				Id = id,
				CanSetCargoIsCustomsCleared =
					data.StateId == _config.CargoAtCustomsStateId && !data.GTD.IsNullOrWhiteSpace()
			};

			return PartialView("CargoIsCustomsClearedButton", model);
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Manager, RoleType.Sender)]
		public virtual HttpStatusCodeResult Delete(long id)
		{
			_awbManager.Delete(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Manager, RoleType.Sender)]
		public virtual ActionResult SetAirWaybill(long applicationId, long? airWaybillId)
		{
			_applicationAwbManager.SetAwb(applicationId, airWaybillId);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		#endregion
	}
}