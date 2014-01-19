using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Controllers.Application
{
	public partial class SenderApplicationController : Controller
	{
		private readonly IApplicationSenderManager _applicationSenderManager;
		private readonly IClientRepository _clientRepository;
		private readonly IIdentityService _identity;
		private readonly ICountryRepository _countries;
		private readonly ISenderRepository _senders;

		public SenderApplicationController(
			IApplicationSenderManager applicationSenderManager,
			IClientRepository clientRepository,
			IIdentityService identity,
			ICountryRepository countries,
			ISenderRepository senders)
		{
			_applicationSenderManager = applicationSenderManager;
			_clientRepository = clientRepository;
			_identity = identity;
			_countries = countries;
			_senders = senders;
		}

		[HttpGet, Access(RoleType.Sender)]
		public virtual ViewResult Create(long id)
		{
			var clientId = id;

			BindBag(clientId);

			return View(new ApplicationSenderModel());
		}

		[HttpPost, Access(RoleType.Sender)]
		public virtual ActionResult Create(long id, ApplicationSenderModel model)
		{
			var clientId = id;

			if(!ModelState.IsValid)
			{
				BindBag(clientId);

				return View(model);
			}

			Debug.Assert(_identity.Id != null);

			var senderId = _senders.GetByUserId(_identity.Id.Value);

			if(!senderId.HasValue)
			{
				throw new EntityNotFoundException("Current user is not sender.");
			}

			_applicationSenderManager.Add(model, clientId, senderId.Value);

			return RedirectToAction(MVC.ApplicationList.Index());
		}

		[HttpGet, Access(RoleType.Sender)]
		public virtual ViewResult Edit(long id)
		{
			var model = _applicationSenderManager.Get(id);

			BindBag(id, model.Count);

			return View(model);
		}

		[HttpPost, Access(RoleType.Sender)]
		public virtual ActionResult Edit(long id, ApplicationSenderModel model)
		{
			if(!ModelState.IsValid)
			{
				BindBag(id, model.Count);

				return View(model);
			}

			_applicationSenderManager.Update(id, model);

			return RedirectToAction(MVC.SenderApplication.Edit(id));
		}

		private void BindBag(long applicationId, int? count)
		{
			var nic = _clientRepository.GetNicByApplications(applicationId).First();
			ViewBag.Nic = nic;
			ViewBag.ApplicationId = applicationId;
			ViewBag.ApplicationNumber = ApplicationHelper.GetDisplayNumber(applicationId, count);
			ViewBag.Countries = _countries.All(_identity.Language).ToDictionary(x => x.Id, x => x.Name);
		}

		private void BindBag(long clientId)
		{
			var clientData = _clientRepository.Get(clientId);
			ViewBag.Nic = clientData.Nic;
			ViewBag.Countries = _countries.All(_identity.Language).ToDictionary(x => x.Id, x => x.Name);
		}
	}
}