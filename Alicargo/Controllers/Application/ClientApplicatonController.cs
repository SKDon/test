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
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Controllers.Application
{
	public partial class ClientApplicationController : Controller
	{
		private readonly IClientRepository _clients;
		private readonly ICountryRepository _countries;
		private readonly IIdentityService _identity;
		private readonly IClientApplicationManager _manager;

		public ClientApplicationController(
			IClientApplicationManager manager,
			ICountryRepository countries,
			IIdentityService identity,
			IClientRepository clients)
		{
			_manager = manager;
			_countries = countries;
			_identity = identity;
			_clients = clients;
		}

		#region Edit

		[HttpGet]
		[Access(RoleType.Client)]
		public virtual ViewResult Edit(long id)
		{
			var application = _manager.Get(id);

			BindBag(id, GetClientId(), application.Count);

			return View(application);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Access(RoleType.Client)]
		public virtual ActionResult Edit(long id, ApplicationClientModel model, CarrierSelectModel carrierModel,
			[Bind(Prefix = "Transit")] TransitEditModel transitModel)
		{
			if (!ModelState.IsValid)
			{
				BindBag(id, GetClientId(), model.Count);

				return View(model);
			}

			_manager.Update(id, model, carrierModel, transitModel);

			return RedirectToAction(MVC.ClientApplication.Edit(id));
		}

		#endregion

		#region Create

		[Access(RoleType.Client)]
		public virtual ViewResult Create()
		{
			BindBag(null, GetClientId());

			return View(new ApplicationClientModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Access(RoleType.Client)]
		public virtual ActionResult Create(ApplicationClientModel model, CarrierSelectModel carrierModel,
			[Bind(Prefix = "Transit")] TransitEditModel transitModel)
		{
			var clientId = GetClientId();

			if (!ModelState.IsValid)
			{
				BindBag(null, clientId);

				return View(model);
			}

			try
			{
				_manager.Add(model, carrierModel, transitModel, clientId);
			}
			catch (DublicateException ex)
			{
				ModelState.AddModelError("DublicateException", ex.ToString());

				return View(model);
			}

			return RedirectToAction(MVC.ApplicationList.Index());
		}

		#endregion

		private long GetClientId()
		{
			Debug.Assert(_identity.Id != null);

			return _clients.GetByUserId(_identity.Id.Value).ClientId;
		}

		private void BindBag(long? applicationId, long clientId, int? count = 0)
		{
			ViewBag.ClientId = clientId;

			ViewBag.ApplicationId = applicationId;

			if (applicationId.HasValue)
			{
				ViewBag.ApplicationNumber = ApplicationHelper.GetDisplayNumber(applicationId.Value, count);
			}

			ViewBag.Countries = _countries.Get().ToDictionary(x => x.Id, x => x.Name[_identity.Language]);
		}
	}
}