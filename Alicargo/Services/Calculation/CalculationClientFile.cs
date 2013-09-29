using System;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Calculation;
using Alicargo.ViewModels.Helpers;
using Resources;

namespace Alicargo.Services.Calculation
{
	internal sealed class CalculationClientFile : ICalculationClientFile
	{
		private readonly IAwbRepository _awbs;

		public CalculationClientFile(IAwbRepository awbs)
		{
			_awbs = awbs;
		}

		// todo: 2. test
		public string Build(ApplicationData application)
		{
			if (application.AirWaybillId == null)
				throw new InvalidOperationException("For calculation an air waybill should be presented. Applicaiton id: "
													+ application.Id);

			var awb = _awbs.Get(application.AirWaybillId.Value).First();

			var weigth = (decimal)(application.Weigth ?? 0);
			var tariffPerKg = application.TariffPerKg ?? 0;
			var insurance = application.Value / CalculationItem.InsuranceRate;
			var cost = tariffPerKg * weigth;
			var scotch = application.ScotchCostEdited ?? application.ScotchCost ?? 0;
			var facture = application.FactureCostEdited ?? application.FactureCost ?? 0;
			var total = cost + scotch + facture + insurance;

			return string.Format(Mail.Calculation_Body,
								 AwbHelper.GetAirWayBillDisplay(awb),
								 ApplicationModelHelper.GetDisplayNumber(application.Id, application.Count),
								 application.MarkName,
								 weigth.ToString("N2"),
								 tariffPerKg.ToString("N2"),
								 cost.ToString("N2"),
								 scotch.ToString("N2"),
								 insurance.ToString("N2"),
								 facture.ToString("N2"),
								 total.ToString("N2"));
		}
	}
}