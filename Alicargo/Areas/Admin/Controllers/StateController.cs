using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Alicargo.Areas.Admin.Models.State;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Contracts.State;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services;
using Resources;

namespace Alicargo.Areas.Admin.Controllers
{
	[Access(RoleType.Admin, RoleType.Manager)]
	public partial class StateController : Controller
	{
		private readonly IIdentityService _identity;
		private readonly IStateRepository _states;
		private readonly ITemplateRepository _templates;

		public StateController(
			IStateRepository states,
			ITemplateRepository templates,
			IIdentityService identity)
		{
			_states = states;
			_templates = templates;
			_identity = identity;
		}

		[HttpGet]
		public virtual ViewResult Create()
		{
			return View();
		}

		[HttpPost]
		public virtual ActionResult Create(StateCreateModel model)
		{
			if(!ModelState.IsValid)
			{
				BindLanguageList();

				return View(model);
			}

			var language = _identity.Language;

			var id = _states.Add(
				new StateEditData
				{
					LocalizedName = model.Name,
					Name = model.Name,
					Position = model.Position,
					Language = language
				});

			return RedirectToAction(MVC.Admin.State.Edit(id, language));
		}

		[HttpPost]
		public virtual ActionResult Delete(long id)
		{
			try
			{
				_states.Delete(id);
			}
			catch(DeleteConflictedWithConstraintException)
			{
				return Content(Validation.StateIsUsed);
			}

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[HttpGet]
		public virtual ViewResult Edit(long id, string lang)
		{
			BindLanguageList();

			var model = GetStateModel(id, lang ?? _identity.Language);

			return View(model);
		}

		[HttpPost]
		public virtual ActionResult Edit(StateEditModel model)
		{
			if(!ModelState.IsValid)
			{
				BindLanguageList();

				return View(model);
			}

			_states.Update(model.Id,
				new StateEditData
				{
					LocalizedName = model.LocalizedName,
					Name = model.Name,
					Position = model.Position,
					Language = model.Language
				});

			return RedirectToAction(MVC.Admin.State.Edit(model.Id, model.Language));
		}

		public virtual ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List()
		{
			var states = _states.Get(_identity.Language).Select(x => new
			{
				Id = x.Key,
				Name = x.Value.LocalizedName,
				Deletable = !x.Value.IsSystem
			}).ToArray();

			return Json(states);
		}

		private void BindLanguageList()
		{
			ViewBag.Languages = new Dictionary<string, string>
			{
				{ TwoLetterISOLanguageName.English, LanguageName.English },
				{ TwoLetterISOLanguageName.Russian, LanguageName.Russian },
				{ TwoLetterISOLanguageName.Italian, LanguageName.Italian },
			};
		}

		private StateEditModel GetStateModel(long stateId, string language)
		{
			var state = _states.Get(language, stateId).Single().Value;

			var commonData = _templates.GetByEventType(EventType.ApplicationSetState);

			return new StateEditModel
			{
				Id = stateId,
				Name = state.Name,
				Language = language,
				LocalizedName = state.LocalizedName,
				Position = state.Position,
				EnableEmailSend = commonData != null && commonData.EnableEmailSend
			};
		}
	}
}