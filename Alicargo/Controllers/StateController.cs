using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Contracts.State;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services;
using Alicargo.ViewModels.State;
using Resources;

namespace Alicargo.Controllers
{
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
		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ViewResult Create()
		{
			return View();
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ActionResult Create(StateCreateModel model)
		{
			if(!ModelState.IsValid)
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

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Manager)]
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
		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ViewResult Edit(long id, string lang)
		{
			BindLanguageList();

			var model = GetStateModel(id, lang ?? _identity.Language);

			return View(model);
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ActionResult Edit(StateEditModel model)
		{
			if(!ModelState.IsValid)
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

			return RedirectToAction(MVC.State.Edit(model.Id, model.Language));
		}

		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Manager)]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List()
		{
			var states = _states.Get(_identity.Language).Select(x => new
			{
				Id = x.Key,
				Name = x.Value.LocalizedName
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