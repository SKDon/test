using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class ApplicationManager : IApplicationManager
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IApplicationUpdateRepository _applicationUpdater;
		private readonly IStateConfig _stateConfig;
		private readonly IStateService _stateService;
		private readonly ITransitService _transitService;
		private readonly IUnitOfWork _unitOfWork;

		public ApplicationManager(
			IApplicationRepository applicationRepository,
			IApplicationUpdateRepository applicationUpdater,
			IStateConfig stateConfig,
			ITransitService transitService,
			IUnitOfWork unitOfWork,
			IStateService stateService)
		{
			_applicationRepository = applicationRepository;
			_applicationUpdater = applicationUpdater;
			_stateConfig = stateConfig;
			_transitService = transitService;
			_unitOfWork = unitOfWork;
			_stateService = stateService;
		}

		public ApplicationEditModel Get(long id)
		{
			var data = _applicationRepository.Get(id);

			var application = GetApplicationEditModel(data);

			return application;
		}

		public void Update(long applicationId, ApplicationEditModel model, CarrierSelectModel carrierModel,
						   TransitEditModel transitModel)
		{
			var data = _applicationRepository.Get(applicationId);

			_transitService.Update(data.TransitId, transitModel, carrierModel);

			Map(model, data);

			_applicationUpdater.Update(data, model.SwiftFile, model.InvoiceFile, model.CPFile, model.DeliveryBillFile,
									   model.Torg12File, model.PackingFile);

			_unitOfWork.SaveChanges();
		}

		public long Add(ApplicationEditModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel,
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
			var applicationData = _applicationRepository.Get(id);

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
			_applicationUpdater.SetTransitCost(id, transitCost);

			_unitOfWork.SaveChanges();
		}

		public void SetState(long applicationId, long stateId)
		{
			if (!_stateService.HasPermissionToSetState(stateId))
				throw new AccessForbiddenException("User don't have access to the state " + stateId);

			// todo: 2. test logic with states
			if (stateId == _stateConfig.CargoInStockStateId)
			{
				_applicationUpdater.SetDateInStock(applicationId, DateTimeOffset.UtcNow);
			}

			_applicationUpdater.SetState(applicationId, stateId);

			_unitOfWork.SaveChanges();
		}

		private static ApplicationEditModel GetApplicationEditModel(ApplicationData data)
		{
			var application = new ApplicationEditModel
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
				MethodOfDeliveryId = data.MethodOfDeliveryId,
				SwiftFileName = data.SwiftFileName,
				TermsOfDelivery = data.TermsOfDelivery,
				Torg12FileName = data.Torg12FileName,
				CountryId = data.CountryId,
				Volume = data.Volume,
				WarehouseWorkingTime = data.WarehouseWorkingTime,
				ForwarderCost = data.ForwarderCost,
				FactureCost = data.FactureCost,
				InvoiceFile = null,
				PackingFile = null,
				CPFile = null,
				DeliveryBillFile = null,
				ScotchCost = data.ScotchCost,
				SwiftFile = null,
				Torg12File = null,
				TransitCost = data.TransitCost,
				WithdrawCost = data.WithdrawCost,
				TariffPerKg = data.TariffPerKg
			};
			return application;
		}

		private static void Map(ApplicationEditModel @from, ApplicationData to)
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
			to.MethodOfDeliveryId = @from.MethodOfDeliveryId;
			to.ForwarderCost = @from.ForwarderCost;
			to.FactureCost = @from.FactureCost;
			to.ScotchCost = @from.ScotchCost;
			to.TransitCost = @from.TransitCost;
			to.WithdrawCost = @from.WithdrawCost;
			to.TariffPerKg = @from.TariffPerKg;
		}

		private ApplicationData GetNewApplicationData(ApplicationEditModel model, long clientId, long transitId)
		{
			var data = new ApplicationData
			{
				CreationTimestamp = DateTimeOffset.UtcNow,
				StateChangeTimestamp = DateTimeOffset.UtcNow,
				StateId = _stateConfig.DefaultStateId,
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
				MethodOfDeliveryId = model.MethodOfDeliveryId,
				Id = 0,
				AirWaybillId = null,
				DateInStock = null,
				DateOfCargoReceipt = null,
				TransitReference = null,
				ClientId = clientId,
				WithdrawCost = model.WithdrawCost,
				ScotchCost = model.ScotchCost,
				TransitCost = model.TransitCost,
				FactureCost = model.FactureCost,
				ForwarderCost = model.ForwarderCost,
				TariffPerKg = model.TariffPerKg				
			};
			return data;
		}
	}
}