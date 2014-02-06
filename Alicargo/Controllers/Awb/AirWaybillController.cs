using System.Linq;
using System.Net;
using System.Web.Mvc;
using Alicargo.Core.Contracts.AirWaybill;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Extensions;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.Services.AirWaybill;
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
		private readonly IAwbStateManager _awbStateManager;
		private readonly IAwbUpdateManager _awbUpdateManager;
		private readonly IAwbRepository _awbs;
		private readonly IBrokerRepository _brokers;
		private readonly IStateConfig _config;
		private readonly IIdentityService _identity;

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
			_brokers = brokers;
			_applicationAwbManager = applicationAwbManager;
			_awbUpdateManager = awbUpdateManager;
			_awbManager = awbManager;
			_config = config;
			_awbs = awbs;
			_awbStateManager = awbStateManager;
			_brokers = brokers;
			_identity = identity;
		}

		private void BindBag(long? awbId)
		{
			ViewBag.AwbId = awbId;

			ViewBag.Brokers = _brokers.GetAll().ToDictionary(x => x.Id, x => x.Name);
		}

		#region Create

		[Access(RoleType.Admin)]
		public virtual ActionResult Create(long? applicationId)
		{
			BindBag(null);

			return View();
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual ActionResult Create(long? applicationId, AwbAdminModel model)
		{
			try
			{
				if(ModelState.IsValid)
				{
					var data = AwbMapper.Map(model, _config.CargoIsFlewStateId);

					_awbManager.Create(
						applicationId,
						data,
						model.GTDFile,
						model.GTDAdditionalFile,
						model.PackingFile,
						model.InvoiceFile,
						model.AWBFile);

					return RedirectToAction(MVC.AirWaybill.Index());
				}
			}
			catch(DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);
			}

			BindBag(null);

			return View(model);
		}

		#endregion

		#region List

		[Access(RoleType.Admin, RoleType.Broker, RoleType.Sender)]
		[HttpGet]
		public virtual ViewResult Index()
		{
			return View();
		}

		[Access(RoleType.Admin, RoleType.Broker, RoleType.Sender)]
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

		[Access(RoleType.Admin, RoleType.Broker)]
		[HttpPost]
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
		[Access(RoleType.Admin, RoleType.Sender)]
		public virtual HttpStatusCodeResult Delete(long id)
		{
			_awbManager.Delete(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[Access(RoleType.Admin, RoleType.Sender)]
		[HttpPost]
		public virtual ActionResult SetAirWaybill(long applicationId, long? airWaybillId)
		{
			_applicationAwbManager.SetAwb(applicationId, airWaybillId);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		#endregion

		#region Edit

		[Access(RoleType.Admin)]
		[HttpGet]
		public virtual ViewResult Edit(long id)
		{
			var model = _awbPresenter.Get(id);

			BindBag(id);

			return View(model);
		}

		[Access(RoleType.Admin)]
		[HttpPost]
		public virtual ActionResult Edit(long id, AwbAdminModel model)
		{
			try
			{
				if(ModelState.IsValid)
				{
					_awbUpdateManager.Update(id, model);

					return RedirectToAction(MVC.AirWaybill.Edit(id));
				}
			}
			catch(DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);				
			}

			BindBag(id);

			return View(model);
		}

		#endregion

		#region Files

		public virtual FileResult AWBFile(long id)
		{
			var file = _awbs.GetAWBFile(id);

			return file.GetFileResult();
		}

		public virtual FileResult GTDAdditionalFile(long id)
		{
			var file = _awbs.GTDAdditionalFile(id);

			return file.GetFileResult();
		}

		public virtual FileResult GTDFile(long id)
		{
			var file = _awbs.GetGTDFile(id);

			return file.GetFileResult();
		}

		public virtual FileResult InvoiceFile(long id)
		{
			var file = _awbs.GetInvoiceFile(id);

			return file.GetFileResult();
		}

		public virtual FileResult PackingFile(long id)
		{
			var file = _awbs.GetPackingFile(id);

			return file.GetFileResult();
		}

		#endregion
	}
}