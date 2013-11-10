using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
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
		private readonly ISerializer _serializer;

		public ApplicationManagerWithEvent(IApplicationManager manager, IApplicationEventRepository events,
			ISerializer serializer)
		{
			_manager = manager;
			_events = events;
			_serializer = serializer;
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

			var bytes = _serializer.Serialize(new ApplicationCreatedEventData
			{
				ClientId = clientId,
				FactoryName = model.FactoryName,
				MarkName = model.MarkName,
				Count = model.Count,
				CreationTimestamp = DateTimeOffset.UtcNow
			});

			_events.Add(applicationId, ApplicationEventType.Created, bytes);

			return applicationId;
		}

		public void Delete(long id)
		{
			_manager.Delete(id);
		}

		public void SetState(long applicationId, long stateId)
		{
			_manager.SetState(applicationId, stateId);

			_events.Add(applicationId, ApplicationEventType.SetState, _serializer.Serialize(new ApplicationSetStateEventData
			{
				StateId = stateId,
				Timestamp = DateTimeOffset.UtcNow
			}));
		}

		public void SetTransitReference(long id, string transitReference)
		{
			_manager.SetTransitReference(id, transitReference);

			_events.Add(id, ApplicationEventType.SetTransitReference, _serializer.Serialize(transitReference));
		}

		public void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt)
		{
			_manager.SetDateOfCargoReceipt(id, dateOfCargoReceipt);

			_events.Add(id, ApplicationEventType.SetDateOfCargoReceipt, _serializer.Serialize(dateOfCargoReceipt));
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
			AddFileUploadEvent(applicationId, model, ApplicationEventType.CPFileUploaded,
				model.CPFileName, model.CPFile);

			AddFileUploadEvent(applicationId, model, ApplicationEventType.InvoiceFileUploaded,
				model.InvoiceFileName, model.InvoiceFile);

			AddFileUploadEvent(applicationId, model, ApplicationEventType.PackingFileUploaded,
				model.PackingFileName, model.PackingFile);

			AddFileUploadEvent(applicationId, model, ApplicationEventType.SwiftFileUploaded,
				model.SwiftFileName, model.SwiftFile);

			AddFileUploadEvent(applicationId, model, ApplicationEventType.DeliveryBillFileUploaded,
				model.DeliveryBillFileName, model.DeliveryBillFile);

			AddFileUploadEvent(applicationId, model, ApplicationEventType.Torg12FileUploaded,
				model.Torg12FileName, model.Torg12File);
		}

		private void AddFileUploadEvent(long applicationId, ApplicationAdminModel model, ApplicationEventType type,
			string fileName, byte[] fileData)
		{
			if (fileData == null || fileData.Length == 0) return;

			_events.Add(applicationId, type, _serializer.Serialize(
				new ApplicationFileUploadedEventData
				{
					Count = model.Count,
					FactoryName = model.FactoryName,
					MarkName = model.MarkName,
					Invoice = model.Invoice,
					File = new FileHolder
					{
						Data = fileData,
						Name = fileName
					}
				}));
		}
	}
}