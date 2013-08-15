using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Enums;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Repositories;
using Alicargo.Helpers;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Resources;

namespace Alicargo.Controllers
{
	public partial class BrockerController : Controller
	{
		private readonly IAWBRepository _awbRepository;
		private readonly IAwbManager _awbManager;
		private readonly IBrockerRepository _brockerRepository;
		private readonly IStateConfig _stateConfig;

		public BrockerController(
			IBrockerRepository brockerRepository,
			IStateConfig stateConfig,
			IAWBRepository awbRepository,
			IAwbManager awbManager)
		{
			_brockerRepository = brockerRepository;
			_stateConfig = stateConfig;
			_awbRepository = awbRepository;
			_awbManager = awbManager;
		}

		[ChildActionOnly]
		public virtual PartialViewResult Select(string name, long? selectedId)
		{
			var all = _brockerRepository.GetAll();

			var model = new SelectModel
			{
				Id = selectedId.HasValue
					? selectedId.Value
					: all.First().Id, // todo: test
				List = all.ToDictionary(x => x.Id, x => x.Name),
				Name = name
			};

			return PartialView(model);
		}

		[Access(RoleType.Brocker), HttpGet]
		public virtual ViewResult AWB(long id)
		{
			var data = _awbRepository.Get(id).First();

			if (data.StateId == _stateConfig.CargoIsCustomsClearedStateId)
			{
				return View("Message", (object) string.Format(Pages.CantEditAirWaybill, data.Bill));
			}

			var model = new BrockerAWBModel
			{
				GTD = data.GTD,
				GTDAdditionalFileName = data.GTDAdditionalFileName,
				InvoiceFileName = data.InvoiceFileName,
				GTDFileName = data.GTDFileName,
				PackingFileName = data.PackingFileName,
				GTDAdditionalFile = null,
				InvoiceFile = null,
				GTDFile = null,
				PackingFile = null
			};

			BindBag(data);

			return View(model);
		}

		private void BindBag(AirWaybillData data)
		{
			ViewBag.AWB = data.Bill;
			ViewBag.AwbId = data.Id;
		}

		// todo: test
		[Access(RoleType.Brocker), HttpPost]
		public virtual ActionResult AWB(long id, BrockerAWBModel model)
		{
			if (!ModelState.IsValid)
			{
				var data = _awbRepository.Get(id).First();
				BindBag(data);

				return View(model);
			}

			try
			{
				_awbManager.Update(id, model);
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

			return RedirectToAction(MVC.Brocker.AWB(id));
		}
	}
}