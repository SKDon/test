using System.Globalization;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Helpers;
using Alicargo.Core.Resources;
using Alicargo.Core.Services;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Email
{
	internal sealed class MessageBuilder : IMessageBuilder
	{
		public string DefaultSubject
		{
			get { return Mail.Default_Subject; }
		}

		public string ClientAdd(ClientModel model, AuthenticationModel authenticationModel)
		{
			return string.Format(Mail.Client_Add, model.Contacts, authenticationModel.Login,
								 authenticationModel.NewPassword);
		}

		#region AWB

		public string AwbCreate(AirWaybillData model, string culture, float totalWeight, int totalCount)
		{
			return string.Format(Mail.Awb_Create, model.DepartureAirport,
								 LocalizationHelper.GetDate(model.DateOfDeparture, CultureInfo.GetCultureInfo(culture)),
								 model.ArrivalAirport,
								 LocalizationHelper.GetDate(model.DateOfArrival, CultureInfo.GetCultureInfo(culture)),
								 totalWeight, totalCount, model.Bill);
		}

		public string AwbSet(AirWaybillData model, string applicationNumber, string culture, float totalWeight, int totalCount)
		{
			return string.Format(Mail.Awb_Set, model.DepartureAirport,
								 LocalizationHelper.GetDate(model.DateOfDeparture, CultureInfo.GetCultureInfo(culture)),
								 model.ArrivalAirport,
								 LocalizationHelper.GetDate(model.DateOfArrival, CultureInfo.GetCultureInfo(culture)),
								 totalWeight, totalCount, model.Bill, applicationNumber);
		}

		public string AwbPackingFileAdded(AirWaybillData model)
		{
			return string.Format(Mail.Awb_PackingFileAdd, model.Bill);
		}

		public string AwbAWBFileAdded(AirWaybillData model)
		{
			return string.Format(Mail.Awb_AWBFileAdd, model.Bill);
		}

		public string AwbGTDAdditionalFileAdded(AirWaybillData model)
		{
			return string.Format(Mail.Awb_GTDAdditionalFileAdd, model.Bill);
		}

		public string AwbGTDFileAdded(AirWaybillData model)
		{
			return string.Format(Mail.Awb_GTDFileAdd, model.Bill);
		}

		public string AwbInvoiceFileAdded(AirWaybillData model)
		{
			return string.Format(Mail.Awb_InvoiceFileAdd, model.Bill);
		}

		#endregion
	}
}