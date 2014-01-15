using System.Web.Mvc;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Controllers
{
	public partial class CityController : Controller
	{
		private readonly ICityService _cities;
		private readonly IIdentityService _identity;

		public CityController(
			ICityService cities,
			IIdentityService identity)
		{
			_cities = cities;
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
			var list = _cities.List(take, skip, _identity.Language);

			return Json(list);
		}

		[HttpGet, Access(RoleType.Admin)]
		public virtual ViewResult Edit(long id)
		{
			var model = _cities.Get(id);

			return View(model);
		}

		[HttpGet, Access(RoleType.Admin)]
		public virtual ViewResult Create()
		{
			return View();
		}

		[HttpPost, Access(RoleType.Admin)]
		public virtual ActionResult Edit(long id, CityEditModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_cities.Edit(id, model);

			return RedirectToAction(MVC.City.Edit(id));
		}

		[HttpPost, Access(RoleType.Admin)]
		public virtual ActionResult Create(CityEditModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var id = _cities.Add(model);

			return RedirectToAction(MVC.City.Edit(id));
		}
	}
}
