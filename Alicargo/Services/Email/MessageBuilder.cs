using System.Linq;
using Alicargo.Core.Enums;
using Alicargo.Core.Models;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.Services.Contract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;
using Resources;

namespace Alicargo.Services.Email
{
	// todo: use recipient culture
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

		public string ApplicationAdd(ApplicationModel model, string culture)
		{
			return string.Format(Mail.Application_Add,
				ApplicationListItem.GetDisplayNumber(model.Id, model.Count),
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

		public string ApplicationSetState(ApplicationModel model, string culture)
		{
			return string.Format(Mail.Application_SetState,
				ApplicationListItem.GetDisplayNumber(model.Id, model.Count),
				_localizationService.GetDate(model.DateOfCargoReceipt, culture),
				model.Transit.CarrierName,
				model.FactoryName,
				model.FactoryEmail,
				model.FactoryPhone,
				model.FactoryContact,
				ApplicationModel.GetDaysInWork(model.CreationTimestamp),
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
				_localizationService.GetMethodOfDelivery(model.MethodOfDelivery, culture),
				model.Transit.City,
				model.Transit.Address,
				model.Transit.RecipientName,
				model.Transit.Phone,
				model.Transit.WarehouseWorkingTime,
				_localizationService.GetMethodOfTransit(model.Transit.MethodOfTransit, culture),
				_localizationService.GetDeliveryType(model.Transit.DeliveryType, culture),
				model.ReferenceBill,
				_localizationService.GetDate(model.ReferenceDateOfDeparture, culture),
				_localizationService.GetDate(model.ReferenceDateOfArrival, culture),
				model.ReferenceGTD,
				model.TransitReference,
				_localizationService.GetStateName(model.StateId, culture));
		}

		public string ApplicationSetDateOfCargoReceipt(ApplicationModel model, string culture)
		{
			return string.Format(Mail.Application_SetDateOfCargoReceipt,
				_localizationService.GetDate(model.DateOfCargoReceipt, culture));
		}

		public string ApplicationInvoiceFileAdded(ApplicationModel model)
		{
			return string.Format(Mail.Application_InvoiceFileAdded, ApplicationListItem.GetDisplayNumber(model.Id, model.Count), model.FactoryName, model.MarkName,
				model.Invoice, model.InvoiceFileName);
		}

		public string ApplicationSwiftFileAdded(ApplicationModel model)
		{
			return string.Format(Mail.Application_SwiftFileAdded, ApplicationListItem.GetDisplayNumber(model.Id, model.Count), model.FactoryName, model.MarkName,
				model.Invoice);
		}

		public string ApplicationPackingFileAdded(ApplicationModel model)
		{
			return string.Format(Mail.Application_PackingFileAdded, ApplicationListItem.GetDisplayNumber(model.Id, model.Count), model.FactoryName, model.MarkName,
				model.Invoice);
		}

		public string ApplicationDeliveryBillFileAdded(ApplicationModel model)
		{
			return string.Format(Mail.Application_DeliveryBillFileAdded, ApplicationListItem.GetDisplayNumber(model.Id, model.Count), model.FactoryName, model.MarkName,
				model.Invoice);
		}

		public string ApplicationTorg12FileAdded(ApplicationModel model)
		{
			return string.Format(Mail.Application_Torg12FileAdded, ApplicationListItem.GetDisplayNumber(model.Id, model.Count), model.FactoryName, model.MarkName,
				model.Invoice);
		}

		public string ApplicationCPFileAdded(ApplicationModel model)
		{
			return string.Format(Mail.Application_CPFileAdded, ApplicationListItem.GetDisplayNumber(model.Id, model.Count), model.FactoryName, model.MarkName,
				model.Invoice);
		}

		#endregion

		public string ClientAdd(Client model)
		{
			return string.Format(Mail.Client_Add, model.Contacts, model.AuthenticationModel.Login,
				model.AuthenticationModel.NewPassword);
		}

		#region AWB

		public string AwbCreate(ReferenceModel model, string culture)
		{
			return string.Format(Mail.Awb_Create, model.DepartureAirport,
				_localizationService.GetDate(model.DateOfDeparture, culture),
				model.ArrivalAirport,
				_localizationService.GetDate(model.DateOfArrival, culture),
				model.TotalWeight, model.TotalCount, model.Bill);
		}

		public string AwbSet(ReferenceModel model, string applicationNumber, string culture)
		{
			return string.Format(Mail.Awb_Set, model.DepartureAirport,
				_localizationService.GetDate(model.DateOfDeparture, culture),
				model.ArrivalAirport,
				_localizationService.GetDate(model.DateOfArrival, culture),
				model.TotalWeight, model.TotalCount, model.Bill, applicationNumber);
		}

		public string AwbPackingFileAdded(ReferenceModel model)
		{
			return string.Format(Mail.Awb_PackingFileAdd, model.Bill);
		}

		public string AwbAWBFileAdded(ReferenceModel model)
		{
			return string.Format(Mail.Awb_AWBFileAdd, model.Bill);
		}

		public string AwbGTDAdditionalFileAdded(ReferenceModel model)
		{
			return string.Format(Mail.Awb_GTDAdditionalFileAdd, model.Bill);
		}

		public string AwbGTDFileAdded(ReferenceModel model)
		{
			return string.Format(Mail.Awb_GTDFileAdd, model.Bill);
		}

		public string AwbInvoiceFileAdded(ReferenceModel model)
		{
			return string.Format(Mail.Awb_InvoiceFileAdd, model.Bill);
		}

		#endregion
	}
}