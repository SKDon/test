using System.Linq;
using System.Net;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Contracts.User;
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
	[Access(RoleType.Admin, RoleType.Manager)]
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
		public virtual HttpStatusCodeResult Delete(long id)
		{
			_manager.Delete(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		private void BindBag(long? applicationId, ClientData client)
		{
			ViewBag.ClientNic = client.Nic;

			ViewBag.ClientId = client.ClientId;

			ViewBag.ApplicationId = applicationId;

			if(applicationId.HasValue)
			{
				var data = _applications.Get(applicationId.Value);

				ViewBag.ApplicationNumber = data.DisplayNumber;
			}

			ViewBag.Countries = _countries.All(_identity.Language).ToDictionary(x => x.Id, x => x.Name);

			ViewBag.Senders = _senders.GetAll().OrderBy(x => x.Name).ToDictionary(x => (long?)x.EntityId, x => x.Name);

			ViewBag.Forwarders = _forwarders.GetAll().OrderBy(x => x.Name).ToDictionary(x => (long?)x.Id, x => x.Name);

			ViewBag.Carriers = _carriers.GetAll().OrderBy(x => x.Name).ToDictionary(x => (long?)x.Id, x => x.Name);
		}

		#region Edit

		[HttpGet]
		public virtual ViewResult Edit(long id)
		{
			var application = _presenter.Get(id);

			var clientId = _applications.GetClientId(id);

			var client = _clients.Get(clientId);

			BindBag(id, client);

			return View(application);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Edit(
			long id,
			ApplicationAdminModel model,
			[Bind(Prefix = "Transit")] TransitEditModel transitModel)
		{
			if(!ModelState.IsValid)
			{
				var clientId = _applications.GetClientId(id);

				var data = _clients.Get(clientId);

				BindBag(id, data);

				return View(model);
			}

			_manager.Update(id, model, transitModel);

			return RedirectToAction(MVC.Application.Edit(id));
		}

		#endregion

		#region Create

		[HttpGet]
		public virtual ViewResult Create(long clientId)
		{
			var client = _clients.Get(clientId);

			BindBag(null, client);

			var insuranceRate = client.InsuranceRate ?? _applications.GetDefaultInsuranceRate();

			return View(
				new ApplicationAdminModel
				{
					SenderId = client.DefaultSenderId,
					InsuranceRate = insuranceRate * 100,
					FactureCost = client.FactureCost,
					FactureCostEx = client.FactureCostEx,
					TransitCost = client.TransitCost,
					PickupCost = client.PickupCost,
					TariffPerKg = client.TariffPerKg,
					ScotchCostEdited = client.ScotchCostEdited,
                    Comments = client.Comments
				});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Create(
			long clientId,
			ApplicationAdminModel model,
			[Bind(Prefix = "Transit")] TransitEditModel transitModel)
		{
			var client = _clients.Get(clientId);

			if(!ModelState.IsValid)
			{
				BindBag(null, client);

				return View(model);
			}

			try
			{
				_manager.Add(model, transitModel, clientId);
			}
			catch(DublicateException ex)
			{
				ModelState.AddModelError("DublicateException", ex.ToString());

				BindBag(null, client);

				return View(model);
			}

			return RedirectToAction(MVC.ApplicationList.Index());
		}

		#endregion
	}
}