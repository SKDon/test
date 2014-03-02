﻿using System;
using System.Linq;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.State;
using Alicargo.Core.Contracts.Users;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Services.Abstract;
using Alicargo.Utilities;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class AdminApplicationManager : IAdminApplicationManager
	{
		private readonly IApplicationRepository _applications;
		private readonly IStateConfig _config;
		private readonly IApplicationEditor _editor;
		private readonly IForwarderService _forwarders;
		private readonly IIdentityService _identity;
		private readonly ISenderService _senders;
		private readonly IStateSettingsRepository _settings;
		private readonly ITransitService _transitService;

		public AdminApplicationManager(
			IApplicationRepository applications,
			IForwarderService forwarders,
			ISenderService senders,
			IApplicationEditor editor,
			IStateConfig config,
			IIdentityService identity,
			ITransitService transitService,
			IStateSettingsRepository settings)
		{
			_applications = applications;
			_forwarders = forwarders;
			_senders = senders;
			_editor = editor;
			_config = config;
			_identity = identity;
			_transitService = transitService;
			_settings = settings;
		}

		public long Add(ApplicationAdminModel model, TransitEditModel transit,
			long clientId)
		{
			var transitId = _transitService.Add(transit, model.CarrierId);

			var data = new ApplicationData
			{
				Id = 0,
				CreationTimestamp = DateTimeProvider.Now,
				StateChangeTimestamp = DateTimeProvider.Now,
				StateId = _config.DefaultStateId,
				Class = null,
				TransitId = transitId,
				Invoice = model.Invoice,
				Characteristic = model.Characteristic,
				AddressLoad = model.AddressLoad,
				WarehouseWorkingTime = model.WarehouseWorkingTime,
				Weight = model.Weight,
				Count = model.Count,
				Volume = model.Volume,
				TermsOfDelivery = model.TermsOfDelivery,
				Value = model.Currency.Value,
				CurrencyId = model.Currency.CurrencyId,
				CountryId = model.CountryId,
				FactoryName = model.FactoryName,
				FactoryPhone = model.FactoryPhone,
				FactoryEmail = model.FactoryEmail,
				FactoryContact = model.FactoryContact,
				MarkName = model.MarkName,
				MethodOfDelivery = model.MethodOfDelivery,
				AirWaybillId = null,
				DateInStock = null,
				DateOfCargoReceipt = null,
				TransitReference = null,
				ClientId = clientId,
				PickupCost = model.PickupCost,
				TransitCost = model.TransitCost,
				FactureCost = model.FactureCost,
				FactureCostEx = model.FactureCostEx,
				TariffPerKg = model.TariffPerKg,
				ScotchCostEdited = model.ScotchCostEdited,
				FactureCostEdited = model.FactureCostEdited,
				FactureCostExEdited = model.FactureCostExEdited,
				TransitCostEdited = model.TransitCostEdited,
				PickupCostEdited = model.PickupCostEdited,
				SenderId = GetSenderId(model.SenderId, model.CountryId, null),
				ForwarderId = GetForwarderId(model.ForwarderId, transit.CityId, null),
				SenderRate = null,
				InsuranceRate = model.InsuranceRate / 100,
				InsuranceRateForClient = model.InsuranceRateForClient / 100
			};

			return _editor.Add(data);
		}

		public void Update(long applicationId, ApplicationAdminModel model, TransitEditModel transit)
		{
			var data = _applications.Get(applicationId);

			_transitService.Update(data.TransitId, transit, model.CarrierId, applicationId);

			data.Invoice = model.Invoice;
			data.Characteristic = model.Characteristic;
			data.AddressLoad = model.AddressLoad;
			data.WarehouseWorkingTime = model.WarehouseWorkingTime;
			data.Weight = model.Weight;
			data.Count = model.Count;
			data.Volume = model.Volume;
			data.TermsOfDelivery = model.TermsOfDelivery;
			data.Value = model.Currency.Value;
			data.CurrencyId = model.Currency.CurrencyId;
			data.CountryId = model.CountryId;
			data.FactoryName = model.FactoryName;
			data.FactoryPhone = model.FactoryPhone;
			data.FactoryEmail = model.FactoryEmail;
			data.FactoryContact = model.FactoryContact;
			data.MarkName = model.MarkName;
			data.MethodOfDelivery = model.MethodOfDelivery;
			data.FactureCost = model.FactureCost;
			data.FactureCostEx = model.FactureCostEx;
			data.TransitCost = model.TransitCost;
			data.PickupCost = model.PickupCost;
			data.TariffPerKg = model.TariffPerKg;
			data.FactureCostEdited = model.FactureCostEdited;
			data.FactureCostExEdited = model.FactureCostExEdited;
			data.TransitCostEdited = model.TransitCostEdited;
			data.PickupCostEdited = model.PickupCostEdited;
			data.ScotchCostEdited = model.ScotchCostEdited;
			data.SenderId = GetSenderId(model.SenderId, model.CountryId, data.SenderId);
			data.ForwarderId = GetForwarderId(model.ForwarderId, transit.CityId, data.ForwarderId);
			data.InsuranceRate = model.InsuranceRate / 100;
			data.InsuranceRateForClient = model.InsuranceRateForClient / 100;

			_editor.Update(data);
		}

		public void Delete(long id)
		{
			var applicationData = _applications.Get(id);

			_editor.Delete(id);

			_transitService.Delete(applicationData.TransitId);
		}

		public void SetTransitReference(long id, string transitReference)
		{
			SetState(id, _config.CargoOnTransitStateId);

			_editor.SetTransitReference(id, transitReference);
		}

		public void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt)
		{
			_editor.SetDateOfCargoReceipt(id, dateOfCargoReceipt);
		}

		public void SetTransitCost(long id, decimal? transitCost)
		{
			var calculations = _applications.GetCalculations(new[] { id });

			if(calculations.ContainsKey(id))
			{
				throw new InvalidLogicException("Can't set transit cost after a calculation was submitted.");
			}

			_editor.SetTransitCost(id, transitCost);
		}

		public void SetTransitCostEdited(long id, decimal? transitCost)
		{
			if(!_identity.IsInRole(RoleType.Admin))
			{
				throw new AccessForbiddenException("Edited value can be defined only be admin");
			}

			_editor.SetTransitCostEdited(id, transitCost);
		}

		public void SetCount(long id, int? value)
		{
			_editor.SetCount(id, value);
		}

		public void SetWeight(long id, float? value)
		{
			_editor.SetWeight(id, value);
		}

		public void SetInsuranceCost(long id, float? value)
		{
			var data = _applications.Get(id);

			var insurance = value.HasValue ? (decimal)value.Value / data.Value : 0;

			_editor.SetInsuranceRate(id, (float)insurance);
		}

		public void SetInsuranceCostForClient(long id, float? value)
		{
			var data = _applications.Get(id);

			var insurance = value.HasValue ? (decimal)value.Value / data.Value : 0;

			_editor.SetInsuranceRateForClient(id, (float)insurance);
		}

		public void SetTariffPerKg(long id, decimal? tariffPerKg)
		{
			_editor.SetTariffPerKg(id, tariffPerKg);
		}

		public void SetPickupCostEdited(long id, decimal? pickupCost)
		{
			if(!_identity.IsInRole(RoleType.Admin))
			{
				throw new AccessForbiddenException("Edited value can be defined only be admin");
			}

			_editor.SetPickupCostEdited(id, pickupCost);
		}

		public void SetFactureCostEdited(long id, decimal? factureCost)
		{
			if(!_identity.IsInRole(RoleType.Admin))
			{
				throw new AccessForbiddenException("Edited value can be defined only be admin");
			}

			_editor.SetFactureCostEdited(id, factureCost);
		}

		public void SetFactureCostExEdited(long id, decimal? factureCostEx)
		{
			if(!_identity.IsInRole(RoleType.Admin))
			{
				throw new AccessForbiddenException("Edited value can be defined only be admin");
			}

			_editor.SetFactureCostExEdited(id, factureCostEx);
		}

		public void SetScotchCostEdited(long id, decimal? scotchCost)
		{
			if(!_identity.IsInRole(RoleType.Admin))
			{
				throw new AccessForbiddenException("Edited value can be defined only be admin");
			}

			_editor.SetScotchCostEdited(id, scotchCost);
		}

		public void SetSenderRate(long id, decimal? senderRate)
		{
			_editor.SetSenderRate(id, senderRate);
		}

		public void SetClass(long id, ClassType? classType)
		{
			_editor.SetClass(id, (int?)classType);
		}

		public void SetState(long applicationId, long stateId)
		{
			if(!HasPermissionToSetState(stateId))
				throw new AccessForbiddenException("User don't have access to the state " + stateId);

			// todo: 2. test logic with states
			if(stateId == _config.CargoInStockStateId)
			{
				_editor.SetDateInStock(applicationId, DateTimeProvider.Now);
			}

			_editor.SetState(applicationId, stateId);
		}

		private long GetForwarderId(long? forwarderId, long cityId, long? oldForwarderId)
		{
			return forwarderId.HasValue
				? forwarderId.Value
				: _forwarders.GetByCityOrAny(cityId, oldForwarderId);
		}

		private long GetSenderId(long? senderId, long countryId, long? oldSenderId)
		{
			return senderId.HasValue
				? senderId.Value
				: _senders.GetByCountryOrAny(countryId, oldSenderId);
		}

		private bool HasPermissionToSetState(long stateId)
		{
			return _settings.GetStateAvailabilities()
				.Any(x => x.StateId == stateId && _identity.IsInRole(x.Role));
		}
	}
}