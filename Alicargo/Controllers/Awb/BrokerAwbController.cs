using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Exceptions;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Awb;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.Services.AirWaybill;
using Alicargo.ViewModels.AirWaybill;
using Resources;

namespace Alicargo.Controllers.Awb
{
	public partial class BrokerAwbController : Controller
	{
		private readonly IAwbRepository _awbRepository;
		private readonly IAwbUpdateManager _awbUpdateManager;
		private readonly IStateConfig _stateConfig;

		public BrokerAwbController(
			IStateConfig stateConfig,
			IAwbRepository awbRepository,
			IAwbUpdateManager awbUpdateManager)
		{
			_stateConfig = stateConfig;
			_awbRepository = awbRepository;
			_awbUpdateManager = awbUpdateManager;
		}

		[HttpGet]
		[Access(RoleType.Broker)]
		public virtual ViewResult Edit(long id)
		{
			var data = _awbRepository.Get(id).First();

			if(data.StateId == _stateConfig.CargoIsCustomsClearedStateId)
			{
				return View("Message", (object)string.Format(Pages.CantEditAirWaybill, data.Bill));
			}

			var model = AwbMapper.GetBrokerModel(data);

			BindBag(data);

			return View(model);
		}

		[HttpPost]
		[Access(RoleType.Broker)]
		public virtual ActionResult Edit(long id, AwbBrokerModel model)
		{
			try
			{
				if(ModelState.IsValid)
				{
					_awbUpdateManager.Update(id, model);

					return RedirectToAction(MVC.BrokerAwb.Edit(id));
				}

				var data = _awbRepository.Get(id).First();

				BindBag(data);

				return View(model);
			}
			catch(UnexpectedStateException ex)
			{
				if(ex.StateId == _stateConfig.CargoIsCustomsClearedStateId)
				{
					var data = _awbRepository.Get(id).First();

					return View("Message", (object)string.Format(Pages.CantEditAirWaybill, data.Bill));
				}

				throw;
			}
		}

		private void BindBag(AirWaybillData data)
		{
			ViewBag.AWB = data.Bill;

			ViewBag.AwbId = data.Id;
		}
	}
}