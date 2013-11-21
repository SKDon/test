using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Core.Helpers;
using Alicargo.Core.Localization;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Calculation;
using Alicargo.ViewModels.Calculation.Client;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Services.Calculation
{
	public sealed class ClientCalculationPresenter : IClientCalculationPresenter
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IAwbRepository _awbRepository;
		private readonly IClientRepository _clientRepository;
		private readonly IStateFilter _stateFilter;

		public ClientCalculationPresenter(
			IApplicationRepository applicationRepository,
			IAwbRepository awbRepository,
			IStateFilter stateFilter,
			IClientRepository clientRepository)
		{
			_applicationRepository = applicationRepository;
			_awbRepository = awbRepository;
			_stateFilter = stateFilter;
			_clientRepository = clientRepository;
		}

		public ClientCalculationListCollection List(long clientId, int take, long skip)
		{
			long total;
			var applications = GetCalculatedApplications(clientId, take, skip, out total);

			// ReSharper disable PossibleInvalidOperationException
			var awbsData = _awbRepository.Get(applications.Select(x => x.AirWaybillId.Value).ToArray()).ToArray(); // ReSharper restore PossibleInvalidOperationException

			var items = GetItems(applications);

			var groups = GetGroups(awbsData, items);

			return new ClientCalculationListCollection
			{
				Groups = groups.ToArray(),
				Total = total
			};
		}

		private ApplicationListItemData[] GetCalculatedApplications(long clientId, int take, long skip, out long total)
		{
			var stateIds = _stateFilter.GetStateVisibility();

			var applications = _applicationRepository.List(stateIds, new[]
			{
			    new Order
			    {
			        Desc = true,
			        OrderType = OrderType.AirWaybill
			    }
			}, take, (int)skip, clientId, hasCalculation: true).ToArray();

			total = _applicationRepository.Count(stateIds, clientId, hasCalculation: true);

			return applications;
		}

		private static List<ClientCalculationGroup> GetGroups(IEnumerable<AirWaybillData> awbsData,
			IEnumerable<ClientCalculationItem> items)
		{
			return items.GroupBy(x => x.AirWaybillId).Select(g => new ClientCalculationGroup
			{
				AirWaybillId = g.Key,
				items = g.ToArray(),
				value = AwbHelper.GetAirWaybillDisplay(awbsData.First(x => x.Id == g.Key)),
				aggregates = new ClientCalculationGroup.Aggregates(g.Sum(x => x.Profit))
			}).ToList();
		}

		private IEnumerable<ClientCalculationItem> GetItems(ApplicationListItemData[] applications)
		{
			var appIds = applications.Select(x => x.Id).ToArray();
			var nics = _clientRepository.GetNicByApplications(appIds);

			return applications.Select(a => new ClientCalculationItem
			{
				ApplicationId = a.Id,
				Value = a.Value,
				Count = a.Count,
				ClientNic = nics[a.Id],
				Factory = a.FactoryName,
				FactureCost = a.FactureCost,
				Invoice = a.Invoice,
				Mark = a.MarkName,
				ScotchCost = a.ScotchCost,
				TariffPerKg = a.TariffPerKg,
				TransitCost = a.TransitCost,
				ValueCurrencyId = a.CurrencyId,
				Weight = a.Weight,
				PickupCost = a.PickupCost, // ReSharper disable PossibleInvalidOperationException
				AirWaybillId = a.AirWaybillId.Value, // ReSharper restore PossibleInvalidOperationException
				DisplayNumber = ApplicationHelper.GetDisplayNumber(a.Id, a.Count),
				TotalTariffCost = CalculationHelper.GetTotalTariffCost(a.TariffPerKg, a.Weight),
				Profit = CalculationHelper.GetProfit(a),
				InsuranceCost = CalculationHelper.GetInsuranceCost(a.Value),
				ClassName = a.ClassId.HasValue
					? ((ClassType)a.ClassId.Value).ToLocalString()
					: ""
			}).ToArray();
		}
	}
}