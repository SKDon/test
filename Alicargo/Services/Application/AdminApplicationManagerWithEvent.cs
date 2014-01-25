using System;
using Alicargo.Core.Contracts.Event;
using Alicargo.Core.Event;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Jobs.ApplicationEvents.Entities;
using Alicargo.Services.Abstract;
using Alicargo.Utilities;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class AdminApplicationManagerWithEvent : IAdminApplicationManager
	{
		private readonly IEventFacade _events;
		private readonly IAdminApplicationManager _manager;

		public AdminApplicationManagerWithEvent(IAdminApplicationManager manager, IEventFacade events)
		{
			_manager = manager;
			_events = events;
		}

		public void Update(long applicationId, ApplicationAdminModel model, CarrierSelectModel carrierModel,
			TransitEditModel transitModel)
		{
			_manager.Update(applicationId, model, carrierModel, transitModel);
		}

		public long Add(ApplicationAdminModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel,
			long clientId)
		{
			var applicationId = _manager.Add(model, carrierModel, transitModel, clientId);

			_events.Add(applicationId, EventType.ApplicationCreated, EventState.Emailing);

			return applicationId;
		}

		public void Delete(long id)
		{
			_manager.Delete(id);
		}

		public void SetState(long applicationId, long stateId)
		{
			_manager.SetState(applicationId, stateId);

			_events.Add(applicationId,
				EventType.ApplicationSetState, EventState.Emailing,
				new ApplicationSetStateEventData
				{
					StateId = stateId,
					Timestamp = DateTimeProvider.Now
				});
		}

		public void SetTransitReference(long applicationId, string transitReference)
		{
			_manager.SetTransitReference(applicationId, transitReference);

			_events.Add(applicationId, EventType.SetTransitReference, EventState.Emailing, transitReference);
		}

		public void SetDateOfCargoReceipt(long applicationId, DateTimeOffset? dateOfCargoReceipt)
		{
			_manager.SetDateOfCargoReceipt(applicationId, dateOfCargoReceipt);

			_events.Add(applicationId, EventType.SetDateOfCargoReceipt, EventState.Emailing, dateOfCargoReceipt);
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