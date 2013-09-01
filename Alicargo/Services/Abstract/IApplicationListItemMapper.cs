using Alicargo.Contracts.Contracts;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
    public interface IApplicationListItemMapper
    {
        ApplicationListItem[] Map(ApplicationListItemData[] data);
    }
}
