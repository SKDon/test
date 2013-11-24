using System;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Localization;
using Alicargo.MvcHelpers.Filters;

namespace Alicargo.Controllers
{
	public partial class EmailTemplateController : Controller
	{
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

		public virtual ViewResult Edit(ApplicationEventType id)
		{
			return View();
		}
	}
}