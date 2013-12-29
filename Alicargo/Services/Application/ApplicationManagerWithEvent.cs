using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Core.Helpers;
using Alicargo.Jobs.ApplicationEvents.Entities;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class ApplicationManagerWithEvent : IApplicationManager
	{
		private readonly IPartitionConverter _converter;
		private readonly IEventRepository _events;
		private readonly IApplicationManager _manager;
		private readonly ISerializer _serializer;

		public ApplicationManagerWithEvent(IApplicationManager manager, IEventRepository events,
			ISerializer serializer, IPartitionConverter converter)
		{
			_manager = manager;
			_events = events;
			_serializer = serializer;
			_converter = converter;
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

			_events.Add(
				_converter.GetKey(applicationId),
				EventType.ApplicationCreated, EventState.ApplicationEmailing,
				_serializer.Serialize(new EventDataForApplication
				{
					ApplicationId = applicationId
				}));

			return applicationId;
		}

		public void Delete(long id)
		{
			_manager.Delete(id);
		}

		public void SetState(long applicationId, long stateId)
		{
			_manager.SetState(applicationId, stateId);

			var data = _serializer.Serialize(new ApplicationSetStateEventData
			{
				StateId = stateId,
				Timestamp = DateTimeOffset.UtcNow
			});

			_events.Add(
				_converter.GetKey(applicationId),
				EventType.ApplicationSetState, EventState.ApplicationEmailing,
				_serializer.Serialize(new EventDataForApplication
				{
					ApplicationId = applicationId,
					Data = data
				}));
		}

		public void SetTransitReference(long applicationId, string transitReference)
		{
			_manager.SetTransitReference(applicationId, transitReference);

			var data = _serializer.Serialize(transitReference);

			_events.Add(
				_converter.GetKey(applicationId),
				EventType.SetTransitReference, EventState.ApplicationEmailing,
				_serializer.Serialize(new EventDataForApplication
				{
					ApplicationId = applicationId,
					Data = data
				}));
		}

		public void SetDateOfCargoReceipt(long applicationId, DateTimeOffset? dateOfCargoReceipt)
		{
			_manager.SetDateOfCargoReceipt(applicationId, dateOfCargoReceipt);

			var data = _serializer.Serialize(dateOfCargoReceipt);

			_events.Add(
				_converter.GetKey(applicationId),
				EventType.SetDateOfCargoReceipt, EventState.ApplicationEmailing,
				_serializer.Serialize(new EventDataForApplication
				{
					ApplicationId = applicationId,
					Data = data
				}));
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