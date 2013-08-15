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
	public sealed class ApplicationGrouper : IApplicationGrouper
	{
		private readonly IAwbRepository _awbRepository;

		public ApplicationGrouper(IAwbRepository awbRepository)
		{
			_awbRepository = awbRepository;
		}

		public ApplicationGroup[] Group(ApplicationListItem[] applications,
										IReadOnlyCollection<Order> groups)
		{
			var ids = applications.Select(x => x.AirWaybillId ?? 0).ToArray();
			var airWaybills = _awbRepository.Get(ids).ToDictionary(x => x.Id, x => x);

			return Group(applications, groups, airWaybills);
		}

		private ApplicationGroup[] Group(IEnumerable<ApplicationListItem> applications,
			IReadOnlyCollection<Order> groups, IDictionary<long, AirWaybillData> airWaybills)
		{
			var @group = groups.First();
			var orders = groups.Except(new[] { @group }).ToArray();

			switch (@group.OrderType)
			{
				case OrderType.AirWaybill:
					return
						applications.GroupBy(x => x.AirWaybillId ?? 0)
									.Select(x =>
										GetApplicationGroup(x, orders, "AirWaybill",
											g => airWaybills.ContainsKey(g.Key)
												? GetAirWayBillDisplay(airWaybills[g.Key])
												: "", airWaybills))
									.ToArray();

				case OrderType.State:
					return applications.GroupBy(x => x.State.StateName)
						.Select(x => GetApplicationGroup(x, orders, "State", g => g.Key, airWaybills))
						.ToArray();

				case OrderType.LegalEntity:
					return applications.GroupBy(x => x.ClientLegalEntity)
						.Select(x => GetApplicationGroup(x, orders, "ClientLegalEntity", g => g.Key, airWaybills))
						.ToArray();

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private static string GetAirWayBillDisplay(AirWaybillData airWaybillModel)
		{
			return string.Format("{0} &plusmn; {1}_{2} &plusmn; {3}_{4}{5}", airWaybillModel.Bill,
				airWaybillModel.DepartureAirport, airWaybillModel.DateOfDeparture.ToString("ddMMMyyyy").ToUpperInvariant(),
				airWaybillModel.ArrivalAirport, airWaybillModel.DateOfArrival.ToString("ddMMMyyyy").ToUpperInvariant(),
				string.IsNullOrWhiteSpace(airWaybillModel.GTD) ? "" : string.Format(" &plusmn; {0}_{1}", Entities.GTD, airWaybillModel.GTD));
		}

		private ApplicationGroup GetApplicationGroup<T>(
			IGrouping<T, ApplicationListItem> grouping,
			IReadOnlyCollection<Order> orders,
			string field,
			Func<IGrouping<T, ApplicationListItem>, string> getValue,
			IDictionary<long, AirWaybillData> airWaybills)
		{
			return new ApplicationGroup
			{
				// todo: 1. fix - get aggregation to all data, not only current set
				aggregates = new
				{
					Count = new { sum = grouping.Sum(y => y.Count ?? 0) },
					Weigth = new { sum = grouping.Sum(y => y.Weigth ?? 0) }
				},
				field = field,
				value = getValue(grouping),
				hasSubgroups = orders.Count > 0,
				items = orders.Count > 0
					? Group(grouping.ToArray(), orders, airWaybills).Cast<object>().ToArray()
					: grouping.Cast<object>().ToArray()
			};
		}
	}
}
