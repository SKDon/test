using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.User;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.Abstract
{
	public interface IAwbPresenter
	{
		ListCollection<AirWaybillListItem> List(int take, int skip, long? brokerId, string language);
		AwbAdminModel Get(long id);
		AirWaybillData GetData(long id);
		AirWaybillAggregate GetAggregate(long id);
		AwbSenderModel GetSenderAwbModel(long id);
		BrokerData GetBroker(long brokerId);
	}
}