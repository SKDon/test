using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
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

		public void Add(EmailMessage message)
		{
			throw new System.NotImplementedException();
		}

		public EmailMessageData GetNext(EmailMessageState state, int partitionId)
		{
			throw new System.NotImplementedException();
		}

		public void SetState(long id, EmailMessageState state)
		{
			throw new System.NotImplementedException();
		}
	}
}
