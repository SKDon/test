using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class ClientFileRepository : IClientFileRepository
	{
		private readonly AlicargoDataContext _context;
		private readonly ISqlProcedureExecutor _executor;

		public ClientFileRepository(IUnitOfWork unitOfWork, ISqlProcedureExecutor executor)
		{
			_executor = executor;
			_context = (AlicargoDataContext)unitOfWork.Context;
		}

		public void SetCalculationExcel(long clientId, byte[] bytes)
		{
			var client = _context.Clients.First(x => x.Id == clientId);

			client.CalculationFileData = bytes;
		}

		public FileHolder GetClientDocument(long clientId)
		{
			return _executor.Get<FileHolder>("[dbo].[GetClientContract]", new { clientId });
		}

		public FileHolder GetCalculationFile(long clientId)
		{
			return _context.Clients.Where(x => x.Id == clientId && x.CalculationFileData != null)
						   .Select(x => new FileHolder
						   {
							   Data = x.CalculationFileData.ToArray(),
							   Name = "calculations.xlsx"
						   }).FirstOrDefault();
		}
	}
}