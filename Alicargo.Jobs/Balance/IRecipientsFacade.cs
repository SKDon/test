using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.Balance
{
	public interface IRecipientsFacade
	{
		RecipientData[] GetRecipients(EventType type, long clientId);
	}
}