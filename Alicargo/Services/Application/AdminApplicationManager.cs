using System;
using System.Linq;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.State;
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
		private readonly IIdentityService _identity;
		private readonly ISenderService _senders;
		private readonly IStateSettingsRepository _settings;
		private readonly ITransitService _transitService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IApplicationUpdateRepository _updater;

		public AdminApplicationManager(
			IApplicationRepository applications,
			ISenderService senders,
			IApplicationUpdateRepository updater,
			IStateConfig config,
			IIdentityService identity,
			ITransitService transitService,
			IUnitOfWork unitOfWork,
			IStateSettingsRepository settings)
		{
			_applications = applications;
			_senders = senders;
			_updater = updater;
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

			_updater.Update(data);

			_unitOfWork.SaveChanges();
		}

		public long Add(ApplicationAdminModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel,
			long clientId)
		{
			var transitId = _transitService.AddTransit(transitModel, carrierModel);

			var data = GetNewApplicationData(model, clientId, transitId);

			var id = _updater.Add(data);

			_unitOfWork.SaveChanges();

			return id();
		}

		public void Delete(long id)
		{
			var applicationData = _applications.Get(id);

			_updater.Delete(id);

			_unitOfWork.SaveChanges();

			_transitService.Delete(applicationData.TransitId);
		}

		public void SetTransitReference(long id, string transitReference)
		{
			SetState(id, _config.CargoOnTransitStateId);

			_updater.SetTransitReference(id, transitReference);

			_unitOfWork.SaveChanges();
		}

		public void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt)
		{
			_updater.SetDateOfCargoReceipt(id, dateOfCargoReceipt);

			_unitOfWork.SaveChanges();
		}

		public void SetTransitCost(long id, decimal? transitCost)
		{
			var calculations = _applications.GetCalculations(new[] { id });

			if(calculations.ContainsKey(id))
			{
				throw new InvalidLogicException("Can't set transit cost after a calculation was submitted.");
			}

			_updater.SetTransitCost(id, transitCost);

			_unitOfWork.SaveChanges();
		}

		public void SetTransitCostEdited(long id, decimal? transitCost)
		{
			if(!_identity.IsInRole(RoleType.Admin))
			{
				throw new AccessForbiddenException("Edited value can be defined only be admin");
			}

			_updater.SetTransitCostEdited(id, transitCost);

			_unitOfWork.SaveChanges();
		}

		public void SetTariffPerKg(long id, decimal? tariffPerKg)
		{
			_updater.SetTariffPerKg(id, tariffPerKg);

			_unitOfWork.SaveChanges();
		}

		public void SetPickupCostEdited(long id, decimal? pickupCost)
		{
			if(!_identity.IsInRole(RoleType.Admin))
			{
				throw new AccessForbiddenException("Edited value can be defined only be admin");
			}

			_updater.SetPickupCostEdited(id, pickupCost);

			_unitOfWork.SaveChanges();
		}

		public void SetFactureCostEdited(long id, decimal? factureCost)
		{
			if(!_identity.IsInRole(RoleType.Admin))
			{
				throw new AccessForbiddenException("Edited value can be defined only be admin");
			}

			_updater.SetFactureCostEdited(id, factureCost);

			_unitOfWork.SaveChanges();
		}

		public void SetScotchCostEdited(long id, decimal? scotchCost)
		{
			if(!_identity.IsInRole(RoleType.Admin))
			{
				throw new AccessForbiddenException("Edited value can be defined only be admin");
			}

			_updater.SetScotchCostEdited(id, scotchCost);

			_unitOfWork.SaveChanges();
		}

		public void SetSenderRate(long id, decimal? senderRate)
		{
			_updater.SetSenderRate(id, senderRate);

			_unitOfWork.SaveChanges();
		}

		public void SetClass(long id, ClassType? classType)
		{
			_updater.SetClass(id, (int?)classType);

			_unitOfWork.SaveChanges();
		}

		public void SetState(long applicationId, long stateId)
		{
			if(!HasPermissionToSetState(stateId))
				throw new AccessForbiddenException("User don't have access to the state " + stateId);

			// todo: 2. check permissions to the application for a sender

			// todo: 2. test logic with states
			if(stateId == _config.CargoInStockStateId)
			{
				_updater.SetDateInStock(applicationId, DateTimeProvider.Now);
			}

			_updater.SetState(applicationId, stateId);

			_unitOfWork.SaveChanges();
		}

		private ApplicationData GetNewApplicationData(ApplicationAdminModel model, long clientId, long transitId)
		{
			return new ApplicationData
			{
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
				SenderId = GetSenderId(model, null),
				ForwarderId = model.ForwarderId,
				SenderRate = null
			};
		}

		private long GetSenderId(ApplicationAdminModel model, long? oldSenderId)
		{
			return model.SenderId.HasValue
				? model.SenderId.Value
				: _senders.GetByCountryOrAny(model.CountryId, oldSenderId);
		}

		private bool HasPermissionToSetState(long stateId)
		{
			return _settings.GetStateAvailabilities()
				.Any(x => x.StateId == stateId && _identity.IsInRole(x.Role));
		}

		private void Map(ApplicationAdminModel @from, ApplicationData to)
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
			to.MethodOfDelivery = @from.MethodOfDelivery;
			to.FactureCost = @from.FactureCost;
			to.TransitCost = @from.TransitCost;
			to.PickupCost = @from.PickupCost;
			to.TariffPerKg = @from.TariffPerKg;
			to.FactureCostEdited = from.FactureCostEdited;
			to.TransitCostEdited = from.TransitCostEdited;
			to.PickupCostEdited = from.PickupCostEdited;
			to.ScotchCostEdited = from.ScotchCostEdited;
			to.SenderId = GetSenderId(@from, to.SenderId);
			to.ForwarderId = from.ForwarderId;
		}
	}
}