using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Calculation;
using Alicargo.ViewModels.Calculation.Sender;
using Alicargo.ViewModels.Helpers;
using Resources;

namespace Alicargo.Services.Calculation
{
	public sealed class SenderCalculationPresenter : ISenderCalculationPresenter
	{
		private readonly IApplicationRepository _applications;
		private readonly IAwbRepository _awbs;
		private readonly IClientRepository _clients;
		private readonly ISenderRepository _senders;
		private readonly IStateService _stateService;

		public SenderCalculationPresenter(
			IApplicationRepository applications,
			IStateService stateService,
			IAwbRepository awbs,
			IClientRepository clients,
			ISenderRepository senders)
		{
			_applications = applications;
			_stateService = stateService;
			_awbs = awbs;
			_clients = clients;
			_senders = senders;
		}

		public SenderCalculationListCollection List(long senderId, int take, long skip)
		{
			long total;
			var applications = GetApplications(senderId, take, (int)skip, out total);

			// ReSharper disable PossibleInvalidOperationException
			var awbsData = _awbs.Get(applications.Where(x => x.AirWaybillId.HasValue).Select(x => x.AirWaybillId.Value).ToArray())
				// ReSharper restore PossibleInvalidOperationException
								.OrderByDescending(x => x.DateOfArrival)
								.ToArray();


			var items = GetItems(awbsData, applications);

			var groups = GetGroups(awbsData, items);

			var tariffs = _senders.GetTariffs(applications.Select(x => x.SenderId ?? 0).Where(x => x > 0).ToArray());

			var info = GetInfo(awbsData, applications, tariffs);

			return new SenderCalculationListCollection
			{
				Groups = groups.ToArray(),
				Total = total,
				Info = info
			};
		}

		private ApplicationListItemData[] GetApplications(long senderId, int take, int skip, out long total)
		{
			var stateIds = _stateService.GetVisibleStates();

			var applications = _applications.List(senderId: senderId, stateIds: stateIds, skip: skip, take: take, orders: Order.Default)
											.ToArray();

			total = _applications.Count(stateIds, senderId: senderId);

			return applications;
		}

		private static List<SenderCalculationGroup> GetGroups(IEnumerable<AirWaybillData> awbsData,
			IEnumerable<SenderCalculationItem> items)
		{
			var groups = items.GroupBy(x => x.AirWaybillId).Select(g =>
			{
				var itemsGroup = g.ToArray();
				var awb = awbsData.FirstOrDefault(x => x.Id == g.Key);
				var text = awb != null ? AwbHelper.GetAirWaybillDisplay(awb) : Pages.NoAirWaybill;
				return new SenderCalculationGroup
				{
					AirWaybillId = g.Key,
					items = itemsGroup,
					value = new { id = g.Key, text },
					aggregates = new SenderCalculationGroup.Aggregates(itemsGroup)
				};
			}).ToList();

			return groups;
		}

		private IEnumerable<SenderCalculationItem> GetItems(IEnumerable<AirWaybillData> awbsData,
			ApplicationListItemData[] applications)
		{
			var appIds = applications.Select(x => x.Id).ToArray();
			var nics = _clients.GetNicByApplications(appIds);
			var ranks = awbsData.Select((pair, i) => new { pair.Id, Rank = i }).ToDictionary(x => x.Id, x => x.Rank);
			ranks.Add(0, -1);

			return applications.Select(a => new SenderCalculationItem
			{
				ApplicationId = a.Id,
				Value = a.Value,
				Count = a.Count,
				ClientNic = nics[a.Id],
				Factory = a.FactoryName,
				FactureCost = a.SenderFactureCost,
				Invoice = a.Invoice,
				Mark = a.MarkName,
				SenderScotchCost = a.SenderScotchCost,
				ValueCurrencyId = a.CurrencyId,
				Weigth = a.Weigth,
				PickupCost = a.SenderPickupCost,
				AirWaybillId = a.AirWaybillId ?? 0,
				DisplayNumber = ApplicationHelper.GetDisplayNumber(a.Id, a.Count),
				Profit = (a.SenderScotchCost ?? 0) + (a.SenderFactureCost ?? 0) + (a.SenderPickupCost ?? 0)
						 + CalculationHelper.GetTotalSenderRate(a.SenderRate, a.Weigth),
				TotalSenderRate = CalculationHelper.GetTotalSenderRate(a.SenderRate, a.Weigth),
				SenderRate = a.SenderRate
			}).OrderBy(x => ranks[x.AirWaybillId]).ToArray();
		}

		private static SenderCalculationAwbInfo[] GetInfo(IEnumerable<AirWaybillData> awbs,
			IEnumerable<ApplicationListItemData> items,
			IReadOnlyDictionary<long, decimal> tariffs)
		{
			return awbs.Select(awb =>
			{
				var rows = items.Where(a => a.AirWaybillId == awb.Id).ToArray();

				var info = new SenderCalculationAwbInfo
				{
					AirWaybillId = awb.Id,
					TotalCostOfSenderForWeight = awb.TotalCostOfSenderForWeight ?? 0,
					FlightCost = awb.FlightCost ?? 0,
					TotalSenderRate = rows.Sum(x => CalculationHelper.GetTotalSenderRate(x.SenderRate, x.Weigth)),
					TotalScotchCost = rows.Sum(x => CalculationHelper.GetSenderScotchCost(tariffs, x.SenderId, x.Count) ?? 0),
					TotalFactureCost = rows.Sum(x => x.SenderFactureCost ?? 0),
					TotalPickupCost = rows.Sum(x => x.SenderPickupCost ?? 0),
					CostPerKgOfSender = null,
					FlightCostPerKg = null
				};

				var totalWeight = (decimal)rows.Sum(x => x.Weigth ?? 0);

				if (totalWeight != 0)
				{
					info.CostPerKgOfSender = info.TotalSenderRate / totalWeight;
					info.FlightCostPerKg = info.FlightCost / totalWeight;
				}

				return info;
			}).ToArray();
		}
	}
}