using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Enums;
using Alicargo.Core.Models;
using Alicargo.Core.Repositories;
using Alicargo.Helpers;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Controllers
{
	public partial class TransitController : Controller
	{
		private readonly ITransitService _transitService;
		private readonly ITransitRepository _transitRepository;

		public TransitController(
			ITransitService transitService,
			ITransitRepository transitRepository)
		{
			_transitService = transitService;
			_transitRepository = transitRepository;
		}

		[HttpGet, Access(RoleType.Client)]
		public virtual ActionResult Edit(long id)
		{
			var transit = _transitService.Get(id).First();

			var applicationId = _transitRepository.GetaApplicationId(id);

			ViewBag.ApplicationId = applicationId;

			return View(transit);
		}

		[ChildActionOnly]
		public virtual PartialViewResult EditByApplication(long? applicationId)
		{
			ViewData.TemplateInfo.HtmlFieldPrefix = "Transit";

			if (applicationId == null) return PartialView();

			var data = _transitRepository.GetByApplication(applicationId.Value);
			var transit = TransitEditModel.GetModel(data);			

			ViewBag.TransitId = data.Id;

			return PartialView(transit);
		}

		[HttpPost, Access(RoleType.Client)]
		public virtual ActionResult Edit(long id, TransitEditModel model, CarrierSelectModel carrierSelectModel)
		{
			if (!ModelState.IsValid) return View(model);

			_transitService.Update(id, model, carrierSelectModel);

			return RedirectToAction(MVC.ApplicationList.Index());
		}
	}
}
