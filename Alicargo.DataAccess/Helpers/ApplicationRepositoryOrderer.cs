using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Helpers;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Helpers
{
	using OrderFunction = Func<IQueryable<Application>, bool, bool, IQueryable<Application>>;

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

		public IQueryable<Application> Order(IQueryable<Application> applications, IEnumerable<Order> orders)
		{
			if (orders == null)
			{
				return applications;
			}

			var isFirst = true;

			foreach (var order in orders)
			{
				applications = Map[order.OrderType](applications, order.Desc, isFirst);

				isFirst = false;
			}
			return applications;
		}

		private static IQueryable<Application> ByClientNic(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			return Order(applications, desc, isFirst, a => a.Client.Nic);
		}

		// todo: 3. test
		private static IQueryable<Application> ById(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			return Order(applications, desc, isFirst, x => x.Id);
		}

		// todo: 3. test
		private static IQueryable<Application> ByAirWaybillBill(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			var ordered = Order(applications, desc, isFirst, x => !x.AirWaybillId.HasValue);

			return desc
				? ordered.ThenByDescending(x => x.AirWaybill.CreationTimestamp)
				: ordered.ThenBy(x => x.AirWaybill.CreationTimestamp);
		}

		private static IQueryable<Application> ByLegalEntity(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			return Order(applications, desc, isFirst, a => a.Client.LegalEntity);
		}

		private static IQueryable<Application> ByState(IQueryable<Application> applications, bool desc, bool isFirst)
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