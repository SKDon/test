using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.Services.Abstract
{
    internal interface IAwbGtdHelper
    {
        void ProcessGtd(AirWaybillData data, string newGtd);
    }
}