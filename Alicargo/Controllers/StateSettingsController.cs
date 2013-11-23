using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;
using Alicargo.ViewModels.State;

namespace Alicargo.Controllers
{
	public partial class StateSettingsController : Controller
	{
		private readonly IStateSettingsRepository _settings;

		public StateSettingsController(IStateSettingsRepository settings)
		{
			_settings = settings;
		}

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Index(long id)
		{
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
			return RedirectToAction(MVC.StateSettings.Index(id));
		}
	}
}