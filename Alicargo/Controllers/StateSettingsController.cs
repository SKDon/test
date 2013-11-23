using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
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
			var state = _states.Get(_identity.TwoLetterISOLanguageName, id).First().Value;
			ViewBag.StateName = state.LocalizedName;

			var availabilities = _settings.GetStateAvailabilities().Where(x => x.StateId == id).Select(x => x.Role).ToArray();
			var emailRecipients = _settings.GetStateEmailRecipients().Where(x => x.StateId == id).Select(x => x.Role).ToArray();
			var visibilities = _settings.GetStateVisibilities().Where(x => x.StateId == id).Select(x => x.Role).ToArray();

			var model = new StateSettingsModel
			{
				Availabilities = new StateSettingsModel.Settings(availabilities),
				Recipients = new StateSettingsModel.Settings(emailRecipients),
				Visibilities = new StateSettingsModel.Settings(visibilities)
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

			_settings.SetStateAvailabilities(id, GetSettings(model.Availabilities));
			_settings.SetStateEmailRecipients(id, GetSettings(model.Recipients));
			_settings.SetStateVisibilities(id, GetSettings(model.Visibilities));

			return RedirectToAction(MVC.StateSettings.Index(id));
		}

		private static RoleType[] GetSettings(StateSettingsModel.Settings settings)
		{
			var list = new List<RoleType>(5);
			if (settings.Admin)
			{
				list.Add(RoleType.Admin);
			}
			if (settings.Broker)
			{
				list.Add(RoleType.Broker);
			}
			if (settings.Client)
			{
				list.Add(RoleType.Client);
			}
			if (settings.Forwarder)
			{
				list.Add(RoleType.Forwarder);
			}
			if (settings.Sender)
			{
				list.Add(RoleType.Sender);
			}

			return list.ToArray();
		}
	}
}