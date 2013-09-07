using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.Abstract
{
    public interface IAwbManager
    {
        long Create(long applicationId, AirWaybillEditModel model);
	    long Create(long applicationId, SenderAwbModel model);
	    void Delete(long awbId);
    }
}