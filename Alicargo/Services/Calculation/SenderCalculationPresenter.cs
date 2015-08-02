using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.AirWaybill;
using Alicargo.Core.Calculation;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Calculation.Sender;
using Resources;

namespace Alicargo.Services.Calculation
{
	public sealed class SenderCalculationPresenter : ISenderCalculationPresenter
	{
		private readonly IApplicationRepository _applications;
		private readonly IAwbRepository _awbs;
		private readonly ISenderRepository _senders;
		private readonly IStateFilter _stateFilter;

		public SenderCalculationPresenter(
			IApplicationRepository applications,
			IStateFilter stateFilter,
			IAwbRepository awbs,
			ISenderRepository senders)
		{
			_applications = applications;
			_stateFilter = stateFilter;
			_awbs = awbs;
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

		private ApplicationData[] GetApplications(long senderId, int take, int skip, out long total)
		{
			var stateIds = _stateFilter.GetStateVisibility();

			var applications = _applications.List(stateIds,
				new[]
				{
					new Order
					{
						Desc = true,
						OrderType = OrderType.AirWaybill
					}
				},
				take,
				skip,
				senderId: senderId).ToArray();

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

		private static SenderCalculationItem[] GetItems(IEnumerable<ApplicationData> applications)
		{
			return applications.Select(a => new SenderCalculationItem
			{
				ApplicationId = a.Id,
				Value = a.Value,
				Count = a.Count,
				ClientNic = a.ClientNic,
				Factory = a.FactoryName,
				FactureCost = a.FactureCost,
				FactureCostEx = a.FactureCostEx,
				Invoice = a.Invoice,
				Mark = a.MarkName,
				SenderScotchCost = a.SenderScotchCost,
				ValueCurrencyId = a.CurrencyId,
				Weight = a.Weight,
				PickupCost = a.PickupCost,
				AirWaybillId = a.AirWaybillId,
				DisplayNumber = a.GetApplicationDisplay(),
				Profit = GetProfit(a),
				TotalSenderRate = CalculationHelper.GetTotalSenderRate(a.SenderRate, a.Weight),
				SenderRate = a.SenderRate
			}).ToArray();
		}

		private static SenderCalculationAwbInfo[] GetInfo(
			IEnumerable<SenderCalculationGroup> groups,
			IEnumerable<ApplicationData> items,
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
					TotalFactureCost = rows.Sum(x => x.FactureCost ?? 0),
					TotalFactureCostEx = rows.Sum(x => x.FactureCostEx ?? 0),
					TotalPickupCost = rows.Sum(x => x.PickupCost ?? 0),
					CostPerKgOfSender = null,
					FlightCostPerKg = null
				};

				var totalWeight = (decimal)rows.Sum(x => x.Weight ?? 0);

				if(totalWeight != 0)
				{
					info.CostPerKgOfSender = info.TotalSenderRate / totalWeight;
					info.FlightCostPerKg = info.FlightCost / totalWeight;
				}

				return info;
			}).ToArray();
		}

		private static decimal GetProfit(ApplicationData data)
		{
			return (data.SenderScotchCost ?? 0)
			       + (data.FactureCost ?? 0)
			       + (data.FactureCostEx ?? 0)
			       + (data.PickupCost ?? 0)
			       + CalculationHelper.GetTotalSenderRate(data.SenderRate, data.Weight);
		}
	}
}