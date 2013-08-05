using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public 	interface IApplicationHelper
	{
		void SetAdditionalData(params ApplicationModel[] applications);
	}
}
