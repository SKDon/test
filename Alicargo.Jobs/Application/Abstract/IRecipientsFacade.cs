using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.Jobs.Application.Abstract
{
	public interface IRecipientsFacade
	{
		RecipientData[] GetRecipients(EventType type, EventDataForEntity data);
	}
}