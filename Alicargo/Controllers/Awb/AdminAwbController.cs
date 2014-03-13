using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Contracts.AirWaybill;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.Services.AirWaybill;
using Alicargo.ViewModels.AirWaybill;
using Resources;

namespace Alicargo.Controllers.Awb
{
	public partial class AdminAwbController : Controller
	{
		private readonly IAwbManager _awbManager;
		private readonly IAwbPresenter _awbPresenter;
		private readonly IAwbUpdateManager _awbUpdateManager;
		private readonly IBrokerRepository _brokers;
		private readonly IStateConfig _config;


		public AdminAwbController(
			IAwbManager awbManager,
			IAwbPresenter awbPresenter,
			IAwbUpdateManager awbUpdateManager,
			IBrokerRepository brokers,
			IStateConfig config)
		{
			_awbManager = awbManager;
			_awbPresenter = awbPresenter;
			_awbUpdateManager = awbUpdateManager;
			_brokers = brokers;
			_config = config;
		}

		private void BindBag(long? awbId)
		{
			ViewBag.AwbId = awbId;

			ViewBag.Brokers = _brokers.GetAll().ToDictionary(x => (long?)x.Id, x => x.Name);
		}

		#region Edit

		[Access(RoleType.Admin)]
		[HttpGet]
		public virtual ViewResult Edit(long id)
		{
			var model = _awbPresenter.Get(id);

			BindBag(id);

			return View(model);
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual ActionResult Edit(long id, AwbAdminModel model)
		{
			try
			{
				if(ModelState.IsValid)
				{
					_awbUpdateManager.Update(id, model);

					return RedirectToAction(MVC.AdminAwb.Edit(id));
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
					var data = AwbMapper.GetData(model, _config.CargoIsFlewStateId);

					var id = _awbManager.Create(
						applicationId,
						data,
						model.GTDFile,
						model.GTDAdditionalFile,
						model.PackingFile,
						model.InvoiceFile,
						model.AWBFile,
						model.DrawFile);

					return RedirectToAction(MVC.AdminAwb.Edit(id));
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
	}
}