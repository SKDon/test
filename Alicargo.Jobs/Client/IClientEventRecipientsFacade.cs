using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.Jobs.Client
{
	public interface IClientEventRecipientsFacade
	{
		RecipientData[] GetRecipients(EventType type, long clientId);
	}
}