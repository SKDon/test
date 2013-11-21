using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Controllers
{
	public partial class StateController : Controller
	{
		private readonly IStateRepository _states;
		private readonly IEmailTemplateRepository _templates;
		private readonly IIdentityService _identity;

		public StateController(
			IStateRepository states,
			IEmailTemplateRepository templates,
			IIdentityService identity)
		{
			_states = states;
			_templates = templates;
			_identity = identity;
		}

		[Access(RoleType.Admin)]
		public virtual ActionResult Index()
		{
			return View();
		}

		[HttpPost, Access(RoleType.Admin),
		 OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List()
		{
			var states = _states.All();

			return Json(states);
		}

		[Access(RoleType.Admin)]
		public virtual ViewResult Create()
		{
			return View();
		}

		[Access(RoleType.Admin)]
		public virtual ViewResult Edit(long id)
		{
			var data = _states.Get(id).Single().Value;

			var template = _templates.GetByStateId(id);

			var localization = template.Localizations.Single(x => x.TwoLetterISOLanguageName == _identity.TwoLetterISOLanguageName);

			BindLanguageList();

			return View(new StateModel
			{
				Name = data.Name,
				Language = _identity.TwoLetterISOLanguageName,
				LocalizedName = data.Localization[_identity.TwoLetterISOLanguageName],
				Subject = localization.Subject,
				Body = localization.Body,
				Position = data.Position,
				EnableEmailSend = template.EnableEmailSend
			});
		}

		private void BindLanguageList()
		{
			ViewBag.Languages = new Dictionary<string, string>
			{
				{TwoLetterISOLanguageName.English, LanguageName.English},
				{TwoLetterISOLanguageName.Russian, LanguageName.Russian},
				{TwoLetterISOLanguageName.Italian, LanguageName.Italian},
			};
		}
	}
}
