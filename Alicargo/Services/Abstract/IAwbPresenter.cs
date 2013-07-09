using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface IAwbPresenter
	{		
		ListCollection<ReferenceModel> List(int take, int skip);
		ReferenceModel Get(long id);		
	}
}