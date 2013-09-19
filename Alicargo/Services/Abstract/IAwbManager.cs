using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.Abstract
{
    public interface IAwbManager
    {
        long Create(long applicationId, AwbAdminModel model);
	    long Create(long applicationId, AwbSenderModel model);
	    void Delete(long awbId);
    }
}