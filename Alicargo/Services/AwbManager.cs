using System;
using System.Linq;
using Alicargo.Core.Contracts;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Microsoft.Ajax.Utilities;

namespace Alicargo.Services
{
	public sealed class AwbManager : IAwbManager
	{
		private readonly IReferenceRepository _referenceRepository;
		private readonly IApplicationRepository _applicationRepository;
		private readonly IApplicationManager _applicationManager;
		private readonly IStateConfig _stateConfig;

		private readonly IUnitOfWork _unitOfWork;

		public AwbManager(
			IReferenceRepository referenceRepository,
			IApplicationRepository applicationRepository,
			IUnitOfWork unitOfWork,
			IApplicationManager applicationManager,
			IStateConfig stateConfig)
		{
			_referenceRepository = referenceRepository;
			_applicationRepository = applicationRepository;
			_unitOfWork = unitOfWork;
			_applicationManager = applicationManager;
			_stateConfig = stateConfig;
		}

		public void Create(long applicationId, ReferenceModel model)
		{
			model.CreationTimestamp = DateTimeOffset.UtcNow;
			model.StateChangeTimestamp = DateTimeOffset.UtcNow;

			using (var ts = _unitOfWork.StartTransaction())
			{
				model.StateId = _stateConfig.CargoIsFlewStateId;
				var id = _referenceRepository.Add(model, model.GTDFile, model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile);
				_unitOfWork.SaveChanges();
				model.Id = id();

				SetAwb(applicationId, model.Id);

				ts.Complete();
			}
		}

		// todo: refactor with decorator
		public void SetAwb(long applicationId, long? awbId)
		{
			if (awbId.HasValue)
			{
				var aggregate = _referenceRepository.GetAggregate(awbId.Value).First();

				using (var ts = _unitOfWork.StartTransaction())
				{
					// SetReference must be first
					_applicationRepository.SetReference(applicationId, awbId.Value);

					_applicationManager.SetState(applicationId, aggregate.StateId);

					_unitOfWork.SaveChanges();

					ts.Complete();
				}
			}
			else
			{
				_applicationRepository.SetReference(applicationId, null);

				_unitOfWork.SaveChanges();
			}
		}

		public void Update(ReferenceModel model)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				var old = _referenceRepository.Get(model.Id).First();

				// Update must be before SetState
				_referenceRepository.Update(model, model.GTDFile, model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile);

				if (IsReadyForCargoAtCustomsStateId(model))
				{
					if (!IsReadyForCargoAtCustomsStateId(old) && old.StateId != _stateConfig.CargoIsCustomsClearedStateId)
						SetState(model.Id, _stateConfig.CargoAtCustomsStateId);
				}

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}

		private static bool IsReadyForCargoAtCustomsStateId(IReferenceData model)
		{
			return !model.GTD.IsNullOrWhiteSpace();
		}

		// todo: test
		public void Delete(long id)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				var applicationDatas = _applicationRepository.GetByReference(id);

				foreach (var app in applicationDatas)
				{
					_applicationRepository.SetReference(app.Id, null);
				}

				_referenceRepository.Delete(id);

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}

		public void SetState(long referenceId, long stateId)
		{
			var reference = _referenceRepository.Get(referenceId).First();

			var applications = _applicationRepository.GetByReference(referenceId);

			using (var ts = _unitOfWork.StartTransaction())
			{
				_referenceRepository.SetState(referenceId, stateId);

				foreach (var application in applications)
				{
					if (reference.StateId == application.StateId)
					{
						_applicationManager.SetState(application.Id, stateId);
					}
				}

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}
	}
}