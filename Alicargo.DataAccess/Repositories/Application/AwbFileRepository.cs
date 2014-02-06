using System;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories.Application
{
	public sealed class AwbFileRepository : IAwbFileRepository
	{
		private readonly AlicargoDataContext _context;

		public AwbFileRepository(IUnitOfWork unitOfWork)
		{
			_context = (AlicargoDataContext)unitOfWork.Context;
		}

		public FileHolder GetAWBFile(long id)
		{
			return GetFile(
				x => x.Id == id && x.AWBFileData != null && x.AWBFileName != null,
				x => new FileHolder
				{
					Data = x.AWBFileData.ToArray(),
					Name = x.AWBFileName
				});
		}

		public FileHolder GetGTDFile(long id)
		{
			return GetFile(
				x => x.Id == id && x.GTDFileName != null && x.GTDFileData != null,
				x => new FileHolder
				{
					Name = x.GTDFileName,
					Data = x.GTDFileData.ToArray()
				});
		}

		public FileHolder GetPackingFile(long id)
		{
			return GetFile(
				x => x.Id == id && x.PackingFileData != null && x.PackingFileName != null,
				x => new FileHolder
				{
					Data = x.PackingFileData.ToArray(),
					Name = x.PackingFileName
				});
		}

		public FileHolder GTDAdditionalFile(long id)
		{
			return GetFile(
				x => x.Id == id && x.GTDAdditionalFileName != null && x.GTDAdditionalFileData != null,
				x => new FileHolder
				{
					Name = x.GTDAdditionalFileName,
					Data = x.GTDAdditionalFileData.ToArray()
				});
		}

		public FileHolder GetInvoiceFile(long id)
		{
			return GetFile(
				x => x.Id == id && x.InvoiceFileName != null && x.InvoiceFileData != null,
				x => new FileHolder
				{
					Name = x.InvoiceFileName,
					Data = x.InvoiceFileData.ToArray()
				});
		}

		private FileHolder GetFile(Expression<Func<AirWaybill, bool>> where,
			Expression<Func<AirWaybill, FileHolder>> selector)
		{
			return _context.AirWaybills.Where(where).Select(selector).FirstOrDefault();
		}
	}
}