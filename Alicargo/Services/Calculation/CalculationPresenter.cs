﻿using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Calculation;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Services.Calculation
{
	public sealed class CalculationPresenter : ICalculationPresenter
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IAwbRepository _awbRepository;
		private readonly IClientRepository _clientRepository;

		public CalculationPresenter(
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
			var awbs = _awbRepository.GetRange(take, skip).OrderByDescending(SortingValue).ToArray();

			return List(awbs);
		}

		public CalculationListCollection Row(long awbId)
		{
			var data = _awbRepository.Get(awbId);

			return List(data);
		}

		public object ClientCalculationList(int take, long skip)
		{
			throw new NotImplementedException();
		}		

		private CalculationListCollection List(IList<AirWaybillData> data)
		{
			var awbs = data.ToDictionary(x => x.Id, x => x);

			var applications = _applicationRepository.GetByAirWaybill(awbs.Select(x => x.Key).ToArray());

			var items = GetItems(awbs, applications);

			var info = GetInfo(awbs, applications);

			var groups = GetGroups(data, items, awbs);

			return new CalculationListCollection
			{
				Groups = groups.ToArray(),
				Total = _awbRepository.Count(),
				Info = info
			};
		}

		private static CalculationAwbInfo[] GetInfo(Dictionary<long, AirWaybillData> awbs, IEnumerable<ApplicationData> items)
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
								   TotalScotchCost = rows.Sum(x => x.ScotchCost ?? 0),
								   TotalFactureCost = rows.Sum(x => x.FactureCost ?? 0),
								   TotalWithdrawCost = rows.Sum(x => x.WithdrawCost ?? 0),
								   TotalTransitCost = rows.Sum(x => x.TransitCost ?? 0),
								   TotalInsuranceCost = rows.Sum(x => CalculationHelper.GetInsuranceCost(x.Value)),
								   BrokerCostPerKg = null,
								   CostPerKgOfSender = null,
								   CustomCostPerKg = null,
								   FlightCostPerKg = null,
								   ProfitPerKg = null,
								   Profit = 0
							   };

							   info.Profit = rows.Sum(x => CalculationHelper.GetProfit(x)) - info.TotalExpenses;

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

		private static List<CalculationGroup> GetGroups(IList<AirWaybillData> data, IEnumerable<CalculationItem> items, IReadOnlyDictionary<long, AirWaybillData> awbs)
		{
			var groups = items.GroupBy(x => x.AirWaybillId).Select(g =>
			{
				var itemsGroup = g.ToArray();
				return new CalculationGroup
				{
					AirWaybillId = g.Key,
					items = itemsGroup,
					value = AwbHelper.GetAirWaybillDisplay(awbs[g.Key]),
					field = "AirWaybillId",
					hasSubgroups = false,
					aggregates = new CalculationGroup.Aggregates(itemsGroup)
				};
			}).ToList();

			AddMissedGroups(data, groups);

			return groups;
		}

		private IEnumerable<CalculationItem> GetItems(IReadOnlyDictionary<long, AirWaybillData> awbs, ApplicationData[] applications)
		{
			var appIds = applications.Select(x => x.Id).ToArray();
			var calculations = _applicationRepository.GetCalculations(appIds);
			var nics = _clientRepository.GetNicByApplications(appIds);

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
				ScotchCost = a.ScotchCostEdited ?? a.ScotchCost,
				TariffPerKg = a.TariffPerKg,
				SenderRate = a.SenderRate,
				TransitCost = a.TransitCost,
				ValueCurrencyId = a.CurrencyId,
				Weigth = a.Weigth,
				WithdrawCost = a.WithdrawCostEdited ?? a.WithdrawCost, // ReSharper disable PossibleInvalidOperationException
				AirWaybillId = a.AirWaybillId.Value, // ReSharper restore PossibleInvalidOperationException
				DisplayNumber = ApplicationHelper.GetDisplayNumber(a.Id, a.Count),
				TotalTariffCost = CalculationHelper.GetTotalTariffCost(a.TariffPerKg, a.Weigth),
				Profit = CalculationHelper.GetProfit(a),
				InsuranceCost = CalculationHelper.GetInsuranceCost(a.Value),
				TotalSenderRate = CalculationHelper.GetTotalSenderRate(a.SenderRate, a.Weigth),
				IsCalculated = calculations.ContainsKey(a.Id)
			}).OrderByDescending(x => SortingValue(awbs[x.AirWaybillId])).ToArray();
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
						value = AwbHelper.GetAirWaybillDisplay(awb),
						field = "AirWaybillId",
						hasSubgroups = false,
						aggregates = new CalculationGroup.Aggregates(new CalculationItem[0])
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