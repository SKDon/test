using System.Linq;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Calculation;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Services.Calculation
{
	public sealed class CalculationService : ICalculationService
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IAwbRepository _awbRepository;
		private readonly IClientRepository _clientRepository;

		public CalculationService(
			IApplicationRepository applicationRepository,
			IAwbRepository awbRepository,
			IClientRepository clientRepository)
		{
			_applicationRepository = applicationRepository;
			_awbRepository = awbRepository;
			_clientRepository = clientRepository;
		}

		public CalculationListCollection List(int take, long skip)
		{
			var awbs = _awbRepository.GetRange(take, skip);
			var applications = _applicationRepository.GetByAirWaybill(awbs.Select(x => x.Id).ToArray());
			var nics = _clientRepository.GetNicByApplications(applications.Select(x => x.Id).ToArray());

			var items = applications.Select(a => new CalculationDetailsItem
			{
				ApplicationId = a.Id,
				Value = a.Value,
				Count = a.Count,
				ClientNic = nics[a.Id],
				Factory = a.FactoryName,
				FactureCost = a.FactureCostEdited ?? a.FactureCost,
				Invoice = a.Invoice,
				Mark = a.MarkName,
				ScotchCost = a.ScotchCostEdited ?? a.ScotchCost,
				TariffPerKg = a.TariffPerKg,
				TransitCost = a.TransitCost,
				ForwarderCost = a.ForwarderCost,
				ValueCurrencyId = a.CurrencyId,
				Weigth = a.Weigth,
				WithdrawCost = a.WithdrawCostEdited ?? a.WithdrawCost,
				AwbDisplay = AwbHelper.GetAirWayBillDisplay(awbs.First(x=>x.Id == a.AirWaybillId)),
				AirWaybillId = a.AirWaybillId
			}).ToArray();

			return new CalculationListCollection
			{
				Data = items,
				Total = _awbRepository.Count()
			};
		}
	}
}