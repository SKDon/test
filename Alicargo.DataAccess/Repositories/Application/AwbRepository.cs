using System;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.DbContext;
using Alicargo.Utilities;

namespace Alicargo.DataAccess.Repositories.Application
{
	public sealed class AwbRepository : IAwbRepository
	{
		private readonly AlicargoDataContext _context;
		private readonly Expression<Func<AirWaybill, AirWaybillData>> _selector;

		public AwbRepository(IUnitOfWork unitOfWork)
		{
			_context = (AlicargoDataContext)unitOfWork.Context;

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
				GTDFileName = x.GTDFileName,
				Id = x.Id,
				StateId = x.StateId,
				StateChangeTimestamp = x.StateChangeTimestamp,
				AdditionalCost = x.AdditionalCost,
				BrokerCost = x.BrokerCost,
				CustomCost = x.CustomCost,
				FlightCost = x.FlightCost,
				TotalCostOfSenderForWeight = x.TotalCostOfSenderForWeight,
				AWBFileName = x.AWBFileName,
				DrawFileName = x.DrawFileName,
				InvoiceFileName = x.InvoiceFileName,
				PackingFileName = x.PackingFileName,
				GTDAdditionalFileName = x.GTDAdditionalFileName
			};
		}

		public long Add(AirWaybillData data, byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile,
			byte[] invoiceFile, byte[] awbFile, byte[] drawFile)
		{
			var entity = new AirWaybill();

			Map(data, entity, gtdFile, gtdAdditionalFile, packingFile, invoiceFile, awbFile, drawFile);

			_context.AirWaybills.InsertOnSubmit(entity);

			_context.SubmitChanges();

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

		public AirWaybillAggregate[] GetAggregate(long[] awbIds, long? clientId = null, long? senderId = null,
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

		public float? GetTotalWeightWithouAwb()
		{
			return _context.Applications.Where(x => !x.AirWaybillId.HasValue).Sum(x => x.Weight);
		}

		public decimal GetTotalValueWithouAwb()
		{
			return _context.Applications.Where(x => !x.AirWaybillId.HasValue).Sum(x => x.Value);
		}

		public float GetTotalVolumeWithouAwb()
		{
			return _context.Applications.Where(x => !x.AirWaybillId.HasValue).Sum(x => x.Volume);
		}

		public int? GetTotalCountWithouAwb()
		{
			return _context.Applications.Where(x => !x.AirWaybillId.HasValue).Sum(x => x.Count);
		}

		public string[] GetClientEmails(long id)
		{
			return _context.AirWaybills
				.Where(x => x.Id == id)
				.SelectMany(x => x.Applications)
				.Select(x => x.Client.Emails)
				.ToArray()
				.SelectMany(EmailsHelper.SplitAndTrimEmails)
				.ToArray();
		}

		public void SetAdditionalCost(long awbId, decimal? additionalCost)
		{
			var data = _context.AirWaybills.First(x => x.Id == awbId);
			data.AdditionalCost = additionalCost;

			_context.SubmitChanges();
		}

		public void Update(AirWaybillData data, byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile,
			byte[] invoiceFile, byte[] awbFile, byte[] drawFile)
		{
			var entity = _context.AirWaybills.First(x => x.Id == data.Id);

			Map(data, entity, gtdFile, gtdAdditionalFile, packingFile, invoiceFile, awbFile, drawFile);

			_context.SubmitChanges();
		}

		public void SetState(long airWaybillId, long stateId)
		{
			var airWaybill = _context.AirWaybills.First(x => x.Id == airWaybillId);
			airWaybill.StateId = stateId;
			airWaybill.StateChangeTimestamp = DateTimeProvider.Now;

			_context.SubmitChanges();
		}

		public void Delete(long id)
		{
			var airWaybill = _context.AirWaybills.First(x => x.Id == id);

			_context.AirWaybills.DeleteOnSubmit(airWaybill);

			_context.SubmitChanges();
		}

		private static void Map(AirWaybillData @from, AirWaybill to,
			byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile, byte[] invoiceFile, byte[] awbFile, byte[] drawFile)
		{
			if(to.Id == 0)
			{
				to.Id = @from.Id;
				to.CreationTimestamp = @from.CreationTimestamp;
				to.StateId = @from.StateId;
				to.StateChangeTimestamp = @from.StateChangeTimestamp;
			}

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

			SetFile(gtdFile, from.GTDFileName, bytes => to.GTDFileData = bytes, s => to.GTDFileName = s);

			SetFile(gtdAdditionalFile, from.GTDAdditionalFileName, bytes => to.GTDAdditionalFileData = bytes,
				s => to.GTDAdditionalFileName = s);

			SetFile(packingFile, from.PackingFileName, bytes => to.PackingFileData = bytes, s => to.PackingFileName = s);

			SetFile(invoiceFile, from.InvoiceFileName, bytes => to.InvoiceFileData = bytes, s => to.InvoiceFileName = s);

			SetFile(awbFile, from.AWBFileName, bytes => to.AWBFileData = bytes, s => to.AWBFileName = s);

			SetFile(drawFile, from.DrawFileName, bytes => to.DrawFileData = bytes, s => to.DrawFileName = s);
		}

		private static void SetFile(byte[] file, string name, Action<byte[]> setFile, Action<string> setName)
		{
			if(file != null && file.Length > 0)
			{
				setFile(file);
				setName(name);
			}
			else if(string.IsNullOrWhiteSpace(name))
			{
				setFile(null);
				setName(null);
			}
		}
	}
}