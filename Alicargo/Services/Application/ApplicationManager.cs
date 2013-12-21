using System;
using System.Linq;
using Alicargo.Contracts.Contracts.Application;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Core.Services.Abstract;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class ApplicationManager : IApplicationManager
	{
		private readonly IApplicationRepository _applications;
		private readonly IApplicationUpdateRepository _applicationUpdater;
		private readonly IStateConfig _config;
		private readonly IIdentityService _identity;
		private readonly ITransitService _transitService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IStateSettingsRepository _settings;

		public ApplicationManager(
			IApplicationRepository applications,
			IApplicationUpdateRepository applicationUpdater,
			IStateConfig config,
			IIdentityService identity,
			ITransitService transitService,
			IUnitOfWork unitOfWork,
			IStateSettingsRepository settings)
		{
			_applications = applications;
			_applicationUpdater = applicationUpdater;
			_config = config;
			_identity = identity;
			_transitService = transitService;
			_unitOfWork = unitOfWork;
			_settings = settings;
		}

		public void Update(long applicationId, ApplicationAdminModel model, CarrierSelectModel carrierModel,
						   TransitEditModel transitModel)
		{
			var data = _applications.Get(applicationId);

			_transitService.Update(data.TransitId, transitModel, carrierModel);

			Map(model, data);

			_applicationUpdater.Update(data);

			_unitOfWork.SaveChanges();
		}

		public long Add(ApplicationAdminModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel,
						long clientId)
		{
			var transitId = _transitService.AddTransit(transitModel, carrierModel);

			var data = GetNewApplicationData(model, clientId, transitId);

			var id = _applicationUpdater.Add(data);

			_unitOfWork.SaveChanges();

			return id();
		}

		public void Delete(long id)
		{
			var applicationData = _applications.Get(id);

			_applicationUpdater.Delete(id);

			_unitOfWork.SaveChanges();

			_transitService.Delete(applicationData.TransitId);
		}

		public void SetTransitReference(long id, string transitReference)
		{
			SetState(id, _config.CargoOnTransitStateId);

			_applicationUpdater.SetTransitReference(id, transitReference);

			_unitOfWork.SaveChanges();
		}

		public void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt)
		{
			_applicationUpdater.SetDateOfCargoReceipt(id, dateOfCargoReceipt);

			_unitOfWork.SaveChanges();
		}

		public void SetTransitCost(long id, decimal? transitCost)
		{
			var calculations = _applications.GetCalculations(new[] { id });

			if (calculations.ContainsKey(id))
			{
				throw new InvalidLogicException("Can't set transit cost after a calculation was submitted.");
			}

			_applicationUpdater.SetTransitCost(id, transitCost);

			_unitOfWork.SaveChanges();
		}

		public void SetTransitCostEdited(long id, decimal? transitCost)
		{
			if (!_identity.IsInRole(RoleType.Admin))
			{
				throw new AccessForbiddenException("Edited value can be defined only be admin");
			}

			_applicationUpdater.SetTransitCostEdited(id, transitCost);

			_unitOfWork.SaveChanges();
		}

		public void SetTariffPerKg(long id, decimal? tariffPerKg)
		{
			_applicationUpdater.SetTariffPerKg(id, tariffPerKg);

			_unitOfWork.SaveChanges();
		}

		public void SetPickupCostEdited(long id, decimal? pickupCost)
		{
			if (!_identity.IsInRole(RoleType.Admin))
			{
				throw new AccessForbiddenException("Edited value can be defined only be admin");
			}

			_applicationUpdater.SetPickupCostEdited(id, pickupCost);

			_unitOfWork.SaveChanges();
		}

		public void SetFactureCostEdited(long id, decimal? factureCost)
		{
			if (!_identity.IsInRole(RoleType.Admin))
			{
				throw new AccessForbiddenException("Edited value can be defined only be admin");
			}

			_applicationUpdater.SetFactureCostEdited(id, factureCost);

			_unitOfWork.SaveChanges();
		}

		public void SetScotchCostEdited(long id, decimal? scotchCost)
		{
			if (!_identity.IsInRole(RoleType.Admin))
			{
				throw new AccessForbiddenException("Edited value can be defined only be admin");
			}

			_applicationUpdater.SetScotchCostEdited(id, scotchCost);

			_unitOfWork.SaveChanges();
		}

		public void SetSenderRate(long id, decimal? senderRate)
		{
			_applicationUpdater.SetSenderRate(id, senderRate);

			_unitOfWork.SaveChanges();
		}

		public void SetClass(long id, ClassType? classType)
		{
			_applicationUpdater.SetClass(id, (int?)classType);

			_unitOfWork.SaveChanges();
		}

		public void SetState(long applicationId, long stateId)
		{
			if (!HasPermissionToSetState(stateId))
				throw new AccessForbiddenException("User don't have access to the state " + stateId);

			// todo: 2. check permissions to the application for a sender

			// todo: 2. test logic with states
			if (stateId == _config.CargoInStockStateId)
			{
				_applicationUpdater.SetDateInStock(applicationId, DateTimeOffset.UtcNow);
			}

			_applicationUpdater.SetState(applicationId, stateId);

			_unitOfWork.SaveChanges();
		}

		private bool HasPermissionToSetState(long stateId)
		{
			return _settings.GetStateAvailabilities()
				.Any(x => x.StateId == stateId && _identity.IsInRole(x.Role));
		}

		private static void Map(ApplicationAdminModel @from, ApplicationData to)
		{
			to.Invoice = @from.Invoice;
			to.Characteristic = @from.Characteristic;
			to.AddressLoad = @from.AddressLoad;
			to.WarehouseWorkingTime = @from.WarehouseWorkingTime;
			to.Weight = @from.Weight;
			to.Count = @from.Count;
			to.Volume = @from.Volume;
			to.TermsOfDelivery = @from.TermsOfDelivery;
			to.Value = @from.Currency.Value;
			to.CurrencyId = @from.Currency.CurrencyId;
			to.CountryId = @from.CountryId;
			to.FactoryName = @from.FactoryName;
			to.FactoryPhone = @from.FactoryPhone;
			to.FactoryEmail = @from.FactoryEmail;
			to.FactoryContact = @from.FactoryContact;
			to.MarkName = @from.MarkName;
			to.MethodOfDeliveryId = (int)@from.MethodOfDelivery;
			to.FactureCost = @from.FactureCost;
			to.TransitCost = @from.TransitCost;
			to.PickupCost = @from.PickupCost;
			to.TariffPerKg = @from.TariffPerKg;
			to.FactureCostEdited = from.FactureCostEdited;
			to.TransitCostEdited = from.TransitCostEdited;
			to.PickupCostEdited = from.PickupCostEdited;
			to.ScotchCostEdited = from.ScotchCostEdited;
			to.SenderId = from.SenderId;
		}

		private ApplicationData GetNewApplicationData(ApplicationAdminModel model, long clientId, long transitId)
		{
			return new ApplicationData
			{
				CreationTimestamp = DateTimeOffset.UtcNow,
				StateChangeTimestamp = DateTimeOffset.UtcNow,
				StateId = _config.DefaultStateId,
				ClassId = null,
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
				MethodOfDeliveryId = (int)model.MethodOfDelivery,
				Id = 0,
				AirWaybillId = null,
				DateInStock = null,
				DateOfCargoReceipt = null,
				TransitReference = null,
				ClientId = clientId,
				PickupCost = model.PickupCost,
				TransitCost = model.TransitCost,
				FactureCost = model.FactureCost,
				TariffPerKg = model.TariffPerKg,
				ScotchCostEdited = model.ScotchCostEdited,
				FactureCostEdited = model.FactureCostEdited,
				TransitCostEdited = model.TransitCostEdited,
				PickupCostEdited = model.PickupCostEdited,
				SenderId = model.SenderId
			};
		}
	}
}