using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
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
	[Access(RoleType.Client)]
	public partial class ClientApplicationController : Controller
	{
		private readonly IApplicationRepository _applications;
		private readonly IClientRepository _clients;
		private readonly ICountryRepository _countries;
		private readonly IIdentityService _identity;
		private readonly IClientApplicationManager _manager;

		public ClientApplicationController(
			IClientApplicationManager manager,
			ICountryRepository countries,
			IIdentityService identity,
			IClientRepository clients,
			IApplicationRepository applications)
		{
			_manager = manager;
			_countries = countries;
			_identity = identity;
			_clients = clients;
			_applications = applications;
		}

		#region Edit

		[HttpGet]
		public virtual ViewResult Edit(long id)
		{
			var application = _manager.Get(id);

			BindBag(id, GetClientId());

			return View(application);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Edit(long id, ApplicationClientModel model,
			[Bind(Prefix = "Transit")] TransitEditModel transitModel)
		{
			if(!ModelState.IsValid)
			{
				BindBag(id, GetClientId());

				return View(model);
			}

			_manager.Update(id, model, transitModel);

			return RedirectToAction(MVC.ClientApplication.Edit(id));
		}

		#endregion

		#region Create

		[HttpGet]
		public virtual ViewResult Create()
		{
			BindBag(null, GetClientId());

			return View(new ApplicationClientModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Create(ApplicationClientModel model,
			[Bind(Prefix = "Transit")] TransitEditModel transitModel)
		{
			var clientId = GetClientId();

			if(!ModelState.IsValid)
			{
				BindBag(null, clientId);

				return View(model);
			}

			try
			{
				_manager.Add(model, transitModel, clientId);
			}
			catch(DublicateException ex)
			{
				ModelState.AddModelError("DublicateException", ex.ToString());

				return View(model);
			}

			return RedirectToAction(MVC.ApplicationList.Index());
		}

		#endregion

		private void BindBag(long? applicationId, long clientId)
		{
			ViewBag.ClientId = clientId;

			ViewBag.ApplicationId = applicationId;

			if(applicationId.HasValue)
			{
				var data = _applications.Get(applicationId.Value);

				ViewBag.ApplicationNumber = data.DisplayNumber;
			}

			ViewBag.Countries = _countries.All(_identity.Language).ToDictionary(x => x.Id, x => x.Name);
		}

		private long GetClientId()
		{
			return _clients.GetByUserId(_identity.Id).ClientId;
		}
	}
}