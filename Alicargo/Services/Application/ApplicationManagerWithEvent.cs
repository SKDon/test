using System;
using System.Runtime.InteropServices;
using Alicargo.Core.Enums;
using Alicargo.Core.Services.Abstract;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class ApplicationManagerWithEvent : IApplicationManager
	{
		private readonly IApplicationManager _manager;
		private readonly IApplicationEvent _events;

		public ApplicationManagerWithEvent(IApplicationManager manager, IApplicationEvent events)
		{
			_manager = manager;
			_events = events;
		}

		public void Update(long applicationId, ApplicationAdminModel model, CarrierSelectModel carrierModel,
			TransitEditModel transitModel)
		{
			_manager.Update(applicationId, model, carrierModel, transitModel);

			if (model.CPFile != null && model.CPFile.Length != 0)
			{
				_events.Add(applicationId, ApplicationEventType.CPFileUploaded);
			}

			if (model.InvoiceFile != null && model.InvoiceFile.Length != 0)
			{
				_events.Add(applicationId, ApplicationEventType.InvoiceFileUploaded);
			}

			if (model.PackingFile != null && model.PackingFile.Length != 0)
			{
				_events.Add(applicationId, ApplicationEventType.PackingFileUploaded);
			}

			if (model.SwiftFile != null && model.SwiftFile.Length != 0)
			{
				_events.Add(applicationId, ApplicationEventType.SwiftFileUploaded);
			}

			if (model.DeliveryBillFile != null && model.DeliveryBillFile.Length != 0)
			{
				_events.Add(applicationId, ApplicationEventType.DeliveryBillFileUploaded);
			}

			if (model.Torg12File != null && model.Torg12File.Length != 0)
			{
				_events.Add(applicationId, ApplicationEventType.Torg12FileUploaded);
			}
		}

		public long Add(ApplicationAdminModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel, long clientId)
		{
			return _manager.Add(model, carrierModel, transitModel, clientId);
		}

		public void Delete(long id)
		{
			_manager.Delete(id);
		}

		public void SetState(long applicationId, long stateId)
		{
			_manager.SetState(applicationId, stateId);
		}

		public void SetTransitReference(long id, string transitReference)
		{
			_manager.SetTransitReference(id, transitReference);
		}

		public void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt)
		{
			_manager.SetDateOfCargoReceipt(id, dateOfCargoReceipt);
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
	}
}