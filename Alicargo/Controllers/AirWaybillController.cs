using System.Linq;
using System.Net;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Helpers;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;
using Microsoft.Ajax.Utilities;
using Resources;

namespace Alicargo.Controllers
{
    public partial class AirWaybillController : Controller
    {
        private readonly IApplicationAwbManager _applicationAwbManager;
        private readonly IAwbManager _awbManager;
        private readonly IAwbPresenter _awbPresenter;
        private readonly IAwbRepository _awbRepository;
        private readonly IAwbStateManager _awbStateManager;
        private readonly IAwbUpdateManager _awbUpdateManager;
        private readonly IBrockerRepository _brockerRepository;
        private readonly IIdentityService _identityService;
        private readonly IStateConfig _stateConfig;

        public AirWaybillController(
            IAwbPresenter awbPresenter,
            IApplicationAwbManager applicationAwbManager,
            IAwbUpdateManager awbUpdateManager,
            IAwbManager awbManager,
            IStateConfig stateConfig,
            IAwbRepository awbRepository,
            IAwbStateManager awbStateManager,
            IBrockerRepository brockerRepository,
            IIdentityService identityService)
        {
            _awbPresenter = awbPresenter;
            _applicationAwbManager = applicationAwbManager;
            _awbUpdateManager = awbUpdateManager;
            _awbManager = awbManager;
            _stateConfig = stateConfig;
            _awbRepository = awbRepository;
            _awbStateManager = awbStateManager;
            _brockerRepository = brockerRepository;
            _identityService = identityService;
        }

        #region Create

        [Access(RoleType.Admin, RoleType.Sender)]
        public virtual ActionResult Create(long applicationId)
        {
            return View();
        }

        [HttpPost, Access(RoleType.Admin, RoleType.Sender)]
        public virtual ActionResult Create(long applicationId, AirWaybillEditModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                _awbManager.Create(applicationId, model);
            }
            catch (DublicateException)
            {
                ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);
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
            // todo: 3. utility to get current broker
            long? brockerId = null;
            if (_identityService.IsInRole(RoleType.Brocker) && _identityService.Id.HasValue)
            {
                var brocker = _brockerRepository.GetByUserId(_identityService.Id.Value);
                brockerId = brocker.Id;
            }

            var list = _awbPresenter.List(take, skip, brockerId);

            return Json(list);
        }

        #endregion

        #region Edit

        [HttpPost, Access(RoleType.Admin, RoleType.Sender)]
        public virtual HttpStatusCodeResult Delete(long id)
        {
            _awbManager.Delete(id);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [Access(RoleType.Admin, RoleType.Brocker), HttpPost]
        public virtual ActionResult SetState(long id, long stateId)
        {
            _awbStateManager.SetState(id, stateId);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [Access(RoleType.Admin, RoleType.Sender), HttpPost]
        public virtual ActionResult SetAirWaybill(long applicationId, long? airWaybillId)
        {
            _applicationAwbManager.SetAwb(applicationId, airWaybillId);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [Access(RoleType.Admin, RoleType.Brocker), HttpPost]
        public virtual HttpStatusCodeResult CargoIsCustomsCleared(long id)
        {
            var data = _awbRepository.Get(id).First();
            if (data.GTD.IsNullOrWhiteSpace())
            {
                throw new InvalidLogicException("GTD must be definded to set the CargoIsCustomsCleared state");
            }

            _awbStateManager.SetState(id, _stateConfig.CargoIsCustomsClearedStateId);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [ChildActionOnly]
        public virtual PartialViewResult CargoIsCustomsClearedButton(long id)
        {
            var data = _awbRepository.Get(id).First();

            var model = new CargoIsCustomsClearedButtonModel
                {
                    Id = id,
                    CanSetCargoIsCustomsCleared =
                        data.StateId == _stateConfig.CargoAtCustomsStateId && !data.GTD.IsNullOrWhiteSpace()
                };

            return PartialView("CargoIsCustomsClearedButton", model);
        }

        [Access(RoleType.Admin), HttpGet]
        public virtual ViewResult Edit(long id)
        {
            var model = _awbPresenter.Get(id);

            ViewBag.AwbId = id;

            return View(model);
        }

        [Access(RoleType.Admin), HttpPost]
        public virtual ActionResult Edit(long id, AirWaybillEditModel model)
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

            return RedirectToAction(MVC.AirWaybill.Edit(id));
        }

        #endregion

        #region Files

        public virtual FileResult InvoiceFile(long id)
        {
            var file = _awbRepository.GetInvoiceFile(id);

            return file.FileData.GetFileResult(file.FileName);
        }

        public virtual FileResult GTDFile(long id)
        {
            var file = _awbRepository.GetGTDFile(id);

            return file.FileData.GetFileResult(file.FileName);
        }

        public virtual FileResult GTDAdditionalFile(long id)
        {
            var file = _awbRepository.GTDAdditionalFile(id);

            return file.FileData.GetFileResult(file.FileName);
        }

        public virtual FileResult PackingFile(long id)
        {
            var file = _awbRepository.GetPackingFile(id);

            return file.FileData.GetFileResult(file.FileName);
        }

        public virtual FileResult AWBFile(long id)
        {
            var file = _awbRepository.GetAWBFile(id);

            return file.FileData.GetFileResult(file.FileName);
        }

        #endregion
    }
}