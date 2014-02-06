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
	public partial class SenderAwbController : Controller
	{
		private readonly IAwbManager _awbManager;
		private readonly IAwbPresenter _awbPresenter;
		private readonly IAwbUpdateManager _awbUpdateManager;
		private readonly IBrokerRepository _brokers;
		private readonly IStateConfig _stateConfig;

		public SenderAwbController(
			IAwbUpdateManager awbUpdateManager,
			IAwbManager awbManager,
			IAwbPresenter awbPresenter,
			IStateConfig stateConfig,
			IBrokerRepository brokers)
		{
			_awbUpdateManager = awbUpdateManager;
			_awbManager = awbManager;
			_awbPresenter = awbPresenter;
			_stateConfig = stateConfig;
			_brokers = brokers;
		}

		[Access(RoleType.Sender)]
		public virtual ActionResult Create(long? id)
		{
			BindBag(null);

			return View();
		}

		[HttpPost]
		[Access(RoleType.Sender)]
		public virtual ActionResult Create(long? id, AwbSenderModel model)
		{
			try
			{
				if(ModelState.IsValid)
				{
					var airWaybillData = AwbMapper.Map(model, _stateConfig.CargoIsFlewStateId);

					_awbManager.Create(id, airWaybillData, null, null, model.PackingFile, null, model.AWBFile);

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

		[Access(RoleType.Sender)]
		[HttpGet]
		public virtual ViewResult Edit(long id)
		{
			var model = _awbPresenter.GetSenderAwbModel(id);

			BindBag(id);

			return View(model);
		}

		[Access(RoleType.Sender)]
		[HttpPost]
		public virtual ActionResult Edit(long id, AwbSenderModel model)
		{
			try
			{
				if(ModelState.IsValid)
				{
					_awbUpdateManager.Update(id, model);

					return RedirectToAction(MVC.SenderAwb.Edit(id));
				}
			}
			catch(DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);
			}

			BindBag(id);

			return View(model);
		}

		private void BindBag(long? awbId)
		{
			ViewBag.AwbId = awbId;

			ViewBag.Brokers = _brokers.GetAll().ToDictionary(x => x.Id, x => x.Name);
		}
	}
}