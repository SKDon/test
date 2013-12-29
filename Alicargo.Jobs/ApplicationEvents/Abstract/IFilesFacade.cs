using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents.Abstract
{
	public interface IFilesFacade
	{
		FileHolder[] GetFiles(long applicationId, long? awbId, EventType type, byte[] data);
	}
}