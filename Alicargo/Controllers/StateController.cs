using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;

namespace Alicargo.Controllers
{
	public partial class StateController : Controller
	{
		private readonly IStateRepository _states;

		public StateController(IStateRepository states)
		{
			_states = states;
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

		[Access(RoleType.Admin)]
		public virtual ViewResult Create()
		{
			return View();
		}

		[Access(RoleType.Admin)]
		public virtual ViewResult Edit(long id)
		{
			return View();
		}
	}
}
