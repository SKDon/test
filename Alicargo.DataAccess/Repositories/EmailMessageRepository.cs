using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class EmailMessageRepository : IEmailMessageRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public EmailMessageRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public void Add(int partitionId, string @from, string[] to, string[] copyTo, string subject, string body,
			bool isBodyHtml, byte[] files)
		{
			_executor.Execute("[dbo].[EmailMessage_Add]", new
			{
				State = EmailMessageState.New,
				partitionId,
				@from,
				To = EmailMessageData.Join(to),
				CopyTo = EmailMessageData.Join(copyTo),
				subject,
				body,
				isBodyHtml,
				files
			});
		}

		public EmailMessageData GetNext(EmailMessageState state, int partitionId)
		{
			return _executor.Query<EmailMessageData>("[dbo].[EmailMessage_GetNext]", new {state, partitionId});
		}

		public void SetState(long id, EmailMessageState state)
		{
			_executor.Execute("[dbo].[EmailMessage_SetState]", new {id, state});
		}
	}
}