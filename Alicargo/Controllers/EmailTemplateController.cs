using System;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Localization;
using Alicargo.MvcHelpers.Filters;
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

		public virtual ViewResult Edit(ApplicationEventType id, string lang)
		{
			var model = GetModel(id, lang ?? _identity.TwoLetterISOLanguageName);

			return View(model);
		}

		private EmailTemplateModel GetModel(ApplicationEventType id, string language)
		{
			var template = _templates.GetByApplicationEvent(id, language);

			var localization = template.Localization;

			return new EmailTemplateModel
			{
				Body = localization != null ? localization.Body : null,
				Subject = localization != null ? localization.Subject : null,
				EnableEmailSend = template.EnableEmailSend,
				Language = language
			};
		}
	}
}