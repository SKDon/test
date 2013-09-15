using System.Net;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.MvcHelpers;
using Alicargo.Services.Abstract;

namespace Alicargo.Controllers
{
	public partial class CalculationController : Controller
	{
		private readonly IApplicationManager _applicationManager;
		private readonly ICalculationService _calculationService;

		public CalculationController(
			ICalculationService calculationService, 
			IApplicationManager applicationManager)
		{
			_calculationService = calculationService;
			_applicationManager = applicationManager;
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

		[Access(RoleType.Admin), HttpPost]
		public virtual HttpStatusCodeResult SetTariffPerKg(long id, decimal? tariffPerKg)
		{
			_applicationManager.SetTariffPerKg(id, tariffPerKg);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}


		[Access(RoleType.Admin, RoleType.Sender), HttpPost]
		public virtual HttpStatusCodeResult SetScotchCostEdited(long id, decimal? scotchCost)
		{
			_applicationManager.SetScotchCostEdited(id, scotchCost);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[Access(RoleType.Admin, RoleType.Sender), HttpPost]
		public virtual HttpStatusCodeResult SetFactureCostEdited(long id, decimal? factureCost)
		{
			_applicationManager.SetFactureCostEdited(id, factureCost);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[Access(RoleType.Admin, RoleType.Sender), HttpPost]
		public virtual HttpStatusCodeResult SetWithdrawCostEdited(long id, decimal? withdrawCost)
		{
			_applicationManager.SetWithdrawCostEdited(id, withdrawCost);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}		
	}
}