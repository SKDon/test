using System.Web.Mvc;
using Alicargo.Core.Contracts;
using Alicargo.Core.Contracts.AirWaybill;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
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
		private readonly IStateConfig _stateConfig;


		public SenderAwbController(IAwbUpdateManager awbUpdateManager, IAwbManager awbManager, IAwbPresenter awbPresenter,
			IStateConfig stateConfig)
		{
			_awbUpdateManager = awbUpdateManager;
			_awbManager = awbManager;
			_awbPresenter = awbPresenter;
			_stateConfig = stateConfig;
		}

		[Access(RoleType.Sender)]
		[HttpGet]
		public virtual ViewResult Edit(long id)
		{
			var model = _awbPresenter.GetSenderAwbModel(id);

			ViewBag.AwbId = id;

			return View(model);
		}

		[Access(RoleType.Sender)]
		[HttpPost]
		public virtual ActionResult Edit(long id, AwbSenderModel model)
		{
			if(!ModelState.IsValid) return View(model);

			try
			{
				_awbUpdateManager.Update(id, model);
			}
			catch(DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);
				return View(model);
			}

			return RedirectToAction(MVC.SenderAwb.Edit(id));
		}

		[Access(RoleType.Sender)]
		public virtual ActionResult Create(long? id)
		{
			return View();
		}

		[HttpPost]
		[Access(RoleType.Sender)]
		public virtual ActionResult Create(long? id, AwbSenderModel model)
		{
			if(!ModelState.IsValid) return View(model);

			try
			{
				var airWaybillData = AwbMapper.Map(model, _stateConfig.CargoIsFlewStateId);

				_awbManager.Create(id, airWaybillData, null, null, model.PackingFile, null, model.AWBFile);

				return RedirectToAction(MVC.AirWaybill.Index());
			}
			catch(DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);

				return View(model);
			}
		}
	}
}