namespace Alicargo.Services.Abstract
{
    public interface IApplicationAwbManager
    {
        void SetAwb(long applicationId, long? awbId);
    }
}