using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Controllers.Application
{
	[Access(RoleType.Sender)]
	public partial class SenderApplicationController : Controller
	{
		private readonly IApplicationRepository _applications;
		private readonly IClientRepository _clients;
		private readonly ICountryRepository _countries;
		private readonly IIdentityService _identity;
		private readonly ISenderApplicationManager _manager;
		private readonly ISenderRepository _senders;

		public SenderApplicationController(
			ISenderApplicationManager manager,
			IClientRepository clients,
			IIdentityService identity,
			ICountryRepository countries,
			ISenderRepository senders,
			IApplicationRepository applications)
		{
			_manager = manager;
			_clients = clients;
			_identity = identity;
			_countries = countries;
			_senders = senders;
			_applications = applications;
		}

		[HttpGet]
		public virtual ViewResult Create(long id)
		{
			var clientId = id;

			var client = _clients.Get(clientId);

			BindBagWithClient(client);

			return View(
				new ApplicationSenderModel
				{
					FactureCost = client.FactureCost,
					FactureCostEx = client.FactureCostEx,
					TransitCost = client.TransitCost,
					PickupCost = client.PickupCost,
					Comments = client.Comments
				});
		}

		[HttpPost]
		public virtual ActionResult Create(long id, ApplicationSenderModel model)
		{
			var clientId = id;

			var client = _clients.Get(clientId);

			if(!ModelState.IsValid)
			{
				BindBagWithClient(client);

				return View(model);
			}

			var senderId = _senders.GetByUserId(_identity.Id);

			if(!senderId.HasValue)
			{
				throw new EntityNotFoundException("Current user is not sender.");
			}

			_manager.Add(model, client, senderId.Value);

			return RedirectToAction(MVC.ApplicationList.Index());
		}

		[HttpGet]
		public virtual ViewResult Edit(long id)
		{
			var model = _manager.Get(id);

			BindBag(id);

			return View(model);
		}

		[HttpPost]
		public virtual ActionResult Edit(long id, ApplicationSenderModel model)
		{
			if(!ModelState.IsValid)
			{
				BindBag(id);

				return View(model);
			}

			_manager.Update(id, model);

			return RedirectToAction(MVC.SenderApplication.Edit(id));
		}

		private void BindBag(long applicationId)
		{
			var data = _applications.Get(applicationId);
			ViewBag.ApplicationId = applicationId;
			ViewBag.ApplicationNumber = data.DisplayNumber;

			ViewBag.Nic = data.ClientNic;
			BindCountries();
		}

		private void BindBagWithClient(ClientEditData client)
		{
			ViewBag.Nic = client.Nic;
			BindCountries();
		}

		private void BindCountries()
		{
			var senderId = _senders.GetByUserId(_identity.Id);

			Debug.Assert(senderId != null);

			var countries = _senders.GetCountries(senderId.Value);

			ViewBag.Countries = _countries.All(_identity.Language)
				.Where(x => countries.Contains(x.Id))
				.ToDictionary(x => x.Id, x => x.Name);
		}
	}
}