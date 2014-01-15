using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface IEmailMessageRepository
	{
		void Add(int partitionId, string @from, string[] to, string[] copyTo, string subject, string body, bool isBodyHtml,
			byte[] files);

		EmailMessageData GetNext(EmailMessageState state, int partitionId);
		void SetState(long id, EmailMessageState state);
	}
}