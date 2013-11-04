using Alicargo.Core.Enums;

namespace Alicargo.Core.Services.Abstract
{
	public interface IApplicationEvent
	{
		void Add(long applicationId, ApplicationEventType eventType);
	}
}
