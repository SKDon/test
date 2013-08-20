using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Services.Abstract;
using Alicargo.Services.Contract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;
using Resources;

namespace Alicargo.Services.Email
{
	// todo: 1.5. use recipient culture
	public sealed class MessageBuilder : IMessageBuilder
	{
		private readonly IUserRepository _userRepository;
		private readonly ILocalizationService _localizationService;

		public MessageBuilder(IUserRepository userRepository, ILocalizationService localizationService)
		{
			_userRepository = userRepository;
			_localizationService = localizationService;
		}

		#region Helpers

		public Recipient[] GetAdminEmails()
		{
			return _userRepository.GetByRole(RoleType.Admin)
				.Select(x => new Recipient
				{
					Culture = x.TwoLetterISOLanguageName,
					Email = x.Email
				})
				.ToArray();
		}

		public Recipient[] GetSenderEmails()
		{
			return _userRepository.GetByRole(RoleType.Sender)
				.Select(x => new Recipient
				{
					Culture = x.TwoLetterISOLanguageName,
					Email = x.Email
				})
				.ToArray();
		}

		public Recipient[] GetForwarderEmails()
		{
			return _userRepository.GetByRole(RoleType.Forwarder)
				.Select(x => new Recipient
				{
					Culture = x.TwoLetterISOLanguageName,
					Email = x.Email
				})
				.ToArray();
		}

		public string DefaultSubject
		{
			get { return Mail.Default_Subject; }
		}

		#endregion

		#region Application

		public string ApplicationUpdate
		{
			get
			{
				return Mail.Application_Update;
			}
		}

		public string ApplicationAdd(ApplicationDetailsModel model, string culture)
		{
			return string.Format(Mail.Application_Add,
				ApplicationModelHelper.GetDisplayNumber(model.Id, model.Count),
				model.ClientNic,
				model.FactoryName,
				model.MarkName,
				_localizationService.GetDate(model.CreationTimestamp, culture));
		}

		public string ApplicationDelete
		{
			get
			{
				return Mail.Application_Delete;
			}
		}

		public string ApplicationSubject
		{
			get
			{
				return Mail.Application_Subject;
			}
		}

		public string ApplicationSetState(ApplicationDetailsModel model, string culture)
		{
			return string.Format(Mail.Application_SetState,
				ApplicationModelHelper.GetDisplayNumber(model.Id, model.Count),
				_localizationService.GetDate(model.DateOfCargoReceipt, culture),
				model.TransitCarrierName,
				model.FactoryName,
				model.FactoryEmail,
				model.FactoryPhone,
				model.FactoryContact,
				ApplicationModelHelper.GetDaysInWork(model.CreationTimestamp),
				model.Invoice,
				_localizationService.GetDate(model.CreationTimestamp, culture),
				_localizationService.GetDate(model.StateChangeTimestamp, culture),
				model.MarkName,
				model.Count,
				model.Volume,
				model.Weigth,
				model.Characteristic,
				_localizationService.GetCurrency(model.Value, (CurrencyType)model.CurrencyId, culture),
				model.AddressLoad,
				model.CountryName,
				model.WarehouseWorkingTime,
				model.TermsOfDelivery,
				_localizationService.GetMethodOfDelivery((MethodOfDelivery) model.MethodOfDeliveryId, culture),
				model.TransitCity,
				model.TransitAddress,
				model.TransitRecipientName,
				model.TransitPhone,
				model.TransitWarehouseWorkingTime,
				_localizationService.GetMethodOfTransit((MethodOfTransit)model.TransitMethodOfTransitId, culture),
				_localizationService.GetDeliveryType((DeliveryType)model.TransitDeliveryTypeId, culture),
				model.AirWaybill,
				_localizationService.GetDate(model.AirWaybillDateOfDeparture, culture),
				_localizationService.GetDate(model.AirWaybillDateOfArrival, culture),
				model.AirWaybillGTD,
				model.TransitReference,
				_localizationService.GetStateName(model.StateId, culture));
		}

		public string ApplicationSetDateOfCargoReceipt(ApplicationDetailsModel model, string culture)
		{
			return string.Format(Mail.Application_SetDateOfCargoReceipt,
				_localizationService.GetDate(model.DateOfCargoReceipt, culture));
		}

		public string ApplicationInvoiceFileAdded(ApplicationDetailsModel model)
		{
			return string.Format(Mail.Application_InvoiceFileAdded, ApplicationModelHelper.GetDisplayNumber(model.Id, model.Count), model.FactoryName, model.MarkName,
				model.Invoice, model.InvoiceFileName);
		}

		public string ApplicationSwiftFileAdded(ApplicationDetailsModel model)
		{
			return string.Format(Mail.Application_SwiftFileAdded, ApplicationModelHelper.GetDisplayNumber(model.Id, model.Count), model.FactoryName, model.MarkName,
				model.Invoice);
		}

		public string ApplicationPackingFileAdded(ApplicationDetailsModel model)
		{
			return string.Format(Mail.Application_PackingFileAdded, ApplicationModelHelper.GetDisplayNumber(model.Id, model.Count), model.FactoryName, model.MarkName,
				model.Invoice);
		}

		public string ApplicationDeliveryBillFileAdded(ApplicationDetailsModel model)
		{
			return string.Format(Mail.Application_DeliveryBillFileAdded, ApplicationModelHelper.GetDisplayNumber(model.Id, model.Count), model.FactoryName, model.MarkName,
				model.Invoice);
		}

		public string ApplicationTorg12FileAdded(ApplicationDetailsModel model)
		{
			return string.Format(Mail.Application_Torg12FileAdded, ApplicationModelHelper.GetDisplayNumber(model.Id, model.Count), model.FactoryName, model.MarkName,
				model.Invoice);
		}

		public string ApplicationCPFileAdded(ApplicationDetailsModel model)
		{
			return string.Format(Mail.Application_CPFileAdded, ApplicationModelHelper.GetDisplayNumber(model.Id, model.Count), model.FactoryName, model.MarkName,
				model.Invoice);
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