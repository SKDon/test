using Alicargo.Contracts.Contracts;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
    internal interface IApplicationListItemMapper
    {
        ApplicationListItem[] Map(ApplicationListItemData[] data);
    }
}
