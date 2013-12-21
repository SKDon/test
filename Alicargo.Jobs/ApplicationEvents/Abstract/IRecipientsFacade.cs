using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.Application;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents.Abstract
{
	public interface IRecipientsFacade
	{
		RecipientData[] GetRecipients(ApplicationDetailsData application, ApplicationEventType type, byte[] data);
	}
}