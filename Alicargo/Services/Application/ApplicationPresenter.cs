using System.Linq;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class ApplicationPresenter : IApplicationPresenter
	{
		private readonly IApplicationRepository _applications;
		private readonly IIdentityService _identity;
		private readonly IStateRepository _states;
		private readonly IStateFilter _stateFilter;
		private readonly ITransitRepository _transits;

		public ApplicationPresenter(
			IApplicationRepository applications,
			IIdentityService identity,
			IStateFilter stateFilter,
			ITransitRepository transits,
			IStateRepository states)
		{
			_applications = applications;
			_identity = identity;
			_stateFilter = stateFilter;
			_transits = transits;
			_states = states;
		}

		public ApplicationAdminModel Get(long id)
		{
			var data = _applications.Get(id);

			var transit = _transits.Get(data.TransitId).Single();

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
				MethodOfDelivery = data.MethodOfDelivery,
				IsPickup = data.IsPickup,
				TermsOfDelivery = data.TermsOfDelivery,
				CountryId = data.CountryId,
				Volume = data.Volume,
				WarehouseWorkingTime = data.WarehouseWorkingTime,
				FactureCost = data.FactureCost,
				FactureCostEx = data.FactureCostEx,
				TransitCost = data.TransitCost,
				PickupCost = data.PickupCost,
				TariffPerKg = data.TariffPerKg,
				ScotchCostEdited = data.ScotchCostEdited,
				FactureCostEdited = data.FactureCostEdited,
				FactureCostExEdited = data.FactureCostExEdited,
				TransitCostEdited = data.TransitCostEdited,
				PickupCostEdited = data.PickupCostEdited,
				SenderId = data.SenderId,
				ForwarderId = data.ForwarderId,
				CarrierId = transit.CarrierId,
				InsuranceRate = data.InsuranceRate * 100
			};
		}

		public ApplicationStateModel[] GetStateAvailability(long id)
		{
			var applicationData = _applications.Get(id);

			var states = _stateFilter.GetStateAvailabilityToSet();

			if (_identity.IsInRole(RoleType.Admin) || _identity.IsInRole(RoleType.Manager))
			{
				return ToApplicationStateModel(states);
			}

			states = _stateFilter.FilterByBusinessLogic(applicationData, states);

			return ToApplicationStateModel(states);
		}		

		private ApplicationStateModel[] ToApplicationStateModel(long[] ids)
		{
			return _states.Get(_identity.Language, ids)
				.Select(x => new ApplicationStateModel
				{
					StateId = x.Key,
					StateName = x.Value.LocalizedName
				})
				.ToArray();
		}
	}
}