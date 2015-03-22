using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.Jobs.Helpers.Abstract
{
	public interface IMessageBuilder
	{
		EmailMessage[] Get(EventType type, EventData eventData);
	}
}