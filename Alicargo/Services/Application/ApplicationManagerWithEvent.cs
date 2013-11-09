using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Jobs.Entities;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class ApplicationManagerWithEvent : IApplicationManager
	{
		private readonly IApplicationEventRepository _events;
		private readonly IApplicationManager _manager;

		public ApplicationManagerWithEvent(IApplicationManager manager, IApplicationEventRepository events)
		{
			_manager = manager;
			_events = events;
		}

		public void Update(long applicationId, ApplicationAdminModel model, CarrierSelectModel carrierModel,
			TransitEditModel transitModel)
		{
			_manager.Update(applicationId, model, carrierModel, transitModel);

			HandleFilesUpload(applicationId, model);
		}

		public long Add(ApplicationAdminModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel,
			long clientId)
		{
			var applicationId = _manager.Add(model, carrierModel, transitModel, clientId);

			_events.Add(applicationId, ApplicationEventType.Created, new ApplicationCreatedEventData
			{
				ClientId = clientId
			});

			return applicationId;
		}

		public void Delete(long id)
		{
			_manager.Delete(id);
		}

		public void SetState(long applicationId, long stateId)
		{
			_manager.SetState(applicationId, stateId);

			_events.Add(applicationId, ApplicationEventType.SetState, stateId);
		}

		public void SetTransitReference(long id, string transitReference)
		{
			_manager.SetTransitReference(id, transitReference);

			_events.Add(id, ApplicationEventType.SetTransitReference, transitReference);
		}

		public void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt)
		{
			_manager.SetDateOfCargoReceipt(id, dateOfCargoReceipt);

			_events.Add(id, ApplicationEventType.SetDateOfCargoReceipt, dateOfCargoReceipt);
		}

		public void SetTransitCost(long id, decimal? transitCost)
		{
			_manager.SetTransitCost(id, transitCost);
		}

		public void SetTariffPerKg(long id, decimal? tariffPerKg)
		{
			_manager.SetTariffPerKg(id, tariffPerKg);
		}

		public void SetPickupCostEdited(long id, decimal? pickupCost)
		{
			_manager.SetPickupCostEdited(id, pickupCost);
		}

		public void SetFactureCostEdited(long id, decimal? factureCost)
		{
			_manager.SetFactureCostEdited(id, factureCost);
		}

		public void SetScotchCostEdited(long id, decimal? scotchCost)
		{
			_manager.SetScotchCostEdited(id, scotchCost);
		}

		public void SetSenderRate(long id, decimal? senderRate)
		{
			_manager.SetSenderRate(id, senderRate);
		}

		public void SetClass(long id, ClassType? classType)
		{
			_manager.SetClass(id, classType);
		}

		public void SetTransitCostEdited(long id, decimal? transitCost)
		{
			_manager.SetTransitCostEdited(id, transitCost);
		}

		private void HandleFilesUpload(long applicationId, ApplicationAdminModel model)
		{
			if (model.CPFile != null && model.CPFile.Length != 0)
			{
				_events.Add(applicationId, ApplicationEventType.CPFileUploaded, new FileHolder
				{
					Data = model.CPFile,
					Name = model.CPFileName
				});
			}

			if (model.InvoiceFile != null && model.InvoiceFile.Length != 0)
			{
				_events.Add(applicationId, ApplicationEventType.InvoiceFileUploaded, new FileHolder
				{
					Data = model.InvoiceFile,
					Name = model.InvoiceFileName
				});
			}

			if (model.PackingFile != null && model.PackingFile.Length != 0)
			{
				_events.Add(applicationId, ApplicationEventType.PackingFileUploaded, new FileHolder
				{
					Data = model.PackingFile,
					Name = model.PackingFileName
				});
			}

			if (model.SwiftFile != null && model.SwiftFile.Length != 0)
			{
				_events.Add(applicationId, ApplicationEventType.SwiftFileUploaded, new FileHolder
				{
					Data = model.SwiftFile,
					Name = model.SwiftFileName
				});
			}

			if (model.DeliveryBillFile != null && model.DeliveryBillFile.Length != 0)
			{
				_events.Add(applicationId, ApplicationEventType.DeliveryBillFileUploaded, new FileHolder
				{
					Data = model.DeliveryBillFile,
					Name = model.DeliveryBillFileName
				});
			}

			if (model.Torg12File != null && model.Torg12File.Length != 0)
			{
				_events.Add(applicationId, ApplicationEventType.Torg12FileUploaded, new FileHolder
				{
					Data = model.Torg12File,
					Name = model.Torg12FileName
				});
			}
		}
	}
}