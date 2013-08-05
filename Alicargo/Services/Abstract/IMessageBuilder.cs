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

		string AwbCreate(ReferenceModel model, string culture);
		string AwbSet(ReferenceModel model, string applicationNumber, string culture);
		string AwbPackingFileAdded(ReferenceModel model);
		string AwbAWBFileAdded(ReferenceModel model);
		string AwbGTDAdditionalFileAdded(ReferenceModel model);
		string AwbGTDFileAdded(ReferenceModel model);
		string AwbInvoiceFileAdded(ReferenceModel model);

		string ApplicationUpdate { get; }
		string ApplicationAdd(ApplicationModel modell, string culture);
		string ApplicationDelete { get; }
		string ApplicationSubject { get; }
		string ApplicationSetState(ApplicationModel modell, string culture);
		string ApplicationSetDateOfCargoReceipt(ApplicationModel model, string culture);

		string ApplicationInvoiceFileAdded(ApplicationModel model);
		string ApplicationSwiftFileAdded(ApplicationModel model);
		string ApplicationPackingFileAdded(ApplicationModel model);
		string ApplicationDeliveryBillFileAdded(ApplicationModel model);
		string ApplicationTorg12FileAdded(ApplicationModel model);
		string ApplicationCPFileAdded(ApplicationModel model);
	}
}