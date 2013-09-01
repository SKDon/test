using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;
using Resources;

namespace Alicargo.Services.Application
{
	internal sealed class ApplicationGrouper : IApplicationGrouper
	{
		private readonly IAwbRepository _awbRepository;
		private Dictionary<long, AirWaybillData> _airWaybills;
		private Dictionary<long, AirWaybillAggregate> _awbAggregates;

		public ApplicationGrouper(IAwbRepository awbRepository)
		{
			_awbRepository = awbRepository;
		}

		public ApplicationGroup[] Group(ApplicationListItem[] applications, OrderType[] groups)
		{
			var awbIds = applications.Select(x => x.AirWaybillId ?? 0)
									 .Distinct()
									 .ToArray();

			_airWaybills = _awbRepository.Get(awbIds).ToDictionary(x => x.Id, x => x);

			_awbAggregates = _awbRepository.GetAggregate(awbIds).ToDictionary(x => x.AirWaybillId, x => x);

			return GroupImpl(applications, groups);
		}

		private ApplicationGroup[] GroupImpl(IEnumerable<ApplicationListItem> applications,
											 IReadOnlyCollection<OrderType> groups)
		{
			var current = groups.First();
			var rest = groups.Except(new[] { current }).ToArray();

			switch (current)
			{
				case OrderType.AirWaybill:
					return ByAirWaybill(applications, rest);

				case OrderType.State:
					return ByState(applications, rest);

				case OrderType.LegalEntity:
					return ByLegalEntity(applications, rest);

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private static string GetAirWayBillDisplay(AirWaybillData airWaybillModel)
		{
			return string.Format("{0} &plusmn; {1}_{2} &plusmn; {3}_{4}{5}", airWaybillModel.Bill,
								 airWaybillModel.DepartureAirport,
								 airWaybillModel.DateOfDeparture.ToString("ddMMMyyyy").ToUpperInvariant(),
								 airWaybillModel.ArrivalAirport,
								 airWaybillModel.DateOfArrival.ToString("ddMMMyyyy").ToUpperInvariant(),
								 string.IsNullOrWhiteSpace(airWaybillModel.GTD)
									 ? ""
									 : string.Format(" &plusmn; {0}_{1}", Entities.GTD, airWaybillModel.GTD));
		}

		private ApplicationGroup[] ByLegalEntity(IEnumerable<ApplicationListItem> applications, OrderType[] groups)
		{
			return applications.GroupBy(x => x.ClientLegalEntity)
							   .Select(
									   grouping =>
										   GetApplicationGroup(grouping, groups, OrderHelper.LegalEntityFieldName,
															   g => g.Key))
							   .ToArray();
		}

		private ApplicationGroup[] ByState(IEnumerable<ApplicationListItem> applications, OrderType[] groups)
		{
			return applications.GroupBy(x => x.State.StateName)
							   .Select(
									   grouping =>
										   GetApplicationGroup(grouping, groups, OrderHelper.StateFieldName,
															   g => g.Key))
							   .ToArray();
		}

		private ApplicationGroup[] ByAirWaybill(IEnumerable<ApplicationListItem> applications, OrderType[] groups)
		{
			return applications
				.GroupBy(x => x.AirWaybillId ?? 0)
				.Select(grouping =>
							GetApplicationGroup(grouping, groups, OrderHelper.AwbFieldName,
												awb => _airWaybills.ContainsKey(awb.Key)
													? GetAirWayBillDisplay(_airWaybills[awb.Key])
													: ""))
				.ToArray();
		}

		private ApplicationGroup GetApplicationGroup<T>(
			IGrouping<T, ApplicationListItem> grouping,
			IReadOnlyCollection<OrderType> orders,
			string field,
			Func<IGrouping<T, ApplicationListItem>, string> getValue)
		{
			var count = 0;
			float weigth = 0;

			if (field == OrderHelper.AwbFieldName)
			{
				var id = (long)(object)grouping.Key;
				if (_awbAggregates.ContainsKey(id))
				{
					count = _awbAggregates[id].TotalCount;
					weigth = _awbAggregates[id].TotalWeight;
				}
			}
			else
			{
				count = grouping.Sum(y => y.Count ?? 0);
				weigth = grouping.Sum(y => y.Weigth ?? 0);
			}

			return new ApplicationGroup
			{
				aggregates = new ApplicationGroup.Aggregates(count, weigth),
				field = field,
				value = getValue(grouping),
				hasSubgroups = orders.Count > 0,
				items = orders.Count > 0
					? GroupImpl(grouping.ToArray(), orders).Cast<object>().ToArray()
					: grouping.Cast<object>().ToArray()
			};
		}
	}
}