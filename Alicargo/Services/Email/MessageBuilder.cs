using Alicargo.Contracts.Contracts;
using Alicargo.Core.Enums;
using Alicargo.Core.Helpers;
using Alicargo.Core.Resources;
using Alicargo.Core.Services.Abstract;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;
using Alicargo.ViewModels.User;
using Resources;

namespace Alicargo.Services.Email
{
	internal sealed class MessageBuilder : IMessageBuilder
	{
		private readonly ILocalizationService _localizationService;

		public MessageBuilder(ILocalizationService localizationService)
		{
			_localizationService = localizationService;
		}

		public string DefaultSubject
		{
			get { return Mail.Default_Subject; }
		}

		#region Application

		public string ApplicationUpdate
		{
			get { return Mail.Application_Update; }
		}

		public string ApplicationDelete
		{
			get { return Mail.Application_Delete; }
		}

		public string GetApplicationSubject(string displayNumber)
		{
			return string.Format(Mail.Application_Subject, displayNumber);
		}		

		#endregion

		public string ClientAdd(ClientModel model, AuthenticationModel authenticationModel)
		{
			return string.Format(Mail.Client_Add, model.Contacts, authenticationModel.Login,
								 authenticationModel.NewPassword);
		}

		#region AWB

		public string AwbCreate(AirWaybillData model, string culture, float totalWeight, int totalCount)
		{
			return string.Format(Mail.Awb_Create, model.DepartureAirport,
								 _localizationService.GetDate(model.DateOfDeparture, culture),
								 model.ArrivalAirport,
								 _localizationService.GetDate(model.DateOfArrival, culture),
								 totalWeight, totalCount, model.Bill);
		}

		public string AwbSet(AirWaybillData model, string applicationNumber, string culture, float totalWeight, int totalCount)
		{
			return string.Format(Mail.Awb_Set, model.DepartureAirport,
								 _localizationService.GetDate(model.DateOfDeparture, culture),
								 model.ArrivalAirport,
								 _localizationService.GetDate(model.DateOfArrival, culture),
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