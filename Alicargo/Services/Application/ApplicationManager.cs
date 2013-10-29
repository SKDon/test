using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class ApplicationManager : IApplicationManager
	{
		private readonly IApplicationRepository _applications;
		private readonly IApplicationUpdateRepository _applicationUpdater;
		private readonly IStateConfig _stateConfig;
		private readonly IStateService _stateService;
		private readonly ITransitService _transitService;
		private readonly IUnitOfWork _unitOfWork;

		public ApplicationManager(
			IApplicationRepository applications,
			IApplicationUpdateRepository applicationUpdater,
			IStateConfig stateConfig,
			ITransitService transitService,
			IUnitOfWork unitOfWork,
			IStateService stateService)
		{
			_applications = applications;
			_applicationUpdater = applicationUpdater;
			_stateConfig = stateConfig;
			_transitService = transitService;
			_unitOfWork = unitOfWork;
			_stateService = stateService;
		}

		public ApplicationAdminModel Get(long id)
		{
			var data = _applications.Get(id);

			var application = GetModel(data);

			return application;
		}

		public void Update(long applicationId, ApplicationAdminModel model, CarrierSelectModel carrierModel,
						   TransitEditModel transitModel)
		{
			var data = _applications.Get(applicationId);

			_transitService.Update(data.TransitId, transitModel, carrierModel);

			Map(model, data);

			_applicationUpdater.Update(data, model.SwiftFile, model.InvoiceFile, model.CPFile, model.DeliveryBillFile,
									   model.Torg12File, model.PackingFile);

			_unitOfWork.SaveChanges();
		}

		public long Add(ApplicationAdminModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel,
						long clientId)
		{
			var transitId = _transitService.AddTransit(transitModel, carrierModel);

			var data = GetNewApplicationData(model, clientId, transitId);

			var id = _applicationUpdater.Add(data, model.SwiftFile, model.InvoiceFile, model.CPFile, model.DeliveryBillFile,
											 model.Torg12File, model.PackingFile);

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

		public void SetTariffPerKg(long id, decimal? tariffPerKg)
		{
			_applicationUpdater.SetTariffPerKg(id, tariffPerKg);

			_unitOfWork.SaveChanges();
		}

		public void SetPickupCostEdited(long id, decimal? pickupCost)
		{
			_applicationUpdater.SetPickupCostEdited(id, pickupCost);

			_unitOfWork.SaveChanges();
		}

		public void SetFactureCostEdited(long id, decimal? factureCost)
		{
			_applicationUpdater.SetFactureCostEdited(id, factureCost);

			_unitOfWork.SaveChanges();
		}

		public void SetScotchCostEdited(long id, decimal? scotchCost)
		{
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

		public void SetTransitCostEdited(long id, decimal? transitCost)
		{
			_applicationUpdater.SetTransitCostEdited(id, transitCost);

			_unitOfWork.SaveChanges();
		}

		public void SetState(long applicationId, long stateId)
		{
			if (!_stateService.HasPermissionToSetState(stateId))
				throw new AccessForbiddenException("User don't have access to the state " + stateId);

			// todo: 2. check permissions to the application for a sender

			// todo: 2. test logic with states
			if (stateId == _stateConfig.CargoInStockStateId)
			{
				_applicationUpdater.SetDateInStock(applicationId, DateTimeOffset.UtcNow);
			}

			_applicationUpdater.SetState(applicationId, stateId);

			_unitOfWork.SaveChanges();
		}

		private static ApplicationAdminModel GetModel(ApplicationData data)
		{
			var application = new ApplicationAdminModel
			{
				AddressLoad = data.AddressLoad,
				Characteristic = data.Characteristic,
				Count = data.Count,
				CPFileName = data.CPFileName,
				Currency = new CurrencyModel
				{
					CurrencyId = data.CurrencyId,
					Value = data.Value
				},
				DeliveryBillFileName = data.DeliveryBillFileName,
				FactoryContact = data.FactoryContact,
				FactoryEmail = data.FactoryEmail,
				FactoryName = data.FactoryName,
				FactoryPhone = data.FactoryPhone,
				Weigth = data.Weigth,
				Invoice = data.Invoice,
				InvoiceFileName = data.InvoiceFileName,
				PackingFileName = data.PackingFileName,
				MarkName = data.MarkName,
				MethodOfDelivery = (MethodOfDelivery)data.MethodOfDeliveryId,
				SwiftFileName = data.SwiftFileName,
				TermsOfDelivery = data.TermsOfDelivery,
				Torg12FileName = data.Torg12FileName,
				CountryId = data.CountryId,
				Volume = data.Volume,
				WarehouseWorkingTime = data.WarehouseWorkingTime,
				FactureCost = data.FactureCost,
				InvoiceFile = null,
				PackingFile = null,
				CPFile = null,
				DeliveryBillFile = null,
				SwiftFile = null,
				Torg12File = null,
				TransitCost = data.TransitCost,
				PickupCost = data.PickupCost,
				TariffPerKg = data.TariffPerKg,
				ScotchCostEdited = data.ScotchCostEdited,
				FactureCostEdited = data.FactureCostEdited,
				TransitCostEdited = data.TransitCostEdited,
				PickupCostEdited = data.PickupCostEdited,
				SenderId = data.SenderId
			};
			return application;
		}

		private static void Map(ApplicationAdminModel @from, ApplicationData to)
		{
			to.Invoice = @from.Invoice;
			to.InvoiceFileName = @from.InvoiceFileName;
			to.SwiftFileName = @from.SwiftFileName;
			to.PackingFileName = @from.PackingFileName;
			to.DeliveryBillFileName = @from.DeliveryBillFileName;
			to.Torg12FileName = @from.Torg12FileName;
			to.CPFileName = @from.CPFileName;
			to.Characteristic = @from.Characteristic;
			to.AddressLoad = @from.AddressLoad;
			to.WarehouseWorkingTime = @from.WarehouseWorkingTime;
			to.Weigth = @from.Weigth;
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
				StateId = _stateConfig.DefaultStateId,
				ClassId = null,
				TransitId = transitId,
				Invoice = model.Invoice,
				InvoiceFileName = model.InvoiceFileName,
				SwiftFileName = model.SwiftFileName,
				PackingFileName = model.PackingFileName,
				DeliveryBillFileName = model.DeliveryBillFileName,
				Torg12FileName = model.Torg12FileName,
				CPFileName = model.CPFileName,
				Characteristic = model.Characteristic,
				AddressLoad = model.AddressLoad,
				WarehouseWorkingTime = model.WarehouseWorkingTime,
				Weigth = model.Weigth,
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