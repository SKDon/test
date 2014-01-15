using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.Jobs.Balance
{
	public interface IRecipientsFacade
	{
		RecipientData[] GetRecipients(EventType type, long clientId);
	}
}