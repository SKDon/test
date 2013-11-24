using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Localization;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Controllers
{
	public partial class EmailTemplateController : Controller
	{
		private readonly IEmailTemplateRepository _templates;
		private readonly IIdentityService _identity;

		public EmailTemplateController(
			IEmailTemplateRepository templates,
			IIdentityService identity)
		{
			_templates = templates;
			_identity = identity;
		}

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Index()
		{
			return View();
		}

		[HttpPost, Access(RoleType.Admin),
		 OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List()
		{
			var types = Enum.GetValues(typeof(ApplicationEventType))
				.Cast<ApplicationEventType>()
				.Select(x => new
				{
					Id = (int)x,
					Name = x.ToLocalString()
				})
				.ToArray();

			return Json(types);
		}

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Edit(ApplicationEventType id, string lang)
		{
			BindLanguageList();

			var model = GetModel(id, lang ?? _identity.TwoLetterISOLanguageName);

			return View(model);
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual ActionResult Edit(EmailTemplateModel model)
		{
			if (!ModelState.IsValid)
			{
				BindLanguageList();

				return View(model);
			}

			_templates.SetForApplicationEvent(model.EventType, model.Language, model.EnableEmailSend, new EmailTemplateLocalizationData
			{
				Body = model.Body,
				IsBodyHtml = false,
				Subject = model.Subject
			});

			return RedirectToAction(MVC.EmailTemplate.Edit(model.EventType, model.Language));
		}

		private EmailTemplateModel GetModel(ApplicationEventType id, string language)
		{
			var template = _templates.GetByApplicationEvent(id, language);

			var localization = template != null ? template.Localization : null;

			return new EmailTemplateModel
			{
				Body = localization != null ? localization.Body : null,
				Subject = localization != null ? localization.Subject : null,
				EnableEmailSend = template != null && template.EnableEmailSend,
				Language = language,
				EventType = id
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