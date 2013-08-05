using System;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Services.Application
{
	// todo: test
	public sealed class ApplicationManager : IApplicationManager
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IStateConfig _stateConfig;
		private readonly IStateService _stateService;
		private readonly ITransitService _transitService;
		private readonly IUnitOfWork _unitOfWork;

		public ApplicationManager(
			IApplicationRepository applicationRepository,
			IStateConfig stateConfig,
			ITransitService transitService,
			IUnitOfWork unitOfWork, IStateService stateService)
		{
			_applicationRepository = applicationRepository;
			_stateConfig = stateConfig;
			_transitService = transitService;
			_unitOfWork = unitOfWork;
			_stateService = stateService;
		}

		public void Update(ApplicationModel model, CarrierSelectModel carrierSelectModel)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				_transitService.Update(model.Transit, carrierSelectModel);

				_applicationRepository.Update(model, model.SwiftFile, model.InvoiceFile, model.CPFile, model.DeliveryBillFile, model.Torg12File, model.PackingFile);

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}

		public void Add(ApplicationModel model, CarrierSelectModel carrierSelectModel)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				model.TransitId = _transitService.AddTransit(model.Transit, carrierSelectModel);
				model.StateId = _stateConfig.DefaultStateId;
				model.StateChangeTimestamp = DateTimeOffset.UtcNow;
				model.CreationTimestamp = DateTimeOffset.UtcNow;

				var id = _applicationRepository.Add(model, model.SwiftFile, model.InvoiceFile, model.CPFile, model.DeliveryBillFile, model.Torg12File, model.PackingFile);

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

				_applicationRepository.Delete(id);

				_unitOfWork.SaveChanges();

				_transitService.Delete(applicationData.TransitId);

				ts.Complete();
			}
		}

		public void SetTransitReference(long id, string transitReference)
		{
			_applicationRepository.SetTransitReference(id, transitReference);
			_unitOfWork.SaveChanges();
		}

		public void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt)
		{
			_applicationRepository.SetDateOfCargoReceipt(id, dateOfCargoReceipt);
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
					_applicationRepository.SetDateInStock(applicationId, DateTimeOffset.UtcNow);
				}

				_applicationRepository.SetState(applicationId, stateId);
				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}
	}
}