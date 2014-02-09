using System;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;
using Alicargo.ViewModels.Helpers;
using Alicargo.ViewModels.State;

namespace Alicargo.Controllers
{
	public partial class StateSettingsController : Controller
	{
		private readonly IIdentityService _identity;
		private readonly IStateSettingsRepository _settings;
		private readonly IStateRepository _states;

		public StateSettingsController(
			IIdentityService identity,
			IStateSettingsRepository settings,
			IStateRepository states)
		{
			_identity = identity;
			_settings = settings;
			_states = states;
		}

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Index(long id)
		{
			var state = _states.Get(_identity.Language, id).First().Value;
			ViewBag.StateName = state.LocalizedName;

			var availabilities = _settings.GetStateAvailabilities().Where(x => x.StateId == id).Select(x => x.Role).ToArray();
			var visibilities = _settings.GetStateVisibilities().Where(x => x.StateId == id).Select(x => x.Role).ToArray();

			var model = new StateSettingsModel
			{
				Availabilities = EmailTemplateSettingsModelHelper.GetModel(availabilities),
				Visibilities = EmailTemplateSettingsModelHelper.GetModel(visibilities)
			};

			return View(model);
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual ActionResult Index(long id, StateSettingsModel model)
		{
			if (!ModelState.IsValid)
			{
				throw new InvalidOperationException("Failed to save state settings. State id: " + id);
			}

			_settings.SetStateAvailabilities(id, model.Availabilities.GetSettings());
			_settings.SetStateVisibilities(id, model.Visibilities.GetSettings());

			return RedirectToAction(MVC.StateSettings.Index(id));
		}
	}
}