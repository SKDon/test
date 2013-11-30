using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Helpers;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Helpers
{
	using OrderFunction = Func<IQueryable<Application>, bool, bool, IOrderedQueryable<Application>>;

	internal sealed class ApplicationRepositoryOrderer : IApplicationRepositoryOrderer
	{
		private static readonly IDictionary<OrderType, OrderFunction> Map = new Dictionary<OrderType, OrderFunction>
		{
			{OrderType.Id, ById},
			{OrderType.AirWaybill, ByAirWaybillBill},
			{OrderType.State, ByState},
			{OrderType.ClientNic, ByClientNic},
			{OrderType.LegalEntity, ByLegalEntity}
		};

		public IQueryable<Application> Order(IQueryable<Application> applications, IList<Order> orders)
		{
			if (orders == null || orders.Count == 0)
			{
				return applications;
			}

			var isFirst = true;

			foreach (var order in orders)
			{
				applications = Map[order.OrderType](applications, order.Desc, isFirst);

				isFirst = false;
			}

			return AdditionalOrdering((IOrderedQueryable<Application>)applications, orders);
		}

		private static IOrderedQueryable<Application> AdditionalOrdering(IOrderedQueryable<Application> applications,
			IEnumerable<Order> orders)
		{
			if (orders.All(x => x.OrderType != OrderType.Id))
			{
				applications = applications.ThenByDescending(x => x.Id);
			}

			return applications;
		}

		private static IOrderedQueryable<Application> ByClientNic(IQueryable<Application> applications, bool desc,
			bool isFirst)
		{
			return Order(applications, desc, isFirst, a => a.Client.Nic);
		}

		private static IOrderedQueryable<Application> ById(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			return Order(applications, desc, isFirst, x => x.Id);
		}

		private static IOrderedQueryable<Application> ByAirWaybillBill(IQueryable<Application> applications, bool desc,
			bool isFirst)
		{
			var ordered = Order(applications, desc, isFirst, x => !x.AirWaybillId.HasValue);

			return desc
				? ordered.ThenByDescending(x => x.AirWaybill.CreationTimestamp)
				: ordered.ThenBy(x => x.AirWaybill.CreationTimestamp);
		}

		private static IOrderedQueryable<Application> ByLegalEntity(IQueryable<Application> applications, bool desc,
			bool isFirst)
		{
			return Order(applications, desc, isFirst, a => a.Client.LegalEntity);
		}

		private static IOrderedQueryable<Application> ByState(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			return Order(applications, desc, isFirst, a => a.State.Name);
		}

		private static IOrderedQueryable<Application> Order<T>(
			IQueryable<Application> applications, bool desc, bool isFirst,
			Expression<Func<Application, T>> expression)
		{
			if (isFirst)
			{
				return desc
					? applications.OrderByDescending(expression)
					: applications.OrderBy(expression);
			}

			var ordered = (IOrderedQueryable<Application>)applications;
			return desc
				? ordered.ThenByDescending(expression)
				: ordered.ThenBy(expression);
		}
	}
}