using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
    public interface IApplicationListItemMapper
    {
        ApplicationListItem[] Map(ApplicationListItemData[] data);
    }
}
