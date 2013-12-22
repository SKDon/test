using System.Linq;
using System.Net;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Services.Abstract;
using Alicargo.MvcHelpers.Extensions;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;
using Microsoft.Ajax.Utilities;
using Resources;

namespace Alicargo.Controllers.Awb
{
	public partial class AirWaybillController : Controller
	{
		private readonly IApplicationAwbManager _applicationAwbManager;
		private readonly IAwbManager _awbManager;
		private readonly IAwbPresenter _awbPresenter;
		private readonly IAwbRepository _awbs;
		private readonly IAwbStateManager _awbStateManager;
		private readonly IAwbUpdateManager _awbUpdateManager;
		private readonly IBrokerRepository _brokers;
		private readonly IIdentityService _identity;
		private readonly IStateConfig _config;

		public AirWaybillController(
			IAwbPresenter awbPresenter,
			IApplicationAwbManager applicationAwbManager,
			IAwbUpdateManager awbUpdateManager,
			IAwbManager awbManager,
			IStateConfig config,
			IAwbRepository awbs,
			IAwbStateManager awbStateManager,
			IBrokerRepository brokers,
			IIdentityService identity)
		{
			_awbPresenter = awbPresenter;
			_applicationAwbManager = applicationAwbManager;
			_awbUpdateManager = awbUpdateManager;
			_awbManager = awbManager;
			_config = config;
			_awbs = awbs;
			_awbStateManager = awbStateManager;
			_brokers = brokers;
			_identity = identity;
		}

		#region Create

		[Access(RoleType.Admin)]
		public virtual ActionResult Create(long? applicationId)
		{
			return View();
		}

		[HttpPost, Access(RoleType.Admin)]
		public virtual ActionResult Create(long? applicationId, AwbAdminModel model)
		{
			if (!ModelState.IsValid) return View(model);

			try
			{
				_awbManager.Create(applicationId, model);

				return RedirectToAction(MVC.AirWaybill.Index());
			}
			catch (DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);

				return View(model);
			}
		}

		#endregion

		#region List

		[Access(RoleType.Admin, RoleType.Broker, RoleType.Sender), HttpGet]
		public virtual ViewResult Index()
		{
			return View();
		}

		[Access(RoleType.Admin, RoleType.Broker, RoleType.Sender), HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, int skip)
		{
			long? brokerId = null;
			if (_identity.IsInRole(RoleType.Broker) && _identity.Id.HasValue)
			{
				var broker = _brokers.GetByUserId(_identity.Id.Value);
				brokerId = broker.Id;
			}

			var list = _awbPresenter.List(take, skip, brokerId, _identity.Language);

			return Json(list);
		}

		#endregion

		#region Actions

		[HttpPost, Access(RoleType.Admin, RoleType.Sender)]
		public virtual HttpStatusCodeResult Delete(long id)
		{
			_awbManager.Delete(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[Access(RoleType.Admin, RoleType.Sender), HttpPost]
		public virtual ActionResult SetAirWaybill(long applicationId, long? airWaybillId)
		{
			_applicationAwbManager.SetAwb(applicationId, airWaybillId);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[Access(RoleType.Admin, RoleType.Broker), HttpPost]
		public virtual HttpStatusCodeResult CargoIsCustomsCleared(long id)
		{
			var data = _awbs.Get(id).First();
			if (data.GTD.IsNullOrWhiteSpace())
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

		#endregion

		#region Edit

		[Access(RoleType.Admin), HttpGet]
		public virtual ViewResult Edit(long id)
		{
			var model = _awbPresenter.Get(id);

			ViewBag.AwbId = id;

			return View(model);
		}

		[Access(RoleType.Admin), HttpPost]
		public virtual ActionResult Edit(long id, AwbAdminModel model)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.AwbId = id;

				return View(model);
			}

			try
			{
				_awbUpdateManager.Update(id, model);
			}
			catch (DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);
				return View(model);
			}

			return RedirectToAction(MVC.AirWaybill.Edit(id));
		}

		#endregion

		#region Files

		public virtual FileResult InvoiceFile(long id)
		{
			var file = _awbs.GetInvoiceFile(id);

			return file.GetFileResult();
		}

		public virtual FileResult GTDFile(long id)
		{
			var file = _awbs.GetGTDFile(id);

			return file.GetFileResult();
		}

		public virtual FileResult GTDAdditionalFile(long id)
		{
			var file = _awbs.GTDAdditionalFile(id);

			return file.GetFileResult();
		}

		public virtual FileResult PackingFile(long id)
		{
			var file = _awbs.GetPackingFile(id);

			return file.GetFileResult();
		}

		public virtual FileResult AWBFile(long id)
		{
			var file = _awbs.GetAWBFile(id);

			return file.GetFileResult();
		}

		#endregion
	}
}