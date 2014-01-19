using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.Core.Contracts.AirWaybill
{
	public interface IAwbGtdHelper
    {
        void ProcessGtd(AirWaybillData data, string newGtd);
    }
}