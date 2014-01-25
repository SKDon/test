using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents.Abstract
{
	public interface IRecipientsFacade
	{
		RecipientData[] GetRecipients(ApplicationExtendedData application, EventType type, byte[] data);
	}
}