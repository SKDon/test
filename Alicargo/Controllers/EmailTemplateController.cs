using System;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Localization;
using Alicargo.MvcHelpers.Filters;

namespace Alicargo.Controllers
{
	public class EmailTemplateController : Controller
	{
		[HttpGet]
		[Access(RoleType.Admin)]
		public ViewResult Index()
		{
			return View();
		}

		[HttpPost, Access(RoleType.Admin),
		 OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public JsonResult List()
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
	}
}