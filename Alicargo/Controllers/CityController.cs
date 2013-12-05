using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;

namespace Alicargo.Controllers
{
	public partial class CityController : Controller
	{
		private readonly ICityService _cities;
		private readonly IIdentityService _identity;

		public CityController(ICityService cities, IIdentityService identity)
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
			var list = _cities.List(take, skip, _identity.TwoLetterISOLanguageName);

			return Json(list);
		}

		[HttpGet, Access(RoleType.Admin)]
		public virtual ViewResult Edit(long id)
		{
			return View();
		}

		[HttpGet, Access(RoleType.Admin)]
		public virtual ViewResult Create()
		{
			return View();
		}
	}
}
