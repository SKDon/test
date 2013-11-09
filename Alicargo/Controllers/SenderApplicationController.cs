using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Controllers
{
	public partial class SenderApplicationController : Controller
	{
		private readonly IApplicationPresenter _applicationPresenter;
		private readonly IApplicationSenderManager _applicationSenderManager;
		private readonly IClientRepository _clientRepository;
		private readonly IIdentityService _identity;
		private readonly ISenderRepository _senders;

		public SenderApplicationController(
			IApplicationSenderManager applicationSenderManager,
			IClientRepository clientRepository, 
			IIdentityService identity, 
			ISenderRepository senders,
			IApplicationPresenter applicationPresenter)
		{
			_applicationSenderManager = applicationSenderManager;
			_clientRepository = clientRepository;
			_identity = identity;
			_senders = senders;
			_applicationPresenter = applicationPresenter;
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

			if (!ModelState.IsValid)
			{
				BindBag(clientId);

				return View(model);
			}

			Debug.Assert(_identity.Id != null);

			var senderId = _senders.GetByUserId(_identity.Id.Value);

			if (!senderId.HasValue)
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
			if (!ModelState.IsValid)
			{
				BindBag(id, model.Count);

				return View(model);
			}

			_applicationSenderManager.Update(id, model);

			return RedirectToAction(MVC.SenderApplication.Edit(id));
		}

		private void BindBag(long appId, int? count)
		{
			var nic = _clientRepository.GetNicByApplications(appId).First();
			ViewBag.Nic = nic;
			ViewBag.ApplicationNumber = ApplicationHelper.GetDisplayNumber(appId, count);
			ViewBag.Countries = _applicationPresenter.GetLocalizedCountries();
		}

		private void BindBag(long clientId)
		{
			var clientData = _clientRepository.Get(clientId);
			ViewBag.Nic = clientData.Nic;
			ViewBag.Countries = _applicationPresenter.GetLocalizedCountries();
		}
	}
}