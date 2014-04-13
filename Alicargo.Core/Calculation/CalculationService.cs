using System;
using System.Linq;
using System.Web;
using Alicargo.Core.AirWaybill;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Utilities;

namespace Alicargo.Core.Calculation
{
	public sealed class CalculationService : ICalculationService
	{
		private readonly IApplicationRepository _applications;
		private readonly IAwbRepository _awbs;
		private readonly ICalculationRepository _calculations;
		private readonly ISenderRepository _senders;

		public CalculationService(
			ICalculationRepository calculations,
			ISenderRepository senders,
			IAwbRepository awbs,
			IApplicationRepository applications)
		{
			_calculations = calculations;
			_senders = senders;
			_awbs = awbs;
			_applications = applications;
		}

		public CalculationData Calculate(long applicationId)
		{
			var application = _applications.Get(applicationId);

			if(application.AirWaybillId == null)
				throw new InvalidOperationException("For calculation an air waybill should be presented. Applicaiton id: "
													+ application.Id);

			var awb = _awbs.Get(application.AirWaybillId.Value).First();

			var weight = application.Weight ?? 0;
			var tariffPerKg = application.TariffPerKg ?? 0;
			var scotch = GetTapeCost(application);
			var transitCost = application.TransitCostEdited ?? application.TransitCost ?? 0;
			var pickupCost = application.PickupCostEdited ?? application.PickupCost ?? 0;

			var calculation = new CalculationData
			{
				AirWaybillDisplay = HttpUtility.HtmlDecode(AwbHelper.GetAirWaybillDisplay(awb)),
				ApplicationDisplay = ApplicationHelper.GetApplicationDisplay(application.DisplayNumber, application.Count),
				ClientId = application.ClientId,
				FactureCost = application.FactureCostEdited ?? application.FactureCost ?? 0,
				FactureCostEx = application.FactureCostExEdited ?? application.FactureCostEx ?? 0,
				MarkName = application.MarkName,
				FactoryName = application.FactoryName,
				ScotchCost = scotch,
				TariffPerKg = tariffPerKg,
				Weight = weight,
				TransitCost = transitCost,
				PickupCost = pickupCost,
				CreationTimestamp = DateTimeProvider.Now,
				Count = application.Count ?? 0,
				Invoice = application.Invoice,
				Value = application.Value,
				Class = application.Class,
				InsuranceRate = application.InsuranceRate,
				Profit = application.CalculationProfit,
				TotalTariffCost = application.CalculationTotalTariffCost
			};

			_calculations.Add(calculation, applicationId);

			return calculation;
		}

		public void CancelCalculatation(long applicationId)
		{
			_calculations.RemoveByApplication(applicationId);
		}

		private decimal GetTapeCost(ApplicationExtendedData application)
		{
			var scotch = application.ScotchCostEdited;

			if(!scotch.HasValue)
			{
				var tariffs = _senders.GetTariffs(application.SenderId);

				scotch = CalculationHelper.GetSenderScotchCost(tariffs, application.SenderId, application.Count);
			}

			return scotch ?? 0;
		}
	}
}