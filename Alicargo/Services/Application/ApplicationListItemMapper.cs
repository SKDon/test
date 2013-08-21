using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Core.Localization;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
    internal sealed class ApplicationListItemMapper : IApplicationListItemMapper
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IIdentityService _identity;
        private readonly IStateConfig _stateConfig;
        private readonly IStateService _stateService;

        public ApplicationListItemMapper(IStateService stateService, IStateConfig stateConfig,
                                         ICountryRepository countryRepository, IIdentityService identity)
        {
            _stateService = stateService;
            _stateConfig = stateConfig;
            _countryRepository = countryRepository;
            _identity = identity;
        }

        // todo: 1. Test
        public ApplicationListItem[] GetListItems(IEnumerable<ApplicationListItemData> data)
        {
            var countries = _countryRepository.Get()
                                              .ToDictionary(x => x.Id, x => x.Name[_identity.TwoLetterISOLanguageName]);
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
                    CanClose = x.StateId == _stateConfig.CargoOnTransitStateId, // todo: 1. test
                    CanSetState = availableStates.Contains(x.StateId), // todo: 1. test
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
                    TransitDeliveryTypeString = ((DeliveryType) x.TransitDeliveryTypeId).ToLocalString(),
                    TransitMethodOfTransitString = ((MethodOfTransit) x.TransitMethodOfTransitId).ToLocalString(),
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
    }
}