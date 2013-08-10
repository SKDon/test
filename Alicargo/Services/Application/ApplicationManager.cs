using System;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	// todo: test
	public sealed class ApplicationManager : IApplicationManager
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
				TransitId = data.TransitId,
				CountryId = data.CountryId,
				Volume = data.Volume,
				WarehouseWorkingTime = data.WarehouseWorkingTime,
			};

			SetAdditionalData(application);

			return application;
		}

		public void SetAdditionalData(params ApplicationEditModel[] applications)
		{
			SetTransitData(applications);
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

		public void Update(long id, ApplicationEditModel model, CarrierSelectModel carrierSelectModel)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				_transitService.Update(model.Transit, carrierSelectModel);

				var data = new ApplicationData
				{
					Id = id,
					// todo: finish
				};

				_applicationUpdater.Update(data, model.SwiftFile, model.InvoiceFile, model.CPFile, model.DeliveryBillFile,
										   model.Torg12File, model.PackingFile);

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}

		public long Add(ApplicationEditModel model, CarrierSelectModel carrierSelectModel)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				model.TransitId = _transitService.AddTransit(model.Transit, carrierSelectModel);

				var data = new ApplicationData
				{
					CreationTimestamp = DateTimeOffset.UtcNow,
					StateChangeTimestamp = DateTimeOffset.UtcNow,
					StateId = _stateConfig.DefaultStateId
					// todo: finish
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
			if (!_stateService.HasPermissionToSetState(stateId))
				throw new AccessForbiddenException("User don't have access to the state " + stateId);

			using (var ts = _unitOfWork.StartTransaction())
			{
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