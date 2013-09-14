using System;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class AwbRepository : BaseRepository, IAwbRepository
	{
		private readonly Expression<Func<AirWaybill, AirWaybillData>> _selector;

		public AwbRepository(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
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
				AWBFileName = x.AWBFileName,
				InvoiceFileName = x.InvoiceFileName,
				PackingFileName = x.PackingFileName,
				StateId = x.StateId,
				StateChangeTimestamp = x.StateChangeTimestamp,
				GTDAdditionalFileName = x.GTDAdditionalFileName,
				AdditionalCost = x.AdditionalCost,
				BrokerCost = x.BrokerCost,
				CustomCost = x.CustomCost,
				FlightCost = x.FlightCost,
				TotalCostOfSenderForWeight = x.TotalCostOfSenderForWeight
			};
		}

		public Func<long> Add(AirWaybillData data, byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile,
							  byte[] invoiceFile, byte[] awbFile)
		{
			var entity = new AirWaybill();

			Map(data, entity, gtdFile, gtdAdditionalFile, packingFile, invoiceFile, awbFile);

			Context.AirWaybills.InsertOnSubmit(entity);

			return () => entity.Id;
		}

		public AirWaybillData[] Get(params long[] ids)
		{
			var airWaybills = ids.Length == 0
				? Context.AirWaybills.AsQueryable()
				: Context.AirWaybills.Where(x => ids.Contains(x.Id));

			return airWaybills.Select(_selector).ToArray();
		}

		public long Count(long? brokerId = null)
		{
			return brokerId.HasValue
				? Context.AirWaybills.Where(x => x.BrokerId == brokerId.Value).LongCount()
				: Context.AirWaybills.LongCount();
		}

		public AirWaybillData[] GetRange(int take, long skip, long? brokerId = null)
		{
			var airWaybills = Context.AirWaybills.OrderByDescending(x => x.CreationTimestamp).AsQueryable();
			if (brokerId.HasValue)
			{
				airWaybills = airWaybills.Where(x => x.BrokerId == brokerId.Value);
			}
			return airWaybills.Skip((int)skip)
							  .Take(take)
							  .Select(_selector)
							  .ToArray();
		}

		public AirWaybillAggregate[] GetAggregate(params long[] ids)
		{
			var waybills = ids.Length == 0
				? Context.AirWaybills.AsQueryable()
				: Context.AirWaybills.Where(x => ids.Contains(x.Id));

			var data = waybills.Select(x => new
			{
				x.Id,
				x.StateId,
				Data = x.Applications.Select(y => new
				{
					y.Weight,
					y.Value,
					y.Count
				})
			}).ToDictionary(x => new { x.Id, x.StateId }, x => x.Data.ToArray());

			return data.Select(x => new AirWaybillAggregate
			{
				AirWaybillId = x.Key.Id,
				TotalCount = x.Value.Sum(y => y.Count ?? 0),
				TotalWeight = x.Value.Sum(y => y.Weight ?? 0),
				StateId = x.Key.StateId
			}).ToArray();
		}

		// todo: 2. bb-test
		public float? GetTotalWeightWithouAwb()
		{
			return Context.Applications.Where(x => !x.AirWaybillId.HasValue).Sum(x => x.Weight);
		}

		// todo: 2. bb-test
		public int? GetTotalCountWithouAwb()
		{
			return Context.Applications.Where(x => !x.AirWaybillId.HasValue).Sum(x => x.Count);
		}

		public string[] GetClientEmails(long id)
		{
			return Context.AirWaybills
						  .Where(x => x.Id == id)
						  .SelectMany(x => x.Applications)
						  .Select(x => x.Client.Email)
						  .ToArray();
		}

		public void Update(AirWaybillData data, byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile,
						   byte[] invoiceFile, byte[] awbFile)
		{
			var entity = Context.AirWaybills.First(x => x.Id == data.Id);

			Map(data, entity, gtdFile, gtdAdditionalFile, packingFile, invoiceFile, awbFile);
		}

		public void Delete(long id)
		{
			var airWaybill = Context.AirWaybills.First(x => x.Id == id);

			Context.AirWaybills.DeleteOnSubmit(airWaybill);
		}

		public void SetState(long airWaybillId, long stateId)
		{
			var airWaybill = Context.AirWaybills.First(x => x.Id == airWaybillId);
			airWaybill.StateId = stateId;
			airWaybill.StateChangeTimestamp = DateTimeOffset.UtcNow;
		}

		#region Files

		public FileHolder GetAWBFile(long id)
		{
			return GetFile(
						   x => x.Id == id && x.AWBFileData != null && x.AWBFileName != null,
						   x => new FileHolder
						   {
							   FileData = x.AWBFileData.ToArray(),
							   FileName = x.AWBFileName
						   });
		}

		public FileHolder GetGTDFile(long id)
		{
			return GetFile(
						   x => x.Id == id && x.GTDFileName != null && x.GTDFileData != null,
						   x => new FileHolder
						   {
							   FileName = x.GTDFileName,
							   FileData = x.GTDFileData.ToArray()
						   });
		}

		public FileHolder GetPackingFile(long id)
		{
			return GetFile(
						   x => x.Id == id && x.PackingFileData != null && x.PackingFileName != null,
						   x => new FileHolder
						   {
							   FileData = x.PackingFileData.ToArray(),
							   FileName = x.PackingFileName
						   });
		}

		public FileHolder GTDAdditionalFile(long id)
		{
			return GetFile(
						   x => x.Id == id && x.GTDAdditionalFileName != null && x.GTDAdditionalFileData != null,
						   x => new FileHolder
						   {
							   FileName = x.GTDAdditionalFileName,
							   FileData = x.GTDAdditionalFileData.ToArray()
						   });
		}

		public FileHolder GetInvoiceFile(long id)
		{
			return GetFile(
						   x => x.Id == id && x.InvoiceFileName != null && x.InvoiceFileData != null,
						   x => new FileHolder
						   {
							   FileName = x.InvoiceFileName,
							   FileData = x.InvoiceFileData.ToArray()
						   });
		}

		private FileHolder GetFile(Expression<Func<AirWaybill, bool>> where,
								   Expression<Func<AirWaybill, FileHolder>> selector)
		{
			return Context.AirWaybills.Where(where).Select(selector).FirstOrDefault();
		}

		#endregion

		private static void Map(AirWaybillData @from, AirWaybill to,
								byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile, byte[] invoiceFile, byte[] awbFile)
		{
			if (to.Id == 0)
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

			// todo: 2. separate repository for files
			FileDataHelper.SetFile(gtdFile, from.GTDFileName,
								   bytes => to.GTDFileData = bytes, s => to.GTDFileName = s);

			FileDataHelper.SetFile(gtdAdditionalFile, from.GTDAdditionalFileName,
								   bytes => to.GTDAdditionalFileData = bytes, s => to.GTDAdditionalFileName = s);

			FileDataHelper.SetFile(packingFile, from.PackingFileName,
								   bytes => to.PackingFileData = bytes, s => to.PackingFileName = s);

			FileDataHelper.SetFile(invoiceFile, from.InvoiceFileName,
								   bytes => to.InvoiceFileData = bytes, s => to.InvoiceFileName = s);

			FileDataHelper.SetFile(awbFile, from.AWBFileName,
								   bytes => to.AWBFileData = bytes, s => to.AWBFileName = s);
		}
	}
}