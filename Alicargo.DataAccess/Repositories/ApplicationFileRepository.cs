using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class ApplicationFileRepository : IApplicationFileRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		#region Obsolete

		private readonly AlicargoDataContext _context;

		public ApplicationFileRepository(IUnitOfWork unitOfWork, ISqlProcedureExecutor executor)
		{
			_executor = executor;
			_context = (AlicargoDataContext)unitOfWork.Context;
		}

		public FileHolder GetInvoiceFile(long id)
		{
			return GetFile(x => x.Id == id && x.InvoiceFileName != null && x.InvoiceFileData != null,
				application => new FileHolder
				{
					Name = application.InvoiceFileName,
					Data = application.InvoiceFileData.ToArray()
				});
		}

		public FileHolder GetSwiftFile(long id)
		{
			return GetFile(x => x.Id == id && x.SwiftFileName != null && x.SwiftFileData != null,
				application => new FileHolder
				{
					Name = application.SwiftFileName,
					Data = application.SwiftFileData.ToArray()
				});
		}

		public FileHolder GetCPFile(long id)
		{
			return GetFile(x => x.Id == id && x.CPFileName != null && x.CPFileData != null,
				application => new FileHolder
				{
					Name = application.CPFileName,
					Data = application.CPFileData.ToArray()
				});
		}

		public FileHolder GetDeliveryBillFile(long id)
		{
			return GetFile(x => x.Id == id && x.DeliveryBillFileName != null && x.DeliveryBillFileData != null,
				application => new FileHolder
				{
					Name = application.DeliveryBillFileName,
					Data = application.DeliveryBillFileData.ToArray()
				});
		}

		public FileHolder GetTorg12File(long id)
		{
			return GetFile(x => x.Id == id && x.Torg12FileName != null && x.Torg12FileData != null,
				application => new FileHolder
				{
					Name = application.Torg12FileName,
					Data = application.Torg12FileData.ToArray()
				});
		}

		public FileHolder GetPackingFile(long id)
		{
			return GetFile(x => x.Id == id && x.PackingFileName != null && x.PackingFileData != null,
				application => new FileHolder
				{
					Name = application.PackingFileName,
					Data = application.PackingFileData.ToArray()
				});
		}

		private FileHolder GetFile(Expression<Func<Application, bool>> where,
			Expression<Func<Application, FileHolder>> selector)
		{
			return _context.Applications.Where(where).Select(selector).FirstOrDefault();
		}

		#endregion

		public IReadOnlyDictionary<long, string> GetFileNames(long applicationId, ApplicationFileType type)
		{
			var idsTable = TableParameters.GeIdsTable("AppIds", new[] { applicationId });
			var parameters = new TableParameters(new { TypeId = type }, idsTable);

			return _executor.Array<dynamic>("[dbo].[ApplicationFile_GetNames]", parameters)
				.ToDictionary(x => (long)x.Id, x => (string)x.Name);
		}

		public IReadOnlyDictionary<long, FileInfo[]> GetFileNames(long[] applicationIds, ApplicationFileType type)
		{
			var idsTable = TableParameters.GeIdsTable("AppIds", applicationIds);
			var parameters = new TableParameters(new { TypeId = type }, idsTable);

			return _executor.Array<dynamic>("[dbo].[ApplicationFile_GetNames]", parameters)
				.GroupBy(x => (long)x.ApplicationId)
				.ToDictionary(a => a.Key, a => a.Select(x => new FileInfo
				{
					Id = x.Id,
					Name = x.Name
				}).ToArray());
		}

		public FileHolder Get(long id)
		{
			return _executor.Query<FileHolder>("[dbo].[ApplicationFile_Get]", new { id });
		}

		public long Add(long applicationId, ApplicationFileType type, string name, byte[] data)
		{
			return _executor.Query<long>("[dbo].[ApplicationFile_Add]", new { applicationId, TypeId = type, name, data });
		}

		public void Delete(long id)
		{
			_executor.Execute("[dbo].[ApplicationFile_Delete]", new { id });
		}
	}
}