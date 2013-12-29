using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.State;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.State;
using Resources;

namespace Alicargo.Controllers
{
	public partial class StateController : Controller
	{
		private readonly IIdentityService _identity;
		private readonly IStateRepository _states;
		private readonly IEmailTemplateRepository _templates;

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

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Create()
		{
			return View();
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual ActionResult Create(StateCreateModel model)
		{
			if (!ModelState.IsValid)
			{
				BindLanguageList();

				return View(model);
			}

			var language = _identity.Language;

			var id = _states.Add(language, new StateData
			{
				LocalizedName = model.Name,
				Name = model.Name,
				Position = model.Position
			});

			return RedirectToAction(MVC.State.Edit(id, language));
		}

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Edit(long id, string lang)
		{
			BindLanguageList();

			var model = GetStateModel(id, lang ?? _identity.Language);

			return View(model);
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual ActionResult Delete(long id)
		{
			try
			{
				_states.Delete(id);
			}
			catch (DeleteConflictedWithConstraintException)
			{
				return Content(Validation.StateIsUsed);
			}

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual ActionResult Edit(StateEditModel model)
		{
			if (!ModelState.IsValid)
			{
				BindLanguageList();

				return View(model);
			}

			_states.Update(model.Id, model.Language, new StateData
			{
				LocalizedName = model.LocalizedName,
				Name = model.Name,
				Position = model.Position
			});

			_templates.SetForState(model.Id, model.Language, model.EnableEmailSend, model.UseEventTemplate,
				new EmailTemplateLocalizationData
				{
					Body = model.Body,
					IsBodyHtml = false,
					Subject = model.Subject
				});

			return RedirectToAction(MVC.State.Edit(model.Id, model.Language));
		}

		private StateEditModel GetStateModel(long id, string language)
		{
			var state = _states.Get(language, id).Single().Value;

			var commonData = _templates.GetByStateId(id);

			var localization = commonData != null ? _templates.GetLocalization(commonData.EmailTemplateId, language) : null;

			return new StateEditModel
			{
				Id = id,
				Name = state.Name,
				Language = language,
				LocalizedName = state.LocalizedName,
				Subject = localization != null ? localization.Subject : null,
				Body = localization != null ? localization.Body : null,
				Position = state.Position,
				EnableEmailSend = commonData != null && commonData.EnableEmailSend,
				UseEventTemplate = commonData != null && commonData.UseEventTemplate
			};
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