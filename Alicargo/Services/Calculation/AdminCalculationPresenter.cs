using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Calculation;
using Alicargo.ViewModels.Calculation.Admin;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Services.Calculation
{
	public sealed class AdminCalculationPresenter : IAdminCalculationPresenter
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IAwbRepository _awbRepository;
		private readonly ISenderRepository _senders;
		private readonly IClientRepository _clientRepository;

		public AdminCalculationPresenter(
			IApplicationRepository applicationRepository,
			IAwbRepository awbRepository,
			ISenderRepository senders,
			IClientRepository clientRepository)
		{
			_applicationRepository = applicationRepository;
			_awbRepository = awbRepository;
			_senders = senders;
			_clientRepository = clientRepository;
		}

		public CalculationListCollection List(int take, long skip)
		{
			var awbs = _awbRepository.GetRange(take, skip).OrderByDescending(awb => awb.DateOfArrival).ToArray();

			return List(awbs);
		}

		public CalculationListCollection Row(long awbId)
		{
			var data = _awbRepository.Get(awbId);

			return List(data);
		}

		private CalculationListCollection List(IList<AirWaybillData> data)
		{
			var awbs = data.ToDictionary(x => x.Id, x => x);

			var applications = _applicationRepository.GetByAirWaybill(awbs.Select(x => x.Key).ToArray());

			var tariffs = _senders.GetTariffs(applications.Select(x => x.SenderId ?? 0).Where(x => x > 0).ToArray());

			var items = GetItems(data, applications, tariffs);

			var info = GetInfo(awbs, applications, tariffs);

			var groups = GetGroups(data, items, awbs);

			return new CalculationListCollection
			{
				Groups = groups.ToArray(),
				Total = _awbRepository.Count(),
				Info = info
			};
		}

		private static CalculationAwbInfo[] GetInfo(Dictionary<long, AirWaybillData> awbs, IEnumerable<ApplicationData> items,
			IReadOnlyDictionary<long, decimal> tariffs)
		{
			return awbs.Select(x => x.Value)
					   .Select(awb =>
					   {
						   var rows = items.Where(a => a.AirWaybillId == awb.Id).ToArray();

						   var info = new CalculationAwbInfo
						   {
							   AirWaybillId = awb.Id,
							   TotalCostOfSenderForWeight = awb.TotalCostOfSenderForWeight ?? 0,
							   FlightCost = awb.FlightCost ?? 0,
							   CustomCost = awb.CustomCost ?? 0,
							   BrokerCost = awb.BrokerCost ?? 0,
							   AdditionalCost = awb.AdditionalCost,
							   TotalSenderRate = rows.Sum(x => CalculationHelper.GetTotalSenderRate(x.SenderRate, x.Weigth)),
							   TotalScotchCost = rows.Sum(x => CalculationHelper.GetSenderScotchCost(tariffs, x.SenderId, x.Count) ?? 0),
							   TotalFactureCost = rows.Sum(x => x.FactureCost ?? 0),
							   TotalWithdrawCost = rows.Sum(x => x.WithdrawCost ?? 0),
							   TotalTransitCost = rows.Sum(x => x.TransitCost ?? 0),
							   TotalInsuranceCost = rows.Sum(x => CalculationHelper.GetInsuranceCost(x.Value)),
							   BrokerCostPerKg = null,
							   CostPerKgOfSender = null,
							   CustomCostPerKg = null,
							   FlightCostPerKg = null,
							   ProfitPerKg = null,
							   Profit = 0xBAD
						   };

						   info.Profit = rows.Sum(x => CalculationHelper.GetProfit(x, tariffs)) - info.TotalExpenses;

						   var totalWeight = (decimal)rows.Sum(x => x.Weigth ?? 0);

						   if (totalWeight != 0)
						   {
							   info.ProfitPerKg = info.Profit / totalWeight;
							   info.CostPerKgOfSender = info.TotalSenderRate / totalWeight;
							   info.FlightCostPerKg = info.FlightCost / totalWeight;
							   info.CustomCostPerKg = info.CustomCost / totalWeight;
							   info.BrokerCostPerKg = info.BrokerCost / totalWeight;
						   }

						   return info;
					   }).ToArray();
		}

		private static List<CalculationGroup> GetGroups(IList<AirWaybillData> data, IEnumerable<CalculationItem> items,
			IReadOnlyDictionary<long, AirWaybillData> awbs)
		{
			var groups = items.GroupBy(x => x.AirWaybillId).Select(g =>
			{
				var itemsGroup = g.ToArray();
				return new CalculationGroup
				{
					AirWaybillId = g.Key,
					items = itemsGroup,
					value = new { id = g.Key, text = AwbHelper.GetAirWaybillDisplay(awbs[g.Key]) },
					aggregates = new CalculationGroup.Aggregates(itemsGroup)
				};
			}).ToList();

			AddMissedGroups(data, groups);

			return groups;
		}

		private IEnumerable<CalculationItem> GetItems(IEnumerable<AirWaybillData> data, ApplicationData[] applications,
			IReadOnlyDictionary<long, decimal> tariffs)
		{
			var appIds = applications.Select(x => x.Id).ToArray();
			var calculations = _applicationRepository.GetCalculations(appIds);
			var nics = _clientRepository.GetNicByApplications(appIds);
			var ranks = data.Select((pair, i) => new { pair.Id, Rank = i }).ToDictionary(x => x.Id, x => x.Rank);

			return applications.Select(a => new CalculationItem
			{
				ApplicationId = a.Id,
				Value = a.Value,
				Count = a.Count,
				ClientNic = nics[a.Id],
				Factory = a.FactoryName,
				FactureCost = a.FactureCostEdited ?? a.FactureCost,
				Invoice = a.Invoice,
				Mark = a.MarkName,
				ScotchCost = a.ScotchCostEdited ?? CalculationHelper.GetSenderScotchCost(tariffs, a.SenderId, a.Count),
				TariffPerKg = a.TariffPerKg,
				SenderRate = a.SenderRate,
				TransitCost = a.TransitCost,
				ValueCurrencyId = a.CurrencyId,
				Weigth = a.Weigth,
				WithdrawCost = a.WithdrawCostEdited ?? a.WithdrawCost, // ReSharper disable PossibleInvalidOperationException
				AirWaybillId = a.AirWaybillId.Value, // ReSharper restore PossibleInvalidOperationException
				DisplayNumber = ApplicationHelper.GetDisplayNumber(a.Id, a.Count),
				TotalTariffCost = CalculationHelper.GetTotalTariffCost(a.TariffPerKg, a.Weigth),
				Profit = CalculationHelper.GetProfit(a, tariffs),
				InsuranceCost = CalculationHelper.GetInsuranceCost(a.Value),
				TotalSenderRate = CalculationHelper.GetTotalSenderRate(a.SenderRate, a.Weigth),
				IsCalculated = calculations.ContainsKey(a.Id),
				ClassId = (ClassType?)a.ClassId
			}).OrderBy(x => ranks[x.AirWaybillId]).ToArray();
		}

		private static void AddMissedGroups(IList<AirWaybillData> awbs, IList<CalculationGroup> groups)
		{
			for (var i = 0; i < awbs.Count; i++)
			{
				var awb = awbs[i];
				if (groups.Count == i || awb.Id != groups[i].AirWaybillId)
				{
					groups.Insert(i, new CalculationGroup
					{
						AirWaybillId = awb.Id,
						items = new CalculationItem[0],
						value = new { id = awb.Id, text = AwbHelper.GetAirWaybillDisplay(awb) },
						aggregates = new CalculationGroup.Aggregates(new CalculationItem[0])
					});
				}
			}
		}
	}
}