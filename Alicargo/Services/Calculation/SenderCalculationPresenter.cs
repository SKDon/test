using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Helpers;
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

			var items = GetItems(applications);

			var groups = GetGroups(items);

			var tariffs = _senders.GetTariffs(applications.Select(x => x.SenderId ?? 0).ToArray());

			var info = GetInfo(groups, applications, tariffs);

			return new SenderCalculationListCollection
			{
				Groups = groups.ToArray(),
				Total = total,
				Info = info
			};
		}

		private ApplicationListItemData[] GetApplications(long senderId, int take, int skip, out long total)
		{
			var stateIds = _stateService.GetStateVisibility();

			var applications = _applications.List(stateIds, new[]
			{
			    new Order
			    {
			        Desc = true,
			        OrderType = OrderType.AirWaybill
			    }
			}, take, skip, senderId: senderId).ToArray();

			total = _applications.Count(stateIds, senderId: senderId);

			return applications;
		}

		private List<SenderCalculationGroup> GetGroups(SenderCalculationItem[] items)
		{
			var awbsData = _awbs.Get(items.Select(x => x.AirWaybillId ?? 0).ToArray());

			var groups = items.GroupBy(x => x.AirWaybillId).Select(g =>
			{
				var itemsGroup = g.Key.HasValue
					? g.OrderBy(x => x.ClientNic).ThenByDescending(x => x.ApplicationId).ToArray()
					: g.OrderByDescending(x => x.ApplicationId).ToArray();
				
				var awb = awbsData.FirstOrDefault(x => x.Id == g.Key);

				var text = awb != null
					? AwbHelper.GetAirWaybillDisplay(awb)
					: Pages.NoAirWaybill;

				return new SenderCalculationGroup
				{
					AirWaybillId = g.Key,
					items = itemsGroup,
					value = new { id = g.Key, text },
					aggregates = new SenderCalculationGroup.Aggregates(itemsGroup),
					FlightCost = awb != null
						? awb.FlightCost ?? 0
						: 0,
					TotalCostOfSenderForWeight = awb != null
						? awb.TotalCostOfSenderForWeight ?? 0
						: 0
				};
			}).ToList();

			return groups;
		}

		private SenderCalculationItem[] GetItems(ApplicationListItemData[] applications)
		{
			var appIds = applications.Select(x => x.Id).ToArray();
			var nics = _clients.GetNicByApplications(appIds);

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
				Weight = a.Weight,
				PickupCost = a.SenderPickupCost,
				AirWaybillId = a.AirWaybillId,
				DisplayNumber = ApplicationHelper.GetDisplayNumber(a.Id, a.Count),
				Profit = (a.SenderScotchCost ?? 0) + (a.SenderFactureCost ?? 0) + (a.SenderPickupCost ?? 0)
						 + CalculationHelper.GetTotalSenderRate(a.SenderRate, a.Weight),
				TotalSenderRate = CalculationHelper.GetTotalSenderRate(a.SenderRate, a.Weight),
				SenderRate = a.SenderRate
			}).ToArray();
		}

		private static SenderCalculationAwbInfo[] GetInfo(
			IEnumerable<SenderCalculationGroup> groups,
			IEnumerable<ApplicationListItemData> items,
			IReadOnlyDictionary<long, decimal> tariffs)
		{
			return groups.Select(g =>
			{
				var rows = items.Where(a => a.AirWaybillId == g.AirWaybillId).ToArray();

				var info = new SenderCalculationAwbInfo
				{
					AirWaybillId = g.AirWaybillId,
					TotalCostOfSenderForWeight = g.TotalCostOfSenderForWeight,
					FlightCost = g.FlightCost,
					TotalSenderRate = rows.Sum(x => CalculationHelper.GetTotalSenderRate(x.SenderRate, x.Weight)),
					TotalScotchCost = rows.Sum(x => CalculationHelper.GetSenderScotchCost(tariffs, x.SenderId, x.Count) ?? 0),
					TotalFactureCost = rows.Sum(x => x.SenderFactureCost ?? 0),
					TotalPickupCost = rows.Sum(x => x.SenderPickupCost ?? 0),
					CostPerKgOfSender = null,
					FlightCostPerKg = null
				};

				var totalWeight = (decimal)rows.Sum(x => x.Weight ?? 0);

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