using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Enums;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Helpers;
using Alicargo.Core.Repositories;
using Alicargo.Helpers;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using System.Net;

namespace Alicargo.Controllers
{
	public partial class ApplicationController : Controller
	{
		private readonly IApplicationPresenter _applicationPresenter;
		private readonly IApplicationManager _applicationManager;
		private readonly IStateConfig _stateConfig;
		private readonly IIdentityService _identityService;
		private readonly IClientService _clientService;
		private readonly ICountryRepository _countryRepository;
		private readonly IApplicationRepository _applicationRepository;

		public ApplicationController(
			IApplicationPresenter applicationPresenter,
			IApplicationManager applicationManager,
			IStateConfig stateConfig,
			IIdentityService identityService,
			IClientService clientService,
			ICountryRepository countryRepository,
			IApplicationRepository applicationRepository)
		{
			_applicationPresenter = applicationPresenter;
			_applicationManager = applicationManager;
			_stateConfig = stateConfig;
			_identityService = identityService;
			_clientService = clientService;
			_countryRepository = countryRepository;
			_applicationRepository = applicationRepository;
		}

		#region List

		[Access(RoleType.Admin, RoleType.Client, RoleType.Forwarder, RoleType.Sender)]
		public virtual ViewResult Index()
		{
			var model = _applicationPresenter.GetApplicationIndexModel();

			return View(model);
		}

		[HttpGet, ChildActionOnly]
		[Access(RoleType.Admin, RoleType.Client, RoleType.Sender)]
		public virtual PartialViewResult Details(long id)
		{
			var application = _applicationPresenter.Get(id);

			return PartialView(application);
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Client, RoleType.Forwarder, RoleType.Sender)]
		public virtual JsonResult List(int take, int skip, int page, int pageSize, Dictionary<string, string>[] group)
		{
			// todo: use model binder for Order
			var orders = Order.Get(group);

			var data = _applicationPresenter.List(take, skip, orders);

			return Json(data);
		}

		public virtual FileResult InvoiceFile(long id)
		{
			var file = _applicationRepository.GetInvoiceFile(id);

			return file.FileData.FileResult(file.FileName);
		}

		public virtual FileResult DeliveryBillFile(long id)
		{
			var file = _applicationRepository.GetDeliveryBillFile(id);

			return file.FileData.FileResult(file.FileName);
		}

		public virtual FileResult CPFile(long id)
		{
			var file = _applicationRepository.GetCPFile(id);

			return file.FileData.FileResult(file.FileName);
		}

		public virtual FileResult SwiftFile(long id)
		{
			var file = _applicationRepository.GetSwiftFile(id);

			return file.FileData.FileResult(file.FileName);
		}

		public virtual FileResult Torg12File(long id)
		{
			var file = _applicationRepository.GetTorg12File(id);

			return file.FileData.FileResult(file.FileName);
		}

		public virtual FileResult PackingFile(long id)
		{
			var file = _applicationRepository.GetPackingFile(id);

			return file.FileData.FileResult(file.FileName);
		}

		#endregion

		#region Set state

		// todo: test
		[HttpPost]
		[Access(RoleType.Client, RoleType.Admin)]
		public virtual ActionResult Close(long id)
		{
			_applicationManager.SetState(id, _stateConfig.CargoReceivedStateId);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		// todo: test
		[HttpPost]
		[Access(RoleType.Admin, RoleType.Brocker, RoleType.Forwarder, RoleType.Sender)]
		public virtual HttpStatusCodeResult SetState(long id, long stateId)
		{
			_applicationManager.SetState(id, stateId);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[HttpPost]
		public virtual JsonResult States(long id)
		{
			return Json(_applicationPresenter.GetAvailableStates(id));
		}

		#endregion

		#region Edit

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Edit(long id)
		{
			var application = _applicationPresenter.Get(id);

			BindCountries();

			return View(application);
		}

		private void BindCountries()
		{
			ViewBag.Countries = _countryRepository.Get()
				.ToDictionary(x => x.Id, x => x.Name[_identityService.TwoLetterISOLanguageName]);
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual HttpStatusCodeResult Delete(long id)
		{
			_applicationManager.Delete(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[Access(RoleType.Admin, RoleType.Forwarder), HttpPost]
		public virtual HttpStatusCodeResult SetTransitReference(long id, string transitReference)
		{
			_applicationManager.SetTransitReference(id, transitReference);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[Access(RoleType.Admin, RoleType.Forwarder), HttpPost]
		public virtual HttpStatusCodeResult SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt)
		{
			_applicationManager.SetDateOfCargoReceipt(id, dateOfCargoReceipt);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		// todo: test
		[HttpPost]
		//[ValidateAntiForgeryToken]
		[Access(RoleType.Admin)]
		public virtual ActionResult Edit(long id, ApplicationModel model, CarrierSelectModel carrierSelectModel)
		{
			if (!ModelState.IsValid)
			{
				BindCountries();
				return View(model);
			}

			_applicationManager.Update(model, carrierSelectModel);

			return RedirectToAction(MVC.Application.Index());
		}

		#endregion

		#region Create

		[Access(RoleType.Admin, RoleType.Client)]
		public virtual ViewResult Create(long? clientId)
		{
			var client = _clientService.GetClient(clientId);

			var model = new ApplicationModel
			{
				Transit = client.Transit,
				LegalEntity = client.LegalEntity,
				CurrencyId = (int)CurrencyType.Euro,
				ClientNic = client.Nic,
				ClientEmail = client.Email
			};

			BindCountries();

			return View(model);
		}

		// todo: test
		[HttpPost]
		//[ValidateAntiForgeryToken]
		[Access(RoleType.Admin, RoleType.Client)]
		public virtual ActionResult Create(long? clientId, ApplicationModel model, CarrierSelectModel carrierSelectModel)
		{
			if (!ModelState.IsValid)
			{
				BindCountries();
				return View(model);

			}

			var client = _clientService.GetClient(clientId);

			model.ClientId = client.Id;

			try
			{
				_applicationManager.Add(model, carrierSelectModel);
			}
			catch (DublicateException ex)
			{
				ModelState.AddModelError("DublicateException", ex.ToString());
				return View(model);
			}

			return RedirectToAction(MVC.Application.Index());
		}

		#endregion
	}
}