using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.Calculation;
using Alicargo.Core.Calculation.Entities;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Utilities.Localization;
using Alicargo.ViewModels.Calculation;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Services.Calculation
{
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
			var stateIds = _settings.GetStateVisibilities().Where(x => x.Role == RoleType.Client).Select(x => x.StateId).ToArray();

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