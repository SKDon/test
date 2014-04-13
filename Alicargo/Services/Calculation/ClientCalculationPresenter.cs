using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.AirWaybill;
using Alicargo.Core.Calculation;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.Utilities.Localization;
using Alicargo.ViewModels.Calculation.Client;

namespace Alicargo.Services.Calculation
{
	// todo: 1. refactor to use calculated data
	public sealed class ClientCalculationPresenter : IClientCalculationPresenter
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IAwbRepository _awbRepository;
		private readonly IClientRepository _clientRepository;
		private readonly IStateSettingsRepository _settings;

		public ClientCalculationPresenter(
			IApplicationRepository applicationRepository,
			IAwbRepository awbRepository,
			IClientRepository clientRepository,
			IStateSettingsRepository settings)
		{
			_applicationRepository = applicationRepository;
			_awbRepository = awbRepository;
			_clientRepository = clientRepository;
			_settings = settings;
		}

		public ClientCalculationListCollection List(long clientId, int take, long skip)
		{
			long total;
			var applications = GetCalculatedApplications(clientId, take, skip, out total);

			var awbIds = applications.Select(x => x.AirWaybillId.Value).ToArray();

			var awbsData = _awbRepository.Get(awbIds).ToArray();

			var items = GetItems(applications);

			var groups = GetGroups(awbsData, items);

			return new ClientCalculationListCollection
			{
				Groups = groups.ToArray(),
				Total = total
			};
		}

		private ApplicationExtendedData[] GetCalculatedApplications(long clientId, int take, long skip, out long total)
		{
			var stateIds = _settings.GetStateVisibilities()
				.Where(x => x.Role == RoleType.Client).Select(x => x.StateId)
				.ToArray();

			var applications = _applicationRepository.List(stateIds,
				new[]
				{
					new Order
					{
						Desc = true,
						OrderType = OrderType.AirWaybill
					}
				},
				take,
				(int)skip,
				clientId,
				hasCalculation: true).ToArray();

			total = _applicationRepository.Count(stateIds, clientId, hasCalculation: true);

			return applications;
		}

		private IEnumerable<ClientCalculationItem> GetItems(ApplicationExtendedData[] applications)
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
				FactureCost = a.GetAdjustedFactureCost(),
				FactureCostEx = a.GetAdjustedFactureCostEx(),
				Invoice = a.Invoice,
				Mark = a.MarkName,
				ScotchCost = a.GetAdjustedScotchCost(),
				TariffPerKg = a.TariffPerKg,
				TransitCost = a.GetAdjustedTransitCost(),
				ValueCurrencyId = a.CurrencyId,
				Weight = a.Weight,
				PickupCost = a.GetAdjustedPickupCost(),
				AirWaybillId = a.AirWaybillId.Value,
				DisplayNumber = a.GetApplicationDisplay(),
				TotalTariffCost = CalculationHelper.GetTotalTariffCost(a.CalculationTotalTariffCost, a.TariffPerKg, a.Weight),
				Profit = GetProfit(a),
				InsuranceCost = CalculationHelper.GetInsuranceCost(a.Value, a.InsuranceRate),
				ClassName = a.Class.HasValue
					? a.Class.Value.ToLocalString()
					: ""
			}).ToArray();
		}

		private static List<ClientCalculationGroup> GetGroups(
			IEnumerable<AirWaybillData> awbsData,
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

		private static decimal GetProfit(ApplicationExtendedData application)
		{
			return application.CalculationProfit
			       ?? CalculationHelper.GetTotalTariffCost(application.CalculationTotalTariffCost,
				       application.TariffPerKg,
				       application.Weight)
			       + (application.GetAdjustedScotchCost() ?? 0)
			       + CalculationHelper.GetInsuranceCost(application.Value, application.InsuranceRate)
			       + (application.GetAdjustedFactureCost() ?? 0)
			       + (application.GetAdjustedFactureCostEx() ?? 0)
			       + (application.GetAdjustedPickupCost() ?? 0)
			       + (application.GetAdjustedTransitCost() ?? 0);
		}
	}
}