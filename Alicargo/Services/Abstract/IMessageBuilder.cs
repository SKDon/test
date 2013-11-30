using Alicargo.Contracts.Contracts;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Abstract
{
	public interface IMessageBuilder
	{	
		string DefaultSubject { get; }
		string ClientAdd(ClientModel model, AuthenticationModel authenticationModel);

		string AwbCreate(AirWaybillData model, string culture, float totalWeight, int totalCount);
		string AwbSet(AirWaybillData model, string applicationNumber, string culture, float totalWeight, int totalCount);
		string AwbPackingFileAdded(AirWaybillData model);
		string AwbAWBFileAdded(AirWaybillData model);
		string AwbGTDAdditionalFileAdded(AirWaybillData model);
		string AwbGTDFileAdded(AirWaybillData model);
		string AwbInvoiceFileAdded(AirWaybillData model);
	}
}