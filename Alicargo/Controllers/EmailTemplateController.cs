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
using Alicargo.ViewModels.EmailTemplate;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Controllers
{
	public partial class EmailTemplateController : Controller
	{
		private readonly IIdentityService _identity;
		private readonly IEmailTemplateRepository _templates;

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
			var dictionary = new Dictionary<ApplicationEventType, string>
			{
				{ApplicationEventType.Created, "Созадние заявки"},
				{ApplicationEventType.SetState, "Установка статуса"},
				{ApplicationEventType.CPFileUploaded, "Загружена счет-фактура"},
				{ApplicationEventType.InvoiceFileUploaded, "Загружен инвойс"},
				{ApplicationEventType.PackingFileUploaded, "Загружен пакинг"},
				{ApplicationEventType.SwiftFileUploaded, "Загружен SWIFT"},
				{ApplicationEventType.DeliveryBillFileUploaded, "Загружен Счет за доставку"},
				{ApplicationEventType.Torg12FileUploaded, "Загружен Торг 12"},
				{ApplicationEventType.SetDateOfCargoReceipt, "Установлена дата получения груза"},
				{ApplicationEventType.SetTransitReference, "Установлен транзитный референс"}
			};

			var types = Enum.GetValues(typeof(ApplicationEventType))
				.Cast<ApplicationEventType>()
				.Select(x => new
				{
					Id = (int)x,
					Name = dictionary[x]
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
		public virtual ActionResult Edit(ApplicationEventTemplateModel model)
		{
			if (!ModelState.IsValid)
			{
				BindLanguageList();

				return View(model);
			}

			var roles = model.Settings.GetSettings();

			_templates.SetForApplicationEvent(
				model.EventType, model.Language, model.EnableEmailSend, roles,
				new EmailTemplateLocalizationData
				{
					Body = model.Body,
					IsBodyHtml = false,
					Subject = model.Subject
				});

			return RedirectToAction(MVC.EmailTemplate.Edit(model.EventType, model.Language));
		}

		private ApplicationEventTemplateModel GetModel(ApplicationEventType eventType, string language)
		{
			var commonData = _templates.GetByEventType(eventType);

			var localization = commonData != null ? _templates.GetLocalization(commonData.EmailTemplateId, language) : null;

			var recipients = _templates.GetRecipientRoles(eventType);

			return new ApplicationEventTemplateModel
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