using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Calculation;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Services.Calculation
{
	public sealed class ClientCalculationPresenter : IClientCalculationPresenter
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IAwbRepository _awbRepository;
		private readonly IClientRepository _clientRepository;

		public ClientCalculationPresenter(
			IApplicationRepository applicationRepository,
			IAwbRepository awbRepository,
			IClientRepository clientRepository)
		{
			_applicationRepository = applicationRepository;
			_awbRepository = awbRepository;
			_clientRepository = clientRepository;
		}

		public ClientCalculationListCollection List(int take, long skip)
		{
			var awbs = _awbRepository.GetRange(take, skip).OrderByDescending(SortingValue).ToArray();

			return List(awbs);
		}

		private ClientCalculationListCollection List(IList<AirWaybillData> data)
		{
			var awbs = data.ToDictionary(x => x.Id, x => x);

			var applications = _applicationRepository.GetByAirWaybill(awbs.Select(x => x.Key).ToArray());

			var items = GetItems(awbs, applications);

			var groups = GetGroups(data, items, awbs);

			return new ClientCalculationListCollection
			{
				Groups = groups.ToArray(),
				Total = _awbRepository.Count()
			};
		}

		private static List<ClientCalculationGroup> GetGroups(IList<AirWaybillData> data, IEnumerable<ClientCalculationItem> items, IReadOnlyDictionary<long, AirWaybillData> awbs)
		{
			var groups = items.GroupBy(x => x.AirWaybillId).Select(g =>
			{
				var itemsGroup = g.ToArray();
				return new ClientCalculationGroup
				{
					AirWaybillId = g.Key,
					items = itemsGroup,
					value = AwbHelper.GetAirWaybillDisplay(awbs[g.Key])
				};
			}).ToList();

			AddMissedGroups(data, groups);

			return groups;
		}

		private IEnumerable<ClientCalculationItem> GetItems(IReadOnlyDictionary<long, AirWaybillData> awbs, ApplicationData[] applications)
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
				FactureCost = a.FactureCostEdited ?? a.FactureCost,
				Invoice = a.Invoice,
				Mark = a.MarkName,
				ScotchCost = a.ScotchCostEdited ?? a.ScotchCost,
				TariffPerKg = a.TariffPerKg,
				TransitCost = a.TransitCost,
				ValueCurrencyId = a.CurrencyId,
				Weigth = a.Weigth,
				WithdrawCost = a.WithdrawCostEdited ?? a.WithdrawCost, // ReSharper disable PossibleInvalidOperationException
				AirWaybillId = a.AirWaybillId.Value, // ReSharper restore PossibleInvalidOperationException
				DisplayNumber = ApplicationHelper.GetDisplayNumber(a.Id, a.Count),
				TotalTariffCost = CalculationHelper.GetTotalTariffCost(a.TariffPerKg, a.Weigth),
				Profit = CalculationHelper.GetProfit(a),
				InsuranceCost = CalculationHelper.GetInsuranceCost(a.Value),
				Class = "todo"
			}).OrderByDescending(x => SortingValue(awbs[x.AirWaybillId])).ToArray();
		}

		private static void AddMissedGroups(IList<AirWaybillData> awbs, IList<ClientCalculationGroup> groups)
		{
			for (var i = 0; i < awbs.Count; i++)
			{
				var awb = awbs[i];
				if (groups.Count == i || awb.Id != groups[i].AirWaybillId)
				{
					groups.Insert(i, new ClientCalculationGroup
					{
						AirWaybillId = awb.Id,
						items = new ClientCalculationItem[0],
						value = AwbHelper.GetAirWaybillDisplay(awb)						
					});
				}
			}
		}

		private static DateTimeOffset SortingValue(AirWaybillData awb)
		{
			return awb.DateOfArrival;
		}
	}
}