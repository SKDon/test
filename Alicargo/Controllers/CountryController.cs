using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Controllers
{
	public partial class CountryController : Controller
	{
		private readonly ICountryService _countries;
		private readonly IIdentityService _identity;

		public CountryController(
			ICountryService countries,
			IIdentityService identity)
		{
			_countries = countries;
			_identity = identity;
		}

		[Access(RoleType.Admin)]
		public virtual ActionResult Index()
		{
			return View();
		}

		[HttpPost, Access(RoleType.Admin), OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, int skip)
		{
			var list = _countries.List(take, skip, _identity.Language);

			return Json(list);
		}

		[HttpGet, Access(RoleType.Admin)]
		public virtual ViewResult Edit(long id)
		{
			var model = _countries.Get(id);

			return View(model);
		}

		[HttpGet, Access(RoleType.Admin)]
		public virtual ViewResult Create()
		{
			return View();
		}

		[HttpPost, Access(RoleType.Admin)]
		public virtual ActionResult Edit(long id, CountryEditModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_countries.Edit(id, model);

			return RedirectToAction(MVC.Country.Edit(id));
		}

		[HttpPost, Access(RoleType.Admin)]
		public virtual ActionResult Create(CountryEditModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var id = _countries.Add(model);

			return RedirectToAction(MVC.Country.Edit(id));
		}
	}
}
