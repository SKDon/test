using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Services.Application
{
	[Obsolete]
	public sealed class ApplicationHelper : IApplicationHelper
	{
		private readonly ICountryRepository _countryRepository;
		private readonly ITransitService _transitService;
		private readonly IIdentityService _identity;
		private readonly IReferenceRepository _referenceRepository;
		private readonly IClientRepository _clientRepository;

		public ApplicationHelper(ICountryRepository countryRepository, ITransitService transitService,
			IIdentityService identity, IReferenceRepository referenceRepository, IClientRepository clientRepository)
		{
			_countryRepository = countryRepository;
			_transitService = transitService;
			_identity = identity;
			_referenceRepository = referenceRepository;
			_clientRepository = clientRepository;
		}

		public void SetAdditionalData(params ApplicationModel[] applications)
		{
			SetClientData(applications);

			SetTransitData(applications);

			SetReferenceData(applications);

			SetCountryData(applications);
		}

		private void SetCountryData(IEnumerable<ApplicationModel> applications)
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

		private void SetReferenceData(params ApplicationModel[] applications)
		{
			var applicationsWithReference = applications.Where(x => x.ReferenceId.HasValue).ToArray();

			if (applicationsWithReference.Length == 0) return;

			var ids = applicationsWithReference.Select(x => x.ReferenceId ?? 0).ToArray();

			var references = _referenceRepository.Get(ids).ToDictionary(x => x.Id, x => x);

			foreach (var application in applicationsWithReference)
			{
				if (!application.ReferenceId.HasValue || !references.ContainsKey(application.ReferenceId.Value))
					throw new InvalidLogicException();

				var referenceData = references[application.ReferenceId.Value];

				application.ReferenceBill = referenceData.Bill;
				application.ReferenceGTD = referenceData.GTD;
				application.ReferenceDateOfArrival = referenceData.DateOfArrival;
				application.ReferenceDateOfDeparture = referenceData.DateOfDeparture;				
			}
		}

		private void SetTransitData(params ApplicationModel[] applications)
		{
			var ids = applications.Select(x => x.TransitId).ToArray();
			var transits = _transitService.Get(ids).ToDictionary(x => x.Id, x => x);

			foreach (var application in applications)
			{
				application.Transit = transits[application.TransitId];
			}
		}

		private void SetClientData(params ApplicationModel[] applications)
		{
			var clientIds = applications.Select(x => x.ClientId).ToArray();
			var clients = _clientRepository.Get(clientIds).ToDictionary(x => x.Id, x => x);

			foreach (var application in applications)
			{
				var clientData = clients[application.ClientId];

				application.ClientUserId = clientData.UserId;
				application.LegalEntity = clientData.LegalEntity;
				application.ClientNic = clientData.Nic;
				application.ClientEmail = clientData.Email;
			}
		}
	}
}