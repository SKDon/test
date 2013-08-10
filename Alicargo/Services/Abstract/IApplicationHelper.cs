using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationHelper
	{
		void SetAdditionalData(params ApplicationEditModel[] applications);
	}
}
