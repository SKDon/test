using Alicargo.Contracts.Contracts;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.Abstract
{
	public interface IAwbPresenter
	{
		ListCollection<AirWaybillListItem> List(int take, int skip, long? brokerId);
		AirWaybillEditModel Get(long id);
		AirWaybillData GetData(long id);
		AirWaybillAggregate GetAggregate(long id);
		SenderAwbModel GetSenderAwbModel(long id);
		BrokerData GetBroker(long brokerId);
	}
}