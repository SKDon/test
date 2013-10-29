using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
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
		private readonly IStateService _stateService;

		public ClientCalculationPresenter(
			IApplicationRepository applicationRepository,
			IAwbRepository awbRepository,
			IStateService stateService,
			IClientRepository clientRepository)
		{
			_applicationRepository = applicationRepository;
			_awbRepository = awbRepository;
			_stateService = stateService;
			_clientRepository = clientRepository;
		}

		public ClientCalculationListCollection List(long clientId, int take, long skip)
		{
			long total;
			var applications = GetCalculatedApplicationsWithAwb(clientId, take, skip, out total);

			var awbsData = _awbRepository.Get(applications.Select(x => x.AirWaybillId ?? 0).ToArray())
										 .OrderByDescending(x => x.CreationTimestamp)
										 .ToArray();

			var items = GetItems(applications);

			var groups = GetGroups(awbsData, items);

			return new ClientCalculationListCollection
			{
				Groups = groups.ToArray(),
				Total = total
			};
		}

		private ApplicationListItemData[] GetCalculatedApplicationsWithAwb(long clientId, int take, long skip, out long total)
		{
			var stateIds = _stateService.GetVisibleStates();

			var applications = _applicationRepository.List(clientId: clientId, stateIds: stateIds, orders: Order.Default)
													 .ToArray();

			var appIds = applications.Select(x => x.Id).ToArray();

			var calculations = _applicationRepository.GetCalculations(appIds);

			applications = applications.Where(x => calculations.ContainsKey(x.Id) && x.AirWaybillId.HasValue).ToArray();

			total = applications.LongLength;

			return applications.Skip((int) skip).Take(take).ToArray();
		}

		private static List<ClientCalculationGroup> GetGroups(IEnumerable<AirWaybillData> awbsData,
			IEnumerable<ClientCalculationItem> items)
		{
			return items.GroupBy(x => x.AirWaybillId).Select(g =>
			{
				var itemsGroup1 = g.ToArray();

				return new ClientCalculationGroup
				{
					AirWaybillId = g.Key,
					items = itemsGroup1,
					value = AwbHelper.GetAirWaybillDisplay(awbsData.First(x => x.Id == g.Key)),
					aggregates = new ClientCalculationGroup.Aggregates(g.Sum(x => x.Profit))
				};
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
					? ((ClassType) a.ClassId.Value).ToLocalString()
					: ""
			}).ToArray();
		}
	}
}