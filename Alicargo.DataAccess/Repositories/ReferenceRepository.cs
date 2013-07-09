using System;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Core.Contracts;
using Alicargo.Core.Repositories;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class ReferenceRepository : BaseRepository, IReferenceRepository
	{
		private readonly Expression<Func<Reference, ReferenceData>> _selector;

		public ReferenceRepository(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			_selector = x => new ReferenceData
			{
				ArrivalAirport = x.ArrivalAirport,
				Bill = x.Bill,
				BrockerId = x.BrockerId,
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
				GTDAdditionalFileName = x.GTDAdditionalFileName
			};
		}

		public ReferenceData[] GetAll()
		{
			return Context.References.Select(_selector).ToArray();
		}

		public Func<long> Add(ReferenceData data, byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile, byte[] invoiceFile, byte[] awbFile)
		{
			var entity = new Reference();

			data.CopyTo(entity, gtdFile, gtdAdditionalFile, packingFile, invoiceFile, awbFile);

			Context.References.InsertOnSubmit(entity);

			return () => entity.Id;
		}

		public ReferenceData[] Get(params long[] ids)
		{
			return Context.References
				.Where(x => ids.Contains(x.Id))
				.Select(_selector)
				.ToArray();
		}

		// todo: test
		public long Count(long? brockerId = null)
		{
			return brockerId.HasValue
				? Context.References.Where(x => x.BrockerId == brockerId.Value).LongCount()
				: Context.References.LongCount();
		}

		// todo: test
		public ReferenceData[] GetRange(long skip, int take, long? brockerId = null)
		{
			var references = Context.References.AsQueryable();
			if (brockerId.HasValue)
			{
				references = references.Where(x => x.BrockerId == brockerId.Value);
			}
			return references.Skip((int)skip)
				.Take(take)
				.Select(_selector)
				.ToArray();
		}

		// todo: test
		public ReferenceAggregate[] GetAggregate(params long[] ids)
		{
			var data = Context.References
				.Where(x => ids.Contains(x.Id))
				.Select(x => new
				{
					x.Id,
					x.StateId,
					Data = x.Applications.Select(y => new
					{
						y.Gross,
						y.Value,
						y.Count
					})
				})
				.ToDictionary(x => new { x.Id, x.StateId }, x => x.Data.ToArray());

			return data
				.Select(x => new ReferenceAggregate
				{
					ReferenceId = x.Key.Id,
					TotalCount = x.Value.Sum(y => y.Count ?? 0),
					TotalWeight = x.Value.Sum(y => y.Gross ?? 0),
					StateId = x.Key.StateId
				})
				.ToArray();
		}

		public string[] GetClientEmails(long id)
		{
			return Context.References
				.Where(x => x.Id == id)
				.SelectMany(x => x.Applications)
				.Select(x => x.Client.Email)
				.ToArray();
		}

		// todo: test
		public void Update(ReferenceData data, byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile, byte[] invoiceFile, byte[] awbFile)
		{
			var entity = Context.References.First(x => x.Id == data.Id);

			data.CopyTo(entity, gtdFile, gtdAdditionalFile, packingFile, invoiceFile, awbFile);
		}

		#region Files // todo: test

		private FileHolder GetFile(Expression<Func<Reference, bool>> where, Expression<Func<Reference, FileHolder>> selector)
		{
			return Context.References.Where(where).Select(selector).FirstOrDefault();
		}

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

		#endregion

		// todo: test
		public void Delete(long id)
		{
			var reference = Context.References.First(x => x.Id == id);
			Context.References.DeleteOnSubmit(reference);
		}

		public void SetState(long referenceId, long stateId)
		{
			var reference = Context.References.First(x => x.Id == referenceId);
			reference.StateId = stateId;
			reference.StateChangeTimestamp = DateTimeOffset.UtcNow;
		}
	}
}