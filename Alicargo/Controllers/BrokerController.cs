using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services.Abstract;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;
using Resources;

namespace Alicargo.Controllers
{
	public partial class BrokerController : Controller
	{
		private readonly IAwbRepository _awbRepository;
		private readonly IAwbUpdateManager _awbUpdateManager;
		private readonly IBrokerRepository _brokerRepository;
		private readonly IStateConfig _stateConfig;

		public BrokerController(
			IBrokerRepository brokerRepository,
			IStateConfig stateConfig,
			IAwbRepository awbRepository,
			IAwbUpdateManager awbUpdateManager)
		{
			_brokerRepository = brokerRepository;
			_stateConfig = stateConfig;
			_awbRepository = awbRepository;
			_awbUpdateManager = awbUpdateManager;
		}

		[ChildActionOnly]
		public virtual PartialViewResult Select(string name, long? selectedId)
		{
			var all = _brokerRepository.GetAll();

			var model = new SelectModel
			{
				Id = selectedId.HasValue
					? selectedId.Value
					: all.First().Id,
				List = all.ToDictionary(x => x.Id, x => x.Name),
				Name = name
			};

			return PartialView(model);
		}

		[Access(RoleType.Broker), HttpGet]
		public virtual ViewResult AWB(long id)
		{
			var data = _awbRepository.Get(id).First();

			if (data.StateId == _stateConfig.CargoIsCustomsClearedStateId)
			{
				return View("Message", (object) string.Format(Pages.CantEditAirWaybill, data.Bill));
			}

			var model = Map(data);

			BindBag(data);

			return View(model);
		}

		private static AwbBrokerModel Map(AirWaybillData data)
		{
			return new AwbBrokerModel
			{
				GTD = data.GTD,
				GTDAdditionalFileName = data.GTDAdditionalFileName,
				InvoiceFileName = data.InvoiceFileName,
				GTDFileName = data.GTDFileName,
				PackingFileName = data.PackingFileName,
				GTDAdditionalFile = null,
				InvoiceFile = null,
				GTDFile = null,
				PackingFile = null,
				BrokerCost = data.BrokerCost,
				CustomCost = data.CustomCost
			};
		}

		private void BindBag(AirWaybillData data)
		{
			ViewBag.AWB = data.Bill;
			ViewBag.AwbId = data.Id;
		}

		[Access(RoleType.Broker), HttpPost]
		public virtual ActionResult AWB(long id, AwbBrokerModel model)
		{
			if (!ModelState.IsValid)
			{
				var data = _awbRepository.Get(id).First();
				BindBag(data);

				return View(model);
			}

			try
			{
				_awbUpdateManager.Update(id, model);
			}
			catch (UnexpectedStateException ex)
			{
				if (ex.StateId == _stateConfig.CargoIsCustomsClearedStateId)
				{
					var data = _awbRepository.Get(id).First();

					return View("Message", (object) string.Format(Pages.CantEditAirWaybill, data.Bill));
				}

				throw;
			}

			return RedirectToAction(MVC.Broker.AWB(id));
		}
	}
}