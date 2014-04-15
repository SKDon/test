using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Controllers.Application
{
	public partial class ApplicationController : Controller
	{
		private readonly IApplicationRepository _applications;
		private readonly ICarrierRepository _carriers;
		private readonly IClientRepository _clients;
		private readonly ICountryRepository _countries;
		private readonly IForwarderRepository _forwarders;
		private readonly IIdentityService _identity;
		private readonly IAdminApplicationManager _manager;
		private readonly IApplicationPresenter _presenter;
		private readonly ISenderRepository _senders;

		public ApplicationController(
			IAdminApplicationManager manager,
			IApplicationPresenter presenter,
			IForwarderRepository forwarders,
			ICarrierRepository carriers,
			IIdentityService identity,
			ICountryRepository countries,
			IApplicationRepository applications,
			IClientRepository clients,
			ISenderRepository senders)
		{
			_manager = manager;
			_presenter = presenter;
			_forwarders = forwarders;
			_carriers = carriers;
			_identity = identity;
			_countries = countries;
			_applications = applications;
			_clients = clients;
			_senders = senders;
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual HttpStatusCodeResult Delete(long id)
		{
			_manager.Delete(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		private void BindBag(long clientId, long? applicationId, int? count = 0)
		{
			var client = _clients.Get(clientId);

			ViewBag.ClientNic = client.Nic;

			ViewBag.ClientId = client.ClientId;

			ViewBag.ApplicationId = applicationId;

			if(applicationId.HasValue)
			{
				throw new NotImplementedException("set ApplicationNumber");
				// ViewBag.ApplicationNumber = ApplicationHelper.GetApplicationDisplay(applicationId.Value, count);
			}

			ViewBag.Countries = _countries.All(_identity.Language).ToDictionary(x => x.Id, x => x.Name);

			ViewBag.Senders = _senders.GetAll().OrderBy(x => x.Name).ToDictionary(x => (long?)x.EntityId, x => x.Name);

			ViewBag.Forwarders = _forwarders.GetAll().OrderBy(x => x.Name).ToDictionary(x => (long?)x.Id, x => x.Name);

			ViewBag.Carriers = _carriers.GetAll().OrderBy(x => x.Name).ToDictionary(x => (long?)x.Id, x => x.Name);
		}

		#region Edit

		[HttpGet]
		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ViewResult Edit(long id)
		{
			var application = _presenter.Get(id);

			var clientId = _applications.GetClientId(id);

			BindBag(clientId, id, application.Count);

			return View(application);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ActionResult Edit(long id, ApplicationAdminModel model,
			[Bind(Prefix = "Transit")] TransitEditModel transitModel)
		{
			if(!ModelState.IsValid)
			{
				var clientId = _applications.GetClientId(id);

				BindBag(clientId, id, model.Count);

				return View(model);
			}

			_manager.Update(id, model, transitModel);

			return RedirectToAction(MVC.Application.Edit(id));
		}

		#endregion

		#region Create

		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ViewResult Create(long clientId)
		{
			BindBag(clientId, null);

			return View(new ApplicationAdminModel
			{
				InsuranceRate = _applications.GetDefaultInsuranceRate() * 100
			});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ActionResult Create(long clientId, ApplicationAdminModel model,
			[Bind(Prefix = "Transit")] TransitEditModel transitModel)
		{
			if(!ModelState.IsValid)
			{
				BindBag(clientId, null);

				return View(model);
			}

			try
			{
				_manager.Add(model, transitModel, clientId);
			}
			catch(DublicateException ex)
			{
				ModelState.AddModelError("DublicateException", ex.ToString());

				BindBag(clientId, null);

				return View(model);
			}

			return RedirectToAction(MVC.ApplicationList.Index());
		}

		#endregion
	}
}