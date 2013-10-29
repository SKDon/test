using System;
using System.Linq;
using System.Web;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Calculation;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Services.Calculation
{
	internal sealed class CalculationService : ICalculationService
	{
		private readonly IApplicationRepository _applications;
		private readonly IAwbRepository _awbs;
		private readonly ICalculationRepository _calculations;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ISenderRepository _senders;

		public CalculationService(
			ICalculationRepository calculations,
			IUnitOfWork unitOfWork,
			ISenderRepository senders,
			IAwbRepository awbs,
			IApplicationRepository applications)
		{
			_calculations = calculations;
			_unitOfWork = unitOfWork;
			_senders = senders;
			_awbs = awbs;
			_applications = applications;
		}

		public void Calculate(long applicationId)
		{
			var application = _applications.Get(applicationId);

			if (application.AirWaybillId == null)
				throw new InvalidOperationException("For calculation an air waybill should be presented. Applicaiton id: "
													+ application.Id);

			var awb = _awbs.Get(application.AirWaybillId.Value).First();

			var weigth = application.Weigth ?? 0;
			var tariffPerKg = application.TariffPerKg ?? 0;
			var insurance = application.Value / CalculationHelper.InsuranceRate;
			var scotch = GetTapeCost(application);
			var facture = application.FactureCostEdited ?? application.FactureCost ?? 0;
			// todo: !!! почему сдесь нет TransitCostEdited?

			var calculation = new CalculationData
			{
				AirWaybillDisplay = HttpUtility.HtmlDecode(AwbHelper.GetAirWaybillDisplay(awb)),
				ApplicationDisplay = ApplicationHelper.GetDisplayNumber(application.Id, application.Count),
				ClientId = application.ClientId,
				FactureCost = facture,
				InsuranceCost = insurance,
				MarkName = application.MarkName,
				ScotchCost = scotch,
				TariffPerKg = tariffPerKg,
				Weight = weigth
			};

			_calculations.Add(calculation, applicationId);

			_unitOfWork.SaveChanges();
		}

		private decimal GetTapeCost(ApplicationData application)
		{
			var scotch = application.ScotchCostEdited;

			if (!scotch.HasValue && application.SenderId.HasValue)
			{
				var tariffs = _senders.GetTariffs(application.SenderId.Value);

				scotch = CalculationHelper.GetSenderScotchCost(tariffs, application.SenderId.Value, application.Count);
			}

			return scotch ?? 0;
		}

		public void RemoveCalculatation(long applicationId)
		{
			_calculations.RemoveByApplication(applicationId);

			_unitOfWork.SaveChanges();
		}
	}
}