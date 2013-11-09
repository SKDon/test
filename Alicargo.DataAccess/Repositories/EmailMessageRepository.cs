using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class EmailMessageRepository : IEmailMessageRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public EmailMessageRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public void Add(Message message)
		{
			throw new System.NotImplementedException();
		}
	}
}
