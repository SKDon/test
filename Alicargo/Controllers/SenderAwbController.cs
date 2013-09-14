using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services;
using Alicargo.MvcHelpers;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.AirWaybill;
using Resources;

namespace Alicargo.Controllers
{
	public partial class SenderAwbController : Controller
	{
		private readonly IAwbRepository _awbRepository;
		private readonly IAwbManager _awbManager;
		private readonly IAwbUpdateManager _awbUpdateManager;

		public SenderAwbController(IAwbRepository awbRepository, IAwbUpdateManager awbUpdateManager, IAwbManager awbManager)
		{
			_awbRepository = awbRepository;
			_awbUpdateManager = awbUpdateManager;
			_awbManager = awbManager;
		}

		[Access(RoleType.Sender), HttpGet]
		public virtual ViewResult Edit(long id)
		{
			var data = _awbRepository.Get(id).First();

			ViewBag.AwbId = id;

			return View(new SenderAwbModel
			{
				AWBFile = null,
				AWBFileName = data.AWBFileName,
				ArrivalAirport = data.ArrivalAirport,
				Bill = data.Bill,
				BrokerId = data.BrokerId,
				DateOfArrivalLocalString = data.DateOfArrival.ToLocalShortDateString(),
				DateOfDepartureLocalString = data.DateOfDeparture.ToLocalShortDateString(),
				DepartureAirport = data.DepartureAirport,
				PackingFile = null,
				PackingFileName = data.PackingFileName,
				FlightCost = data.FlightCost
			});
		}

		[Access(RoleType.Sender), HttpPost]
		public virtual ActionResult Edit(long id, SenderAwbModel model)
		{
			if (!ModelState.IsValid) return View(model);

			try
			{
				_awbUpdateManager.Update(id, model);
			}
			catch (DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);
				return View(model);
			}

			return RedirectToAction(MVC.SenderAwb.Edit(id));
		}

		[Access(RoleType.Sender)]
		public virtual ActionResult Create(long id)
		{
			return View();
		}

		[HttpPost, Access(RoleType.Sender)]
		public virtual ActionResult Create(long id, SenderAwbModel model)
		{
			if (!ModelState.IsValid) return View(model);

			try
			{
				_awbManager.Create(id, model);
			}
			catch (DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);

				return View(model);
			}

			return RedirectToAction(MVC.ApplicationList.Index());
		}
	}
}
