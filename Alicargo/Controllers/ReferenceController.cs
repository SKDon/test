using System.Linq;
using System.Net;
using System.Web.Mvc;
using Alicargo.Core.Enums;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Repositories;
using Alicargo.Helpers;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Microsoft.Ajax.Utilities;
using Resources;

namespace Alicargo.Controllers
{
	// todo: refactor contracts
	// todo: rename to AwbContorller
	public partial class ReferenceController : Controller
	{
		private readonly IAwbPresenter _awbPresenter;
		private readonly IAwbManager _awbManager;
		private readonly IStateConfig _stateConfig;
		private readonly IReferenceRepository _referenceRepository;

		public ReferenceController(
			IAwbPresenter awbPresenter,
			IAwbManager awbManager,
			IStateConfig stateConfig,
			IReferenceRepository referenceRepository)
		{
			_awbPresenter = awbPresenter;
			_awbManager = awbManager;
			_stateConfig = stateConfig;
			_referenceRepository = referenceRepository;
		}

		#region Create

		[Access(RoleType.Admin, RoleType.Sender)]
		public virtual ActionResult Create(long applicationId)
		{
			return View();
		}

		[HttpPost]
		//[ValidateAntiForgeryToken]
		[Access(RoleType.Admin, RoleType.Sender)]
		public virtual ActionResult Create(long applicationId, ReferenceModel model)
		{
			if (!ModelState.IsValid) return View(model);

			try
			{
				_awbManager.Create(applicationId, model);
			}
			catch (DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.ReferenceAlreadyExists);
				return View(model);
			}

			return RedirectToAction(MVC.ApplicationList.Index());
		}

		#endregion

		#region List

		[Access(RoleType.Admin, RoleType.Brocker, RoleType.Sender), HttpGet]
		public virtual ViewResult Index()
		{
			return View();
		}

		[Access(RoleType.Admin, RoleType.Brocker, RoleType.Sender), HttpPost]
		public virtual JsonResult List(int take, int skip, int page, int pageSize)
		{
			var list = _awbPresenter.List(take, skip);

			return Json(list);
		}

		#endregion

		#region Edit

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Sender)]
		public virtual HttpStatusCodeResult Delete(long id)
		{
			_awbManager.Delete(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[Access(RoleType.Admin, RoleType.Brocker), HttpPost]
		public virtual ActionResult SetState(long id, long stateId)
		{
			_awbManager.SetState(id, stateId);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		//[HttpPost]
		//public virtual JsonResult States(long id)
		//{
		//	return Json(_awbPresenter.GetAvailableStates(id));
		//}

		[Access(RoleType.Admin, RoleType.Sender), HttpPost]
		public virtual ActionResult SetReference(long applicationId, long? referenceId)
		{
			_awbManager.SetAwb(applicationId, referenceId);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[Access(RoleType.Admin, RoleType.Brocker), HttpPost]
		public virtual HttpStatusCodeResult CargoIsCustomsCleared(long id)
		{
			var data = _referenceRepository.Get(id).First();
			if (data.GTD.IsNullOrWhiteSpace())
			{
				throw new InvalidLogicException("GTD must be definded to set the CargoIsCustomsCleared state");
			}

			_awbManager.SetState(id, _stateConfig.CargoIsCustomsClearedStateId);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[Access(RoleType.Admin, RoleType.Brocker), ChildActionOnly]
		public virtual PartialViewResult CargoIsCustomsClearedButton(long id)
		{
			var data = _referenceRepository.Get(id).First();

			var model = new CargoIsCustomsClearedButtonModel
			{
				Id = id,
				CanSetCargoIsCustomsCleared = data.StateId == _stateConfig.CargoAtCustomsStateId && !data.GTD.IsNullOrWhiteSpace()
			};

			return PartialView("CargoIsCustomsClearedButton", model);
		}

		[Access(RoleType.Admin, RoleType.Sender), HttpGet]
		public virtual ViewResult Edit(long id)
		{
			var model = _awbPresenter.Get(id);

			return View(model);
		}

		[Access(RoleType.Admin, RoleType.Sender), HttpPost]
		public virtual ActionResult Edit(ReferenceModel model)
		{
			if (!ModelState.IsValid) return View(model);

			try
			{
				_awbManager.Update(model);
			}
			catch (DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.ReferenceAlreadyExists);
				return View(model);
			}

			return RedirectToAction(MVC.Reference.Edit(model.Id));
		}

		#endregion

		#region Files

		public virtual FileResult InvoiceFile(long id)
		{
			var file = _referenceRepository.GetInvoiceFile(id);

			return file.FileData.GetFileResult(file.FileName);
		}

		public virtual FileResult GTDFile(long id)
		{
			var file = _referenceRepository.GetGTDFile(id);

			return file.FileData.GetFileResult(file.FileName);
		}

		public virtual FileResult GTDAdditionalFile(long id)
		{
			var file = _referenceRepository.GTDAdditionalFile(id);

			return file.FileData.GetFileResult(file.FileName);
		}

		public virtual FileResult PackingFile(long id)
		{
			var file = _referenceRepository.GetPackingFile(id);

			return file.FileData.GetFileResult(file.FileName);
		}

		public virtual FileResult AWBFile(long id)
		{
			var file = _referenceRepository.GetAWBFile(id);

			return file.FileData.GetFileResult(file.FileName);
		}

		#endregion
	}
}
