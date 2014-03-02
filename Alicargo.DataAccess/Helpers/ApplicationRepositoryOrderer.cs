using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Helpers
{
	using OrderFunction = Func<IQueryable<Application>, bool, bool, IOrderedQueryable<Application>>;

	internal sealed class ApplicationRepositoryOrderer : IApplicationRepositoryOrderer
	{
		private static readonly IDictionary<OrderType, OrderFunction> Map = new Dictionary<OrderType, OrderFunction>
		{
			{ OrderType.Id, ById },
			{ OrderType.AirWaybill, ByAirWaybillBill },
			{ OrderType.State, ByState },
			{ OrderType.MethodOfTransit, ByMethodOfTransit },
			{ OrderType.Client, ByClientNic },
			{ OrderType.Country, ByCountry },
			{ OrderType.Factory, ByFactory },
			{ OrderType.Mark, ByMark },
			{ OrderType.Sender, BySender },
			{ OrderType.Forwarder, ByForwarder },
			{ OrderType.Carrier, ByCarrier },
			{ OrderType.City, ByCity }
		};

		private static IOrderedQueryable<Application> ByMethodOfTransit(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			return Order(applications, desc, isFirst, a => a.Transit.MethodOfTransitId);
		}

		public IQueryable<Application> Order(IQueryable<Application> applications, IList<Order> orders)
		{
			if(orders == null || orders.Count == 0)
			{
				return applications;
			}

			var isFirst = true;

			foreach(var order in orders)
			{
				applications = Map[order.OrderType](applications, order.Desc, isFirst);

				isFirst = false;
			}

			return AdditionalOrdering((IOrderedQueryable<Application>)applications, orders);
		}

		private static IOrderedQueryable<Application> AdditionalOrdering(IOrderedQueryable<Application> applications,
			IEnumerable<Order> orders)
		{
			if(orders.All(x => x.OrderType != OrderType.Id))
			{
				applications = applications.ThenByDescending(x => x.Id);
			}

			return applications;
		}

		private static IOrderedQueryable<Application> ByAirWaybillBill(IQueryable<Application> applications, bool desc,
			bool isFirst)
		{
			var ordered = Order(applications, desc, isFirst, x => !x.AirWaybillId.HasValue);

			return desc
				? ordered.ThenByDescending(x => x.AirWaybill.CreationTimestamp)
				: ordered.ThenBy(x => x.AirWaybill.CreationTimestamp);
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

		private static IOrderedQueryable<Application> ByCountry(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			return Order(applications, desc, isFirst, x => x.Country.Position);
		}

		private static IOrderedQueryable<Application> ByFactory(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			return Order(applications, desc, isFirst, x => x.FactoryName);
		}

		private static IOrderedQueryable<Application> ByMark(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			return Order(applications, desc, isFirst, x => x.MarkName);
		}

		private static IOrderedQueryable<Application> BySender(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			return Order(applications, desc, isFirst, x => x.Sender.Name);
		}

		private static IOrderedQueryable<Application> ByForwarder(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			return Order(applications, desc, isFirst, x => x.Forwarder.Name);
		}

		private static IOrderedQueryable<Application> ByCarrier(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			return Order(applications, desc, isFirst, x => x.Transit.Carrier.Name);
		}

		private static IOrderedQueryable<Application> ByCity(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			return Order(applications, desc, isFirst, x => x.Transit.City.Position);
		}

		private static IOrderedQueryable<Application> ByState(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			return Order(applications, desc, isFirst, a => a.State.Name);
		}

		private static IOrderedQueryable<Application> Order<T>(
			IQueryable<Application> applications, bool desc, bool isFirst,
			Expression<Func<Application, T>> expression)
		{
			if(isFirst)
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