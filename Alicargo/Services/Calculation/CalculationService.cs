using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Calculation;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Services.Calculation
{
	public sealed class CalculationService : ICalculationService
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IAwbRepository _awbRepository;
		private readonly IClientRepository _clientRepository;

		public CalculationService(
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
			var awbs = _awbRepository.GetRange(take, skip);

			return List(awbs);
		}

		private CalculationListCollection List(IList<AirWaybillData> data)
		{
			var awbs = data.ToDictionary(x => x.Id, x => x);
			var applications = _applicationRepository.GetByAirWaybill(awbs.Select(x => x.Key).ToArray());
			var nics = _clientRepository.GetNicByApplications(applications.Select(x => x.Id).ToArray());

			var items = applications.Select(a => new CalculationItem
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
				ForwarderCost = a.ForwarderCost,
				ValueCurrencyId = a.CurrencyId,
				Weigth = a.Weigth,
				WithdrawCost = a.WithdrawCostEdited ?? a.WithdrawCost, // ReSharper disable PossibleInvalidOperationException
				AirWaybillId = a.AirWaybillId.Value // ReSharper restore PossibleInvalidOperationException
			}).OrderByDescending(x => awbs[x.AirWaybillId].CreationTimestamp).ToArray();

			var groups = items.GroupBy(x => x.AirWaybillId).Select(g =>
			{
				var itemsGroup = g.ToArray();
				return new CalculationGroup
				{
					AirWaybillId = g.Key,
					items = itemsGroup,
					value = AwbHelper.GetAirWayBillDisplay(awbs[g.Key]),
					field = "AirWaybillId",
					hasSubgroups = false,
					aggregates = new CalculationGroup.Aggregates(itemsGroup)
				};
			}).ToList();

			AddMissedGroups(data, groups);

			var info = awbs.Select(x => x.Value)
						   .Select(x => new CalculationInfo(items.Where(a => a.AirWaybillId == x.Id).ToArray())
						   {
							   AirWaybillId = x.Id,
							   TotalCostOfSenderForWeight = x.TotalCostOfSenderForWeight ?? 0,
							   FlightCost = x.FlightCost ?? 0,
							   CustomCost = x.CustomCost ?? 0,
							   BrokerCost = x.BrokerCost ?? 0,
							   AdditionalCost = x.AdditionalCost
						   }).ToArray();

			return new CalculationListCollection
			{
				Groups = groups.ToArray(),
				Total = _awbRepository.Count(),
				Info = info
			};
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
						value = AwbHelper.GetAirWayBillDisplay(awb),
						field = "AirWaybillId",
						hasSubgroups = false,
						aggregates = new CalculationGroup.Aggregates(new CalculationItem[0])
					});
				}
			}
		}

		public CalculationListCollection Row(long id)
		{
			var data = _awbRepository.Get(id);

			return List(data);
		}
	}
}