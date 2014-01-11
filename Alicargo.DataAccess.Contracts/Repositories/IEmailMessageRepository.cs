using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface IEmailMessageRepository
	{
		void Add(int partitionId, string @from, string[] to, string[] copyTo, string subject, string body, bool isBodyHtml,
			byte[] files);

		EmailMessageData GetNext(EmailMessageState state, int partitionId);
		void SetState(long id, EmailMessageState state);
	}
}