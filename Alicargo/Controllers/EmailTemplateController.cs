using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.EmailTemplate;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Controllers
{
	public partial class EmailTemplateController : Controller
	{
		private readonly IIdentityService _identity;
		private readonly ITemplateRepository _templates;
		private readonly IEventEmailRecipient _recipients;

		public EmailTemplateController(
			ITemplateRepository templates,
			IEventEmailRecipient recipients,
			IIdentityService identity)
		{
			_templates = templates;
			_recipients = recipients;
			_identity = identity;
		}

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Index()
		{
			return View();
		}

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Help()
		{
			return View();
		}

		[HttpPost, Access(RoleType.Admin),
		 OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List()
		{
			var types = Enum.GetValues(typeof(EventType))
				.Cast<EventType>()
				.Select(x => new
				{
					Id = (int)x,
					Name = DataAccess.Contracts.Resources.EventType.ResourceManager.GetString(x.ToString())
				})
				.ToArray();

			return Json(types);
		}

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Edit(EventType id, string lang)
		{
			BindLanguageList();

			var model = GetModel(id, lang ?? _identity.Language);

			return View(model);
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual ActionResult Edit(EventTemplateModel model)
		{
			if (!ModelState.IsValid)
			{
				BindLanguageList();

				return View(model);
			}

			var roles = model.Settings.GetSettings();

			_recipients.SetForEvent(
				model.EventType, model.Language, model.EnableEmailSend, roles,
				new EmailTemplateLocalizationData
				{
					Body = model.Body,
					IsBodyHtml = false,
					Subject = model.Subject
				});

			return RedirectToAction(MVC.EmailTemplate.Edit(model.EventType, model.Language));
		}

		private EventTemplateModel GetModel(EventType eventType, string language)
		{
			var commonData = _templates.GetByEventType(eventType);

			var localization = commonData != null ? _templates.GetLocalization(commonData.EmailTemplateId, language) : null;

			var recipients = _recipients.GetRecipientRoles(eventType);

			return new EventTemplateModel
			{
				Body = localization != null ? localization.Body : null,
				Subject = localization != null ? localization.Subject : null,
				EnableEmailSend = commonData != null && commonData.EnableEmailSend,
				Language = language,
				EventType = eventType,
				Settings = EmailTemplateSettingsModelHelper.GetModel(recipients)
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