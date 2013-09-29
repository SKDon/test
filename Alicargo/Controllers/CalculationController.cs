using System.Net;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;

namespace Alicargo.Controllers
{
	public partial class CalculationController : Controller
	{
		private readonly IApplicationManager _applicationManager;
		private readonly IAwbUpdateManager _awbUpdateManager;
		private readonly ICalculationPresenter _presenter;
		private readonly ICalculationService _calculation;

		public CalculationController(
			IAwbUpdateManager awbUpdateManager,
			ICalculationPresenter presenter,
			ICalculationService calculation,
			IApplicationManager applicationManager)
		{
			_awbUpdateManager = awbUpdateManager;
			_presenter = presenter;
			_calculation = calculation;
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
			var data = _presenter.List(take, skip);

			return Json(data);
		}

		[Access(RoleType.Admin), HttpPost]
		public virtual JsonResult Row(long id)
		{
			var data = _presenter.Row(id);

			return Json(data);
		}

		[Access(RoleType.Admin), HttpPost]
		public virtual HttpStatusCodeResult SetTariffPerKg(long id, decimal? tariffPerKg)
		{
			_applicationManager.SetTariffPerKg(id, tariffPerKg);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[Access(RoleType.Admin), HttpPost]
		public virtual HttpStatusCodeResult Calculate(long id)
		{
			_calculation.Calculate(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[Access(RoleType.Admin), HttpPost]
		public virtual HttpStatusCodeResult SetSenderRate(long id, decimal? senderRate)
		{
			_applicationManager.SetSenderRate(id, senderRate);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[Access(RoleType.Admin), HttpPost]
		public virtual HttpStatusCodeResult SetScotchCostEdited(long id, decimal? scotchCost)
		{
			_applicationManager.SetScotchCostEdited(id, scotchCost);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[Access(RoleType.Admin), HttpPost]
		public virtual HttpStatusCodeResult SetFactureCostEdited(long id, decimal? factureCost)
		{
			_applicationManager.SetFactureCostEdited(id, factureCost);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[Access(RoleType.Admin), HttpPost]
		public virtual HttpStatusCodeResult SetWithdrawCostEdited(long id, decimal? withdrawCost)
		{
			_applicationManager.SetWithdrawCostEdited(id, withdrawCost);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[Access(RoleType.Admin), HttpPost]
		public virtual HttpStatusCodeResult SetAdditionalCost(long awbId, decimal? additionalCost)
		{
			_awbUpdateManager.SetAdditionalCost(awbId, additionalCost);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}
	}
}