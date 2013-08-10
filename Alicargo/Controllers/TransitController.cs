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
	// todo: refactor contracts
	public partial class TransitController : Controller
	{
		private readonly ITransitService _transitService;
		private readonly ITransitRepository _transitRepository;

		public TransitController(ITransitService transitService, ITransitRepository transitRepository)
		{
			_transitService = transitService;
			_transitRepository = transitRepository;
		}

		[HttpGet]
		[Access(RoleType.Client)]
		public virtual ActionResult Edit(long id)
		{
			var transit = _transitService.Get(id).First();

			var applicationId = _transitRepository.GetaApplicationId(id);

			ViewBag.ApplicationId = applicationId;

			return View(transit);
		}

		[HttpPost]
		[Access(RoleType.Client)]
		public virtual ActionResult Edit(Transit model, CarrierSelectModel carrierSelectModel)
		{
			if (!ModelState.IsValid) return View(model);

			_transitService.Update(model, carrierSelectModel);

			return RedirectToAction(MVC.ApplicationList.Index());
		}
	}
}
