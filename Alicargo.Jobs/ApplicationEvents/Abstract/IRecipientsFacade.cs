using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Jobs.ApplicationEvents.Entities;

namespace Alicargo.Jobs.ApplicationEvents.Abstract
{
	public interface IRecipientsFacade
	{
		Recipient[] GetRecipients(ApplicationDetailsData application, ApplicationEventType type, byte[] data);
	}
}