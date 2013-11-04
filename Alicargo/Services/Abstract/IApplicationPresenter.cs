using System.Collections.Generic;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationPresenter
	{
		ApplicationAdminModel Get(long id);
		ApplicationDetailsModel GetDetails(long id);
		ApplicationStateModel[] GetAvailableStates(long id);
		IDictionary<long, string> GetLocalizedCountries();
	}
}