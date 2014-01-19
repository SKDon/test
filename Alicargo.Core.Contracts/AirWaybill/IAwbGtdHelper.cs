using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.Services.Abstract
{
	public interface IAwbGtdHelper
    {
        void ProcessGtd(AirWaybillData data, string newGtd);
    }
}