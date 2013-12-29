using Alicargo.Contracts.Enums;

namespace Alicargo.Core.Event
{
	public interface IEventFacadeForApplication
	{
		void Add<T>(long applicationId, EventType type, EventState state, T obj);
		void Add(long applicationId, EventType type, EventState state);
	}
}