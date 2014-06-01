using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Awb;

namespace Alicargo.Core.Contracts.AirWaybill
{
	public interface IAwbGtdHelper
    {
        void ProcessGtd(AirWaybillData data, string newGtd);
    }
}