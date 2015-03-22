using System.Web.Mvc;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.Services.Excel;

namespace Alicargo.Controllers.Calculation
{
	[Access(RoleType.Admin)]
	public partial class CalculationController : Controller
	{
		private readonly IAdminApplicationManager _applications;
		private readonly IAwbUpdateManager _awbUpdater;
		private readonly IClientBalanceRepository _balances;
		private readonly ICalculationService _calculation;
		private readonly IIdentityService _identity;
		private readonly IAdminCalculationPresenter _presenter;

		public CalculationController(
			IAwbUpdateManager awbUpdater,
			IAdminCalculationPresenter presenter,
			ICalculationService calculation,
			IIdentityService identity,
			IAdminApplicationManager applications,
			IClientBalanceRepository balances)
		{
			_awbUpdater = awbUpdater;
			_presenter = presenter;
			_calculation = calculation;
			_identity = identity;
			_applications = applications;
			_balances = balances;
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult Calculate(long id, long awbId)
		{
			_calculation.Calculate(id);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[HttpGet]
		public virtual FileResult Excel()
		{
			var data = _presenter.List(int.MaxValue, 0);

			var excel = new ExcelAdminCalculation();

			var stream = excel.Get(data, _identity.Language);

			return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "calculation.xlsx");
		}

		[HttpGet]
		public virtual ActionResult Index()
		{
			ViewBag.TotalBalance = _balances.SumBalance();

			return View();
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, long skip)
		{
			var data = _presenter.List(take, skip);

			return Json(data);
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult RemoveCalculatation(long id, long awbId)
		{
			_calculation.CancelCalculatation(id);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetAdditionalCost(long awbId, decimal? additionalCost)
		{
			_awbUpdater.SetAdditionalCost(awbId, additionalCost);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetClass(long id, long awbId, int? classId)
		{
			_applications.SetClass(id, (ClassType?)classId);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetCount(long id, long awbId, int? value)
		{
			_applications.SetCount(id, value);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetFactureCostEdited(long id, long awbId, decimal? factureCost)
		{
			_applications.SetFactureCostEdited(id, factureCost);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetFactureCostExEdited(long id, long awbId, decimal? factureCostEx)
		{
			_applications.SetFactureCostExEdited(id, factureCostEx);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetInsuranceCost(long id, long awbId, float? value)
		{
			_applications.SetInsuranceCost(id, value);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetPickupCostEdited(long id, long awbId, decimal? pickupCost)
		{
			_applications.SetPickupCostEdited(id, pickupCost);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetProfit(long id, long awbId, decimal? value)
		{
			_applications.SetProfit(id, value);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetScotchCostEdited(long id, long awbId, decimal? scotchCost)
		{
			_applications.SetScotchCostEdited(id, scotchCost);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetSenderRate(long id, long awbId, decimal? senderRate)
		{
			_applications.SetSenderRate(id, senderRate);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetTariffPerKg(long id, long awbId, decimal? tariffPerKg)
		{
			_applications.SetTariffPerKg(id, tariffPerKg);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetTotalTariffCost(long id, long awbId, decimal? value)
		{
			_applications.SetTotalTariffCost(id, value);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetTransitCostEdited(long id, long awbId, decimal? transitCost)
		{
			_applications.SetTransitCostEdited(id, transitCost);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetWeight(long id, long awbId, float? value)
		{
			_applications.SetWeight(id, value);

			var data = _presenter.Row(awbId);

			return Json(data);
		}
	}
}