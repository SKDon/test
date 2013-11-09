using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface IEmailMessageRepository
	{
		void Add(EmailMessage message);
		EmailMessageData GetNext(EmailMessageState state, int partitionId);
		void SetState(long id, EmailMessageState state);
	}
}