using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.Helpers;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Services.Application
{
	public sealed class ApplicationGrouper : IApplicationGrouper
	{
		public ApplicationGroup[] Group(ApplicationListItem[] applications, IReadOnlyCollection<Order> groups)
		{
			var @group = groups.First();
			var orders = groups.Except(new[] { @group }).ToArray();

			switch (@group.OrderType)
			{
				case OrderType.ReferenceBill:
					return applications.GroupBy(x => new { x.AirWayBillSorter, x.AirWayBillDisplay })
						.Select(x => GetApplicationGroup(x, orders, "ReferenceBill", g => g.Key.AirWayBillDisplay))
						.ToArray();

				case OrderType.State:
					return applications.GroupBy(x => x.StateName)
						.Select(x => GetApplicationGroup(x, orders, "State", g => g.Key))
						.ToArray();

				case OrderType.LegalEntity:
					return applications.GroupBy(x => x.LegalEntity)
						.Select(x => GetApplicationGroup(x, orders, "LegalEntity", g => g.Key))
						.ToArray();

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		#region private

		private ApplicationGroup GetApplicationGroup<T>(
			IGrouping<T, ApplicationListItem> grouping,
			IReadOnlyCollection<Order> orders,
			string field,
			Func<IGrouping<T, ApplicationListItem>, string> getValue)
		{
			return new ApplicationGroup
			{
				aggregates = new
				{
					Count = new { sum = grouping.Sum(y => y.Count ?? 0) },
					Gross = new { sum = grouping.Sum(y => y.Gross ?? 0) }
				},
				field = field,
				value = getValue(grouping),
				hasSubgroups = orders.Count > 0,
				items = orders.Count > 0
					? Group(grouping.ToArray(), orders).Cast<object>().ToArray()
					: grouping.Cast<object>().ToArray()
			};
		}		

		#endregion
	}
}
