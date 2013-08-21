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

            // todo: 2. create mapper and test it
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
				WarehouseWorkingTime = data.WarehouseWorkingTime
			};

			return application;
		}

		public void Update(long applicationId, ApplicationEditModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				var data = _applicationRepository.Get(applicationId);

				_transitService.Update(data.TransitId, transitModel, carrierModel);

                // todo: 2. create mapper and test it
				data.Invoice = model.Invoice;
				data.InvoiceFileName = model.InvoiceFileName;
				data.SwiftFileName = model.SwiftFileName;
				data.PackingFileName = model.PackingFileName;
				data.DeliveryBillFileName = model.DeliveryBillFileName;
				data.Torg12FileName = model.Torg12FileName;
				data.CPFileName = model.CPFileName;
				data.Characteristic = model.Characteristic;
				data.AddressLoad = model.AddressLoad;
				data.WarehouseWorkingTime = model.WarehouseWorkingTime;
				data.Weigth = model.Weigth;
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
				data.MethodOfDeliveryId = model.MethodOfDeliveryId;

				_applicationUpdater.Update(data, model.SwiftFile, model.InvoiceFile, model.CPFile, model.DeliveryBillFile,
										   model.Torg12File, model.PackingFile);

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}

		public long Add(ApplicationEditModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel, long clientId)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				var transitId = _transitService.AddTransit(transitModel, carrierModel);

                // todo: 2. create mapper and test it
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
					ClientId = clientId
				};

				var id = _applicationUpdater.Add(data, model.SwiftFile, model.InvoiceFile, model.CPFile, model.DeliveryBillFile,
												 model.Torg12File, model.PackingFile);

				_unitOfWork.SaveChanges();

				ts.Complete();

				return id();
			}
		}

		public void Delete(long id)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				var applicationData = _applicationRepository.Get(id);

				_applicationUpdater.Delete(id);

				_unitOfWork.SaveChanges();

				_transitService.Delete(applicationData.TransitId);

				ts.Complete();
			}
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

		public void SetState(long applicationId, long stateId)
		{
            // todo: 2. permission interception
			if (!_stateService.HasPermissionToSetState(stateId))
				throw new AccessForbiddenException("User don't have access to the state " + stateId);

			using (var ts = _unitOfWork.StartTransaction())
			{
                // todo: 2. test logic with states
				if (stateId == _stateConfig.CargoInStockStateId)
				{
					_applicationUpdater.SetDateInStock(applicationId, DateTimeOffset.UtcNow);
				}

				_applicationUpdater.SetState(applicationId, stateId);

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}
	}
}