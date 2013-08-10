using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	[Obsolete]
	public sealed class ApplicationHelper : IApplicationHelper
	{
		private readonly ICountryRepository _countryRepository;
		private readonly ITransitService _transitService;
		private readonly IIdentityService _identity;
		private readonly IAirWaybillRepository _airWaybillRepository;
		private readonly IClientRepository _clientRepository;

		public ApplicationHelper(ICountryRepository countryRepository, ITransitService transitService,
			IIdentityService identity, IAirWaybillRepository airWaybillRepository, IClientRepository clientRepository)
		{
			_countryRepository = countryRepository;
			_transitService = transitService;
			_identity = identity;
			_airWaybillRepository = airWaybillRepository;
			_clientRepository = clientRepository;
		}

		public void SetAdditionalData(params ApplicationEditModel[] applications)
		{
			SetClientData(applications);

			SetTransitData(applications);

			SetAirWaybillData(applications);

			SetCountryData(applications);
		}

		private void SetCountryData(IEnumerable<ApplicationEditModel> applications)
		{
			var applicationWithCountry = applications.Where(x => x.CountryId.HasValue).ToArray();

			var countries = _countryRepository
				// ReSharper disable PossibleInvalidOperationException
				.Get(applicationWithCountry.Select(x => x.CountryId.Value).ToArray())
				// ReSharper restore PossibleInvalidOperationException
				.ToDictionary(x => x.Id, x => x.Name);

			foreach (var application in applicationWithCountry)
			{
				// ReSharper disable PossibleInvalidOperationException
				// ReSharper disable AssignNullToNotNullAttribute
				application.CountryName = countries[application.CountryId.Value][_identity.TwoLetterISOLanguageName];
				// ReSharper restore AssignNullToNotNullAttribute
				// ReSharper restore PossibleInvalidOperationException
			}
		}

		private void SetAirWaybillData(params ApplicationEditModel[] applications)
		{
			var applicationsWithAirWaybill = applications.Where(x => x.AirWaybillId.HasValue).ToArray();

			if (applicationsWithAirWaybill.Length == 0) return;

			var ids = applicationsWithAirWaybill.Select(x => x.AirWaybillId ?? 0).ToArray();

			var airWaybills = _airWaybillRepository.Get(ids).ToDictionary(x => x.Id, x => x);

			foreach (var application in applicationsWithAirWaybill)
			{
				if (!application.AirWaybillId.HasValue || !airWaybills.ContainsKey(application.AirWaybillId.Value))
					throw new InvalidLogicException();

				var airWaybillData = airWaybills[application.AirWaybillId.Value];

				application.AirWaybill = airWaybillData.Bill;
				application.AirWaybillGTD = airWaybillData.GTD;
				application.AirWaybillDateOfArrival = airWaybillData.DateOfArrival;
				application.AirWaybillDateOfDeparture = airWaybillData.DateOfDeparture;				
			}
		}

		private void SetTransitData(params ApplicationEditModel[] applications)
		{
			var ids = applications.Select(x => x.TransitId).ToArray();
			var transits = _transitService.Get(ids).ToDictionary(x => x.Id, x => x);

			foreach (var application in applications)
			{
				application.Transit = transits[application.TransitId];
			}
		}

		private void SetClientData(params ApplicationEditModel[] applications)
		{
			var clientIds = applications.Select(x => x.ClientId).ToArray();
			var clients = _clientRepository.Get(clientIds).ToDictionary(x => x.Id, x => x);

			foreach (var application in applications)
			{
				var clientData = clients[application.ClientId];

				application.ClientUserId = clientData.UserId;
				application.ClientLegalEntity = clientData.LegalEntity;
				application.ClientNic = clientData.Nic;
				application.ClientEmail = clientData.Email;
			}
		}
	}
}