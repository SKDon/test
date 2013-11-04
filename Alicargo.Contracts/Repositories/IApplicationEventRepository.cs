using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface IApplicationEventRepository
	{
		void Add(long applicationId, ApplicationEventType eventType);
		ApplicationEventData GetNext();
	}
}
