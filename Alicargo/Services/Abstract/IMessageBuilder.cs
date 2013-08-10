using Alicargo.Core.Models;
using Alicargo.Services.Contract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IMessageBuilder
	{
		Recipient[] GetAdminEmails();
		Recipient[] GetSenderEmails();
		Recipient[] GetForwarderEmails();

		string DefaultSubject { get; }
		string ClientAdd(Client model);

		string AwbCreate(AirWaybillModel model, string culture);
		string AwbSet(AirWaybillModel model, string applicationNumber, string culture);
		string AwbPackingFileAdded(AirWaybillModel model);
		string AwbAWBFileAdded(AirWaybillModel model);
		string AwbGTDAdditionalFileAdded(AirWaybillModel model);
		string AwbGTDFileAdded(AirWaybillModel model);
		string AwbInvoiceFileAdded(AirWaybillModel model);

		string ApplicationUpdate { get; }
		string ApplicationAdd(ApplicationEditModel modell, string culture);
		string ApplicationDelete { get; }
		string ApplicationSubject { get; }
		string ApplicationSetState(ApplicationEditModel modell, string culture);
		string ApplicationSetDateOfCargoReceipt(ApplicationEditModel model, string culture);

		string ApplicationInvoiceFileAdded(ApplicationEditModel model);
		string ApplicationSwiftFileAdded(ApplicationEditModel model);
		string ApplicationPackingFileAdded(ApplicationEditModel model);
		string ApplicationDeliveryBillFileAdded(ApplicationEditModel model);
		string ApplicationTorg12FileAdded(ApplicationEditModel model);
		string ApplicationCPFileAdded(ApplicationEditModel model);
	}
}