using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Enums;
using Alicargo.Core.Repositories;
using Alicargo.Helpers;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Resources;

namespace Alicargo.Controllers
{
	// todo: refactor contracts
	public partial class BrockerController : Controller
	{
		private readonly IBrockerRepository _brockerRepository;
		private readonly IStateConfig _stateConfig;
		private readonly IReferenceRepository _referenceRepository;
		private readonly IAwbPresenter _awbPresenter;
		private readonly IAwbManager _awbManager;

		public BrockerController(
			IBrockerRepository brockerRepository,
			IStateConfig stateConfig,
			IReferenceRepository referenceRepository,
			IAwbPresenter awbPresenter,
			IAwbManager awbManager)
		{
			_brockerRepository = brockerRepository;
			_stateConfig = stateConfig;
			_referenceRepository = referenceRepository;
			_awbPresenter = awbPresenter;
			_awbManager = awbManager;
		}

		[ChildActionOnly, Access(RoleType.Admin, RoleType.Sender)]
		public virtual PartialViewResult Select(string name, long? selectedId)
		{
			var all = _brockerRepository.GetAll();

			var model = new SelectModel
			{
				Id = selectedId.HasValue ? selectedId.Value : all.First().Id, // todo: test
				List = all.ToDictionary(x => x.Id, x => x.Name),
				Name = name
			};

			return PartialView(model);
		}

		[Access(RoleType.Brocker), HttpGet]
		public virtual ViewResult AWB(long id)
		{
			var data = _referenceRepository.Get(id).First();

			if (data.StateId == _stateConfig.CargoIsCustomsClearedStateId)
			{
				return View("Message", (object)string.Format(Pages.CantEditReference, data.Bill));
			}

			var model = new BrockerAWBModel
			{
				GTD = data.GTD,
				GTDAdditionalFileName = data.GTDAdditionalFileName,
				GTDAdditionalFile = null,
				InvoiceFileName = data.InvoiceFileName,
				InvoiceFile = null,
				GTDFile = null,
				GTDFileName = data.GTDFileName,
				PackingFileName = data.PackingFileName,
				Id = id,
				PackingFile = null
			};

			ViewBag.AWB = data.Bill;

			return View(model);
		}

		// todo: test
		[Access(RoleType.Brocker), HttpPost]
		public virtual ActionResult AWB(BrockerAWBModel model)
		{
			if (!ModelState.IsValid) return View(model);

			var data = _awbPresenter.Get(model.Id);

			if (data.StateId == _stateConfig.CargoIsCustomsClearedStateId)
			{
				return View("Message", (object)string.Format(Pages.CantEditReference, data.Bill));
			}

			data.GTD = model.GTD;
			data.GTDFile = model.GTDFile;
			data.GTDFileName = model.GTDFileName;
			data.InvoiceFile = model.InvoiceFile;
			data.InvoiceFileName = model.InvoiceFileName;
			data.GTDAdditionalFile = model.GTDAdditionalFile;
			data.GTDAdditionalFileName = model.GTDAdditionalFileName;
			data.PackingFile = model.PackingFile;
			data.PackingFileName = model.PackingFileName;

			_awbManager.Update(data);

			return RedirectToAction(MVC.Brocker.AWB(model.Id));
		}
	}
}
