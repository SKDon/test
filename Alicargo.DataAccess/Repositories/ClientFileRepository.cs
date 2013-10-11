using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class ClientFileRepository : IClientFileRepository
	{
		private readonly AlicargoDataContext _context;

		public ClientFileRepository(IUnitOfWork unitOfWork)
		{
			_context = (AlicargoDataContext)unitOfWork.Context;
		}

		public FileHolder GetCalculationFile(long clientId)
		{
			return _context.Clients.Where(x => x.Id == clientId && x.CalculationFileData != null)
						   .Select(x => new FileHolder
						   {
							   FileData = x.CalculationFileData.ToArray(),
							   FileName = "calculations.xlsx"
						   }).FirstOrDefault();
		}
	}
}