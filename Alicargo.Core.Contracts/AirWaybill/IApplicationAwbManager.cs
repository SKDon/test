namespace Alicargo.Core.Contracts.AirWaybill
{
    public interface IApplicationAwbManager
    {
        void SetAwb(long applicationId, long? awbId);
    }
}