using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public interface IRecipientsFacade
	{
		Recipient[] GetRecipients(ApplicationDetailsData application, ApplicationEventType type, byte[] data);
	}
}