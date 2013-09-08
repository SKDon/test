using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Helpers;
using Alicargo.Services.Abstract;

namespace Alicargo.Controllers
{
	public partial class CalculationController : Controller
	{
		private readonly ICalculationService _calculationService;

		public CalculationController(ICalculationService calculationService)
		{
			_calculationService = calculationService;
		}

		[Access(RoleType.Admin), HttpGet]
		public virtual ActionResult Index()
		{
			return View();
		}

		[Access(RoleType.Admin), HttpPost]
		public virtual JsonResult List(int take, long skip)
		{
			var data = _calculationService.List(take, skip);

			return Json(data);
		}
	}
}