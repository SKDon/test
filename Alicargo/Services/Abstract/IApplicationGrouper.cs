using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationGrouper
	{
        ApplicationGroup[] Group(ApplicationListItem[] models, OrderType[] groups);
	}
}