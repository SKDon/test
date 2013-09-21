using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Antlr.Runtime.Misc;

namespace Alicargo.Controllers
{
	public partial class TransitController : Controller
	{
		private readonly ITransitRepository _transitRepository;
		private readonly ITransitService _transitService;

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
			return GetEditPartialView(applicationId, id => _transitRepository.GetByApplication(id));
		}

		private PartialViewResult GetEditPartialView(long? id, Func<long, TransitData> getData)
		{
			ViewData.TemplateInfo.HtmlFieldPrefix = "Transit";

			if (!id.HasValue) return PartialView();

			var data = getData(id.Value);

			var transit = TransitEditModel.GetModel(data);

			ViewBag.TransitId = data.Id;

			return PartialView(transit);
		}

		[ChildActionOnly]
		public virtual PartialViewResult EditByClient(long? clientId)
		{
			return GetEditPartialView(clientId, id => _transitRepository.GetByClient(id));
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