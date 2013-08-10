using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Core.Enums;
using Alicargo.Core.Localization;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	public sealed class ApplicationListPresenter : IApplicationListPresenter
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IApplicationGrouper _applicationGrouper;
		private readonly ICountryRepository _countryRepository;
		private readonly IIdentityService _identity;
		private readonly IAirWaybillRepository _airWaybillRepository;
		private readonly IStateService _stateService;
		private readonly IStateConfig _stateConfig;

		public ApplicationListPresenter(IApplicationRepository applicationRepository,
			IStateService stateService, IAirWaybillRepository airWaybillRepository, 
			IIdentityService identity, IApplicationGrouper applicationGrouper, 
			ICountryRepository countryRepository, IStateConfig stateConfig)
		{
			_applicationRepository = applicationRepository;
			_stateService = stateService;
			_identity = identity;
			_applicationGrouper = applicationGrouper;
			_countryRepository = countryRepository;
			_airWaybillRepository = airWaybillRepository;
			_stateConfig = stateConfig;
		}

		public ApplicationListCollection List(int take, int skip, Order[] groups)
		{
			var stateIds = _stateService.GetVisibleStates();

			var isClient = _identity.IsInRole(RoleType.Client);

			var orders = PrepareOrders(groups);

			var data = _applicationRepository.List(take, skip, stateIds, orders, isClient ? _identity.Id : null);

			var applications = GetListItems(data);

			if (groups == null || groups.Length == 0)
			{
				return new ApplicationListCollection
				{
					Data = applications,
					Total = _applicationRepository.Count(stateIds, isClient ? _identity.Id : null),
				};
			}

			return GetGroupedApplicationListCollection(groups, stateIds, isClient, applications);
		}

		private ApplicationListItem[] GetListItems(IEnumerable<ApplicationListItemData> data)
		{
			var countries = _countryRepository.Get().ToDictionary(x => x.Id, x => x.Name[_identity.TwoLetterISOLanguageName]);
			var localizedStates = _stateService.GetLocalizedDictionary();
			var availableStates = _stateService.GetAvailableStatesToSet();

			var applications = data.Select(x => new ApplicationListItem
			{
				CountryName = x.CountryId.HasValue ? countries[x.CountryId.Value] : null,
				State = new ApplicationStateModel
				{
					StateId = x.StateId,
					StateName = localizedStates[x.StateId]
				},
				CanClose = x.StateId == _stateConfig.CargoOnTransitStateId, // todo: test 1.
				CanSetState = availableStates.Contains(x.StateId), // todo: test 1.
				AddressLoad = x.AddressLoad,
				Id = x.Id,
				PackingFileName = x.PackingFileName,
				FactoryName = x.FactoryName,
				Invoice = x.Invoice,
				InvoiceFileName = x.InvoiceFileName,
				MarkName = x.MarkName,
				SwiftFileName = x.SwiftFileName,
				Volume = x.Volume,
				Count = x.Count,
				AirWaybill = x.AirWaybill,
				CPFileName = x.CPFileName,
				Characteristic = x.Characteristic,
				ClientLegalEntity = x.ClientLegalEntity,
				ClientNic = x.ClientNic,
				CreationTimestamp = x.CreationTimestamp,
				DateInStock = x.DateInStock,
				DateOfCargoReceipt = x.DateOfCargoReceipt,
				DeliveryBillFileName = x.DeliveryBillFileName,
				FactoryContact = x.FactoryContact,
				FactoryEmail = x.FactoryEmail,
				FactoryPhone = x.FactoryPhone,
				StateChangeTimestamp = x.StateChangeTimestamp,
				StateId = x.StateId,
				TermsOfDelivery = x.TermsOfDelivery,
				Torg12FileName = x.Torg12FileName,
				TransitAddress = x.TransitAddress,
				TransitCarrierName = x.TransitCarrierName,
				TransitId = x.TransitId,
				TransitCity = x.TransitCity,
				TransitDeliveryTypeString = ((DeliveryType)x.TransitDeliveryTypeId).ToLocalString(),
				TransitMethodOfTransitString = ((MethodOfTransit)x.TransitMethodOfTransitId).ToLocalString(),
				TransitPhone = x.TransitPhone,
				TransitRecipientName = x.TransitRecipientName,
				TransitReference = x.TransitRecipientName,
				TransitWarehouseWorkingTime = x.TransitWarehouseWorkingTime,
				WarehouseWorkingTime = x.WarehouseWorkingTime,
				Weigth = x.Weigth,
				MethodOfDeliveryId = x.MethodOfDeliveryId,
				Value = x.Value,
				CurrencyId = x.CurrencyId,
				AirWaybillId = x.AirWaybillId				
			}).ToArray();
			return applications;
		}

		private ApplicationListCollection GetGroupedApplicationListCollection(Order[] groups, 
			IEnumerable<long> stateIds, bool isClient, ApplicationListItem[] applications)
		{
			var ids = applications.Select(x => x.AirWaybillId ?? 0).ToArray();
			var airWaybills = _airWaybillRepository.Get(ids).ToDictionary(x => x.Id, x => x);

			return new ApplicationListCollection
			{
				Total = _applicationRepository.Count(stateIds, isClient ? _identity.Id : null),
				Groups = _applicationGrouper.Group(applications, groups, airWaybills),
			};
		}

		private static Order[] PrepareOrders(IEnumerable<Order> orders)
		{
			var byId = new[] { new Order { Desc = true, OrderType = OrderType.Id } };

			return orders == null ? byId : orders.Concat(byId).ToArray();
		}
	}
}