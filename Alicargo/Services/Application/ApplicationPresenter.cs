using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class ApplicationPresenter : IApplicationPresenter
	{
		private readonly IApplicationRepository _applications;
		private readonly ICountryRepository _countries;
		private readonly IIdentityService _identity;
		private readonly IStateRepository _states;
		private readonly IStateFilter _stateFilter;

		public ApplicationPresenter(
			IApplicationRepository applications,
			IIdentityService identity,
			ICountryRepository countries,
			IStateFilter stateFilter,
			IStateRepository states)
		{
			_applications = applications;
			_identity = identity;
			_countries = countries;
			_stateFilter = stateFilter;
			_states = states;
		}

		public ApplicationAdminModel Get(long id)
		{
			var data = _applications.Get(id);

			var application = GetModel(data);

			return application;
		}

		public ApplicationStateModel[] GetStateAvailability(long id)
		{
			var applicationData = _applications.Get(id);

			var states = _stateFilter.GetStateAvailabilityToSet();

			if (_identity.IsInRole(RoleType.Admin)) return ToApplicationStateModel(states);

			states = _stateFilter.FilterByBusinessLogic(applicationData, states);

			var currentState = _states.Get(_identity.TwoLetterISOLanguageName, applicationData.StateId).Values.First();

			states = _stateFilter.FilterByPosition(states, currentState.Position);

			return ToApplicationStateModel(states);
		}		

		private ApplicationStateModel[] ToApplicationStateModel(long[] ids)
		{
			return _states.Get(_identity.TwoLetterISOLanguageName, ids)
				.Select(x => new ApplicationStateModel
				{
					StateId = x.Key,
					StateName = x.Value.LocalizedName
				})
				.ToArray();
		}

		private static ApplicationAdminModel GetModel(ApplicationData data)
		{
			return new ApplicationAdminModel
			{
				AddressLoad = data.AddressLoad,
				Characteristic = data.Characteristic,
				Count = data.Count,
				Currency = new CurrencyModel
				{
					CurrencyId = data.CurrencyId,
					Value = data.Value
				},
				FactoryContact = data.FactoryContact,
				FactoryEmail = data.FactoryEmail,
				FactoryName = data.FactoryName,
				FactoryPhone = data.FactoryPhone,
				Weight = data.Weight,
				Invoice = data.Invoice,
				MarkName = data.MarkName,
				MethodOfDelivery = (MethodOfDelivery)data.MethodOfDeliveryId,
				TermsOfDelivery = data.TermsOfDelivery,
				CountryId = data.CountryId,
				Volume = data.Volume,
				WarehouseWorkingTime = data.WarehouseWorkingTime,
				FactureCost = data.FactureCost,
				TransitCost = data.TransitCost,
				PickupCost = data.PickupCost,
				TariffPerKg = data.TariffPerKg,
				ScotchCostEdited = data.ScotchCostEdited,
				FactureCostEdited = data.FactureCostEdited,
				TransitCostEdited = data.TransitCostEdited,
				PickupCostEdited = data.PickupCostEdited,
				SenderId = data.SenderId
			};
		}
	}
}