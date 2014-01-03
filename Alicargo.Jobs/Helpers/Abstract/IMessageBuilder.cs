using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.Helpers.Abstract
{
	public interface IMessageBuilder
	{
		EmailMessage[] Get(EventType type, EventData eventData);
	}
}