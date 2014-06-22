using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.DataAccess.Contracts.Contracts.Awb;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.DbContext;
using Alicargo.Utilities;

namespace Alicargo.DataAccess.Repositories.Application
{
	public sealed class AwbRepository : IAwbRepository
	{
		private readonly AlicargoDataContext _context;
		private readonly Expression<Func<AirWaybill, AirWaybillData>> _selector;

		public AwbRepository(IDbConnection connection)
		{
			_context = new AlicargoDataContext(connection);

			_selector = x => new AirWaybillData
			{
				ArrivalAirport = x.ArrivalAirport,
				Bill = x.Bill,
				BrokerId = x.BrokerId,
				CreationTimestamp = x.CreationTimestamp,
				DateOfArrival = x.DateOfArrival,
				DateOfDeparture = x.DateOfDeparture,
				DepartureAirport = x.DepartureAirport,
				GTD = x.GTD,
				Id = x.Id,
				StateId = x.StateId,
				StateChangeTimestamp = x.StateChangeTimestamp,
				AdditionalCost = x.AdditionalCost,
				BrokerCost = x.BrokerCost,
				CustomCost = x.CustomCost,
				FlightCost = x.FlightCost,
				TotalCostOfSenderForWeight = x.TotalCostOfSenderForWeight,
				IsActive = x.IsActive
			};
		}

		public long Add(AirWaybillEditData data, long stateId)
		{
			var entity = new AirWaybill
			{
				CreationTimestamp = DateTimeProvider.Now,
				StateChangeTimestamp = DateTimeProvider.Now,
				StateId = stateId,
				IsActive = true
			};

			Map(data, entity);

			_context.AirWaybills.InsertOnSubmit(entity);

			_context.SaveChanges();

			return entity.Id;
		}

		public AirWaybillData[] Get(params long[] ids)
		{
			var airWaybills = ids.Length == 0
				? _context.AirWaybills.AsQueryable()
				: _context.AirWaybills.Where(x => ids.Contains(x.Id));

			return airWaybills.Select(_selector).ToArray();
		}

		public long Count(long? brokerId = null)
		{
			return brokerId.HasValue
				? _context.AirWaybills.Where(x => x.BrokerId == brokerId.Value).LongCount()
				: _context.AirWaybills.LongCount();
		}

		public EmailData[] GetForwarderEmails(long awbId)
		{
			return _context.AirWaybills
				.Where(x => x.Id == awbId)
				.SelectMany(x => x.Applications)
				.Select(x => x.Forwarder)
				.Distinct()
				.Select(x => new EmailData
				{
					Email = x.Email,
					Language = x.User.TwoLetterISOLanguageName
				})
				.ToArray();
		}

		public AirWaybillData[] GetRange(int take, long skip, long? brokerId = null)
		{
			var airWaybills = _context.AirWaybills.OrderByDescending(x => x.CreationTimestamp).AsQueryable();
			if(brokerId.HasValue)
			{
				airWaybills = airWaybills.Where(x => x.BrokerId == brokerId.Value);
			}
			return airWaybills.Skip((int)skip)
				.Take(take)
				.Select(_selector)
				.ToArray();
		}

		public EmailData[] GetSenderEmails(long awbId)
		{
			return _context.AirWaybills
				.Where(x => x.Id == awbId)
				.SelectMany(x => x.Applications)
				.Select(x => x.Sender)
				.Distinct()
				.Select(x => new EmailData
				{
					Email = x.Email,
					Language = x.User.TwoLetterISOLanguageName
				})
				.ToArray();
		}

		public AirWaybillAggregate[] GetAggregate(
			long[] awbIds, long? clientId = null, long? senderId = null,
			long? forwarderId = null, long? carrierId = null)
		{
			var waybills = awbIds.Length == 0
				? _context.AirWaybills.AsQueryable()
				: _context.AirWaybills.Where(x => awbIds.Contains(x.Id));

			var data = waybills.Select(x => new
			{
				x.Id,
				x.StateId,
				Data = x.Applications
					.Where(y
						=> (!forwarderId.HasValue || y.ForwarderId == forwarderId)
						   && (!clientId.HasValue || y.ClientId == clientId)
						   && (!senderId.HasValue || y.SenderId == senderId)
						   && (!carrierId.HasValue || y.Transit.CarrierId == carrierId))
					.Select(y => new
					{
						y.Weight,
						y.Value,
						y.Count,
						y.Volume
					})
			}).ToDictionary(x => new { x.Id, x.StateId }, x => x.Data.ToArray());

			return data.Select(x => new AirWaybillAggregate
			{
				AirWaybillId = x.Key.Id,
				TotalCount = x.Value.Sum(y => y.Count ?? 0),
				TotalWeight = x.Value.Sum(y => y.Weight ?? 0),
				TotalValue = x.Value.Sum(y => y.Value),
				TotalVolume = x.Value.Sum(y => y.Volume),
				StateId = x.Key.StateId
			}).ToArray();
		}

		public EmailData[] GetCarrierEmails(long awbId)
		{
			return _context.AirWaybills
				.Where(x => x.Id == awbId)
				.SelectMany(x => x.Applications)
				.Select(x => x.Transit.Carrier)
				.Distinct()
				.Select(x => new EmailData
				{
					Email = x.Email,
					Language = x.User.TwoLetterISOLanguageName
				})
				.ToArray();
		}

		public float GetTotalWeightWithouAwb(
			long? clientId = null, long? senderId = null,
			long? forwarderId = null, long? carrierId = null)
		{
			return SelectApplicationsWithoutAwb(x => x.Weight, clientId, senderId, forwarderId, carrierId).Sum() ?? 0;
		}

		public decimal GetTotalValueWithouAwb(
			long? clientId = null, long? senderId = null,
			long? forwarderId = null, long? carrierId = null)
		{
			return SelectApplicationsWithoutAwb(x => x.Value, clientId, senderId, forwarderId, carrierId).Sum();
		}

		public float GetTotalVolumeWithouAwb(
			long? clientId = null, long? senderId = null,
			long? forwarderId = null, long? carrierId = null)
		{
			return SelectApplicationsWithoutAwb(x => x.Volume, clientId, senderId, forwarderId, carrierId).Sum();
		}

		public int GetTotalCountWithouAwb(
			long? clientId = null, long? senderId = null,
			long? forwarderId = null, long? carrierId = null)
		{
			return SelectApplicationsWithoutAwb(x => x.Count, clientId, senderId, forwarderId, carrierId).Sum() ?? 0;
		}

		public EmailData[] GetClientEmails(long awbId)
		{
			return _context.AirWaybills
				.Where(x => x.Id == awbId)
				.SelectMany(x => x.Applications)
				.Select(x => x.Client)
				.Distinct()
				.Select(x => new EmailData
				{
					Email = x.Emails,
					Language = x.User.TwoLetterISOLanguageName
				})
				.ToArray();
		}

		public void SetAdditionalCost(long awbId, decimal? additionalCost)
		{
			var data = _context.AirWaybills.First(x => x.Id == awbId);
			data.AdditionalCost = additionalCost;

			_context.SaveChanges();
		}

		public void Update(long id, AirWaybillEditData data)
		{
			var entity = _context.AirWaybills.First(x => x.Id == id);

			Map(data, entity);

			_context.SaveChanges();
		}

		public void SetState(long airWaybillId, long stateId)
		{
			var airWaybill = _context.AirWaybills.First(x => x.Id == airWaybillId);
			airWaybill.StateId = stateId;
			airWaybill.StateChangeTimestamp = DateTimeProvider.Now;

			_context.SaveChanges();
		}

		public void Delete(long id)
		{
			var airWaybill = _context.AirWaybills.First(x => x.Id == id);

			_context.AirWaybills.DeleteOnSubmit(airWaybill);

			_context.SaveChanges();
		}

		private T[] SelectApplicationsWithoutAwb<T>(
			Expression<Func<DbContext.Application, T>> selector,
			long? clientId = null, long? senderId = null, long? forwarderId = null, long? carrierId = null)
		{
			return _context.Applications
				.Where(y => !y.AirWaybillId.HasValue
				            && (!forwarderId.HasValue || y.ForwarderId == forwarderId)
				            && (!clientId.HasValue || y.ClientId == clientId)
				            && (!senderId.HasValue || y.SenderId == senderId)
				            && (!carrierId.HasValue || y.Transit.CarrierId == carrierId))
				.Select(selector)
				.ToArray();
		}

		private static void Map(AirWaybillEditData @from, AirWaybill to)
		{
			to.ArrivalAirport = @from.ArrivalAirport;
			to.Bill = @from.Bill;
			to.BrokerId = @from.BrokerId;
			to.DateOfArrival = @from.DateOfArrival;
			to.DateOfDeparture = @from.DateOfDeparture;
			to.DepartureAirport = @from.DepartureAirport;
			to.GTD = @from.GTD;
			to.AdditionalCost = @from.AdditionalCost;
			to.BrokerCost = @from.BrokerCost;
			to.CustomCost = @from.CustomCost;
			to.FlightCost = @from.FlightCost;
			to.TotalCostOfSenderForWeight = from.TotalCostOfSenderForWeight;
		}
	}
}