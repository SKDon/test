using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.Abstract
{
    public interface IAwbManager
    {
        long Create(long applicationId, AirWaybillEditModel model);
        void Delete(long id);
    }
}