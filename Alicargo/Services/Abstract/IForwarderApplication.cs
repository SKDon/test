using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IForwarderApplication
	{
		void UpdateDeliveryData(ApplicationListItem[] applicationItems, string language);
	}
}