using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Helpers;
using Alicargo.Core.Contracts;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;
using Resources;

namespace Alicargo.Services.Application
{
	public sealed class ApplicationGrouper : IApplicationGrouper
	{
		public ApplicationGroup[] Group(ApplicationListItem[] applications,
			IReadOnlyCollection<Order> groups, IDictionary<long, ReferenceData> references)
		{
			var @group = groups.First();
			var orders = groups.Except(new[] { @group }).ToArray();

			switch (@group.OrderType)
			{
				case OrderType.AirWaybill:
					return
						applications.GroupBy(x => x.Data.AirWaybillId ?? 0)
									.Select(x =>
										GetApplicationGroup(x, orders, "AirWaybill",
											g => references.ContainsKey(g.Key)
												? GetAirWayBillDisplay(references[g.Key])
												: "", references))
									.ToArray();

				case OrderType.State:
					return applications.GroupBy(x => x.State.StateName)
						.Select(x => GetApplicationGroup(x, orders, "State", g => g.Key, references))
						.ToArray();

				case OrderType.LegalEntity:
					return applications.GroupBy(x => x.Data.ClientLegalEntity)
						.Select(x => GetApplicationGroup(x, orders, "LegalEntity", g => g.Key, references))
						.ToArray();

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		#region private

		private static string GetAirWayBillDisplay(ReferenceData referenceData)
		{
			return string.Format("{0} &plusmn; {1}_{2} &plusmn; {3}_{4}{5}", referenceData.Bill,
				referenceData.DepartureAirport, referenceData.DateOfDeparture.ToString("ddMMMyyyy").ToUpperInvariant(),
				referenceData.ArrivalAirport, referenceData.DateOfArrival.ToString("ddMMMyyyy").ToUpperInvariant(),
				string.IsNullOrWhiteSpace(referenceData.GTD) ? "" : string.Format(" &plusmn; {0}_{1}", Entities.GTD, referenceData.GTD));
		}

		private ApplicationGroup GetApplicationGroup<T>(
			IGrouping<T, ApplicationListItem> grouping,
			IReadOnlyCollection<Order> orders,
			string field,
			Func<IGrouping<T, ApplicationListItem>, string> getValue,
			IDictionary<long, ReferenceData> references)
		{
			return new ApplicationGroup
			{
				// todo: 1. fix - get aggregation to all data, not only current set
				aggregates = new
				{
					Count = new { sum = grouping.Sum(y => y.Data.Count ?? 0) },
					Weigth = new { sum = grouping.Sum(y => y.Data.Weigth ?? 0) }
				},
				field = field,
				value = getValue(grouping),
				hasSubgroups = orders.Count > 0,
				items = orders.Count > 0
					? Group(grouping.ToArray(), orders, references).Cast<object>().ToArray()
					: grouping.Cast<object>().ToArray()
			};
		}

		#endregion
	}
}
