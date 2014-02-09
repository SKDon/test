using System;
using Alicargo.Core.Resources;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.Email
{
	[Obsolete]
	internal sealed class MessageBuilder : IMessageBuilder
	{
		public string DefaultSubject
		{
			get { return Mail.Default_Subject; }
		}

		public string AwbCreate(AirWaybillData model, string culture, float totalWeight, int totalCount)
		{
			return "";
			//return string.Format(Mail.Awb_Create, model.DepartureAirport,
			//					 LocalizationHelper.GetDate(model.DateOfDeparture, CultureInfo.GetCultureInfo(culture)),
			//					 model.ArrivalAirport,
			//					 LocalizationHelper.GetDate(model.DateOfArrival, CultureInfo.GetCultureInfo(culture)),
			//					 totalWeight, totalCount, model.Bill);
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
	}
}