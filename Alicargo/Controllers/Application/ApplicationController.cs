using System.Linq;
using System.Net;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Helpers;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Controllers.Application
{
	public partial class ApplicationController : Controller
	{
		private readonly IApplicationManager _applicationManager;
		private readonly IApplicationPresenter _applicationPresenter;
		private readonly IApplicationRepository _applications;
		private readonly IClientRepository _clients;
		private readonly IUserRepository _users;

		public ApplicationController(
			IApplicationManager applicationManager,
			IApplicationPresenter applicationPresenter,
			IApplicationRepository applications,
			IClientRepository clients,
			IUserRepository users)
		{
			_applicationManager = applicationManager;
			_applicationPresenter = applicationPresenter;
			_applications = applications;
			_clients = clients;
			_users = users;
		}

		[ChildActionOnly]
		public virtual PartialViewResult Details(long id)
		{
			var application = _applicationPresenter.GetDetails(id);

			return PartialView(application);
		}		

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual HttpStatusCodeResult Delete(long id)
		{
			_applicationManager.Delete(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		private void BindBag(long clientId, long? applicationId, int? count = 0)
		{
			var client = _clients.Get(clientId);

			ViewBag.ClientNic = client.Nic;

			ViewBag.ClientId = client.Id;

			ViewBag.ApplicationId = applicationId;

			if (applicationId.HasValue)
			{
				ViewBag.ApplicationNumber = ApplicationHelper.GetDisplayNumber(applicationId.Value, count);
			}

			ViewBag.Countries = _applicationPresenter.GetLocalizedCountries();

			ViewBag.Senders = _users.GetByRole(RoleType.Sender).OrderBy(x => x.Name).ToDictionary(x => x.EntityId, x => x.Name);
		}

		#region Edit

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Edit(long id)
		{
			var application = _applicationPresenter.Get(id);

			var clientId = _applications.GetClientId(id);

			BindBag(clientId, id, application.Count);

			return View(application);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Access(RoleType.Admin)]
		public virtual ActionResult Edit(long id, ApplicationAdminModel model, CarrierSelectModel carrierModel,
			[Bind(Prefix = "Transit")] TransitEditModel transitModel)
		{
			if (!ModelState.IsValid)
			{
				var clientId = _applications.GetClientId(id);
				BindBag(clientId, id, model.Count);

				return View(model);
			}

			_applicationManager.Update(id, model, carrierModel, transitModel);

			return RedirectToAction(MVC.Application.Edit(id));
		}

		#endregion

		#region Create

		[Access(RoleType.Admin)]
		public virtual ViewResult Create(long clientId)
		{
			BindBag(clientId, null);

			return View(new ApplicationAdminModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Access(RoleType.Admin)]
		public virtual ActionResult Create(long clientId, ApplicationAdminModel model, CarrierSelectModel carrierModel,
			[Bind(Prefix = "Transit")] TransitEditModel transitModel)
		{
			if (!ModelState.IsValid)
			{
				BindBag(clientId, null);

				return View(model);
			}

			try
			{
				_applicationManager.Add(model, carrierModel, transitModel, clientId);
			}
			catch (DublicateException ex)
			{
				ModelState.AddModelError("DublicateException", ex.ToString());

				return View(model);
			}

			return RedirectToAction(MVC.ApplicationList.Index());
		}

		#endregion
	}
}