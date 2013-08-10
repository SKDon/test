using System;
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
		private readonly IApplicationHelper _applicationHelper;
		private readonly ITransitService _transitService;
		private readonly IUnitOfWork _unitOfWork;

		public ApplicationManager(
			IApplicationRepository applicationRepository,
			IApplicationUpdateRepository applicationUpdater,
			IStateConfig stateConfig,
			ITransitService transitService,
			IUnitOfWork unitOfWork, 
			IStateService stateService,
			IApplicationHelper applicationHelper)
		{
			_applicationRepository = applicationRepository;
			_applicationUpdater = applicationUpdater;
			_stateConfig = stateConfig;
			_transitService = transitService;
			_unitOfWork = unitOfWork;
			_stateService = stateService;
			_applicationHelper = applicationHelper;
		}

		public ApplicationEditModel Get(long id)
		{
			var data = _applicationRepository.Get(id);

			var application = new ApplicationEditModel(data);

			_applicationHelper.SetAdditionalData(application);

			return application;
		}

		public void Update(ApplicationEditModel model, CarrierSelectModel carrierSelectModel)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				_transitService.Update(model.Transit, carrierSelectModel);

				_applicationUpdater.Update(model.GetData(), model.SwiftFile, model.InvoiceFile, model.CPFile, model.DeliveryBillFile,
					model.Torg12File, model.PackingFile);

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}

		public void Add(ApplicationEditModel model, CarrierSelectModel carrierSelectModel)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				model.TransitId = _transitService.AddTransit(model.Transit, carrierSelectModel);
				model.StateId = _stateConfig.DefaultStateId;
				model.StateChangeTimestamp = DateTimeOffset.UtcNow;
				model.CreationTimestamp = DateTimeOffset.UtcNow;

				var id = _applicationUpdater.Add(model.GetData(), model.SwiftFile, model.InvoiceFile, model.CPFile, model.DeliveryBillFile, model.Torg12File, model.PackingFile);

				_unitOfWork.SaveChanges();

				model.Id = id();

				ts.Complete();
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