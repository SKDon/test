using System;
using Alicargo.Core.Contracts.Event;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Utilities;

namespace Alicargo.Core.Event
{
	public sealed class ApplicationEditorWithEvent : IApplicationEditor
	{
		private readonly IApplicationRepository _applications;
		private readonly IApplicationEditor _editor;
		private readonly IEventFacade _events;

		public ApplicationEditorWithEvent(
			IEventFacade events,
			IApplicationRepository applications,
			IApplicationEditor editor)
		{
			_events = events;
			_applications = applications;
			_editor = editor;
		}

		public void Update(long applicationId, ApplicationEditData application)
		{
			var oldData = _applications.Get(applicationId);

			_editor.Update(applicationId, application);

			if(oldData.SenderId != application.SenderId && application.SenderId.HasValue)
			{
				_events.Add(applicationId, EventType.SetSender, EventState.Emailing);
			}

			if(oldData.ForwarderId != application.ForwarderId)
			{
				_events.Add(applicationId, EventType.SetForwarder, EventState.Emailing);
			}
		}

		public void SetTotalTariffCost(long id, decimal? value)
		{
			_editor.SetTotalTariffCost(id, value);
		}

		public void SetProfit(long id, decimal? value)
		{
			_editor.SetProfit(id, value);
		}

		public long Add(ApplicationEditData application)
		{
			var applicationId = _editor.Add(application);

			_events.Add(applicationId, EventType.ApplicationCreated, EventState.Emailing);

			return applicationId;
		}

		public void Delete(long applicationId)
		{
			_editor.Delete(applicationId);
		}

		public void SetAirWaybill(long applicationId, long? airWaybillId)
		{
			_editor.SetAirWaybill(applicationId, airWaybillId);
		}

		public void SetState(long applicationId, long stateId)
		{
			_editor.SetState(applicationId, stateId);

			_events.Add(applicationId,
				EventType.ApplicationSetState, EventState.Emailing,
				new ApplicationSetStateEventData
				{
					StateId = stateId,
					Timestamp = DateTimeProvider.Now
				});
		}

		public void SetDateInStock(long applicationId, DateTimeOffset dateTimeOffset)
		{
			_editor.SetDateInStock(applicationId, dateTimeOffset);
		}

		public void SetTransitReference(long applicationId, string transitReference)
		{
			_editor.SetTransitReference(applicationId, transitReference);

			_events.Add(applicationId, EventType.SetTransitReference, EventState.Emailing, transitReference);
		}

		public void SetCount(long id, int? value)
		{
			_editor.SetCount(id, value);
		}

		public void SetWeight(long id, float? value)
		{
			_editor.SetWeight(id, value);
		}

		public void SetInsuranceRate(long id, float insuranceRate)
		{
			_editor.SetInsuranceRate(id, insuranceRate);
		}

		public void SetDateOfCargoReceipt(long applicationId, DateTimeOffset? dateOfCargoReceipt)
		{
			_editor.SetDateOfCargoReceipt(applicationId, dateOfCargoReceipt);

			_events.Add(applicationId, EventType.SetDateOfCargoReceipt, EventState.Emailing, dateOfCargoReceipt);
		}

		public void SetTransitCost(long applicationId, decimal? transitCost)
		{
			_editor.SetTransitCost(applicationId, transitCost);
		}

		public void SetTransitCostEdited(long applicationId, decimal? transitCost)
		{
			_editor.SetTransitCostEdited(applicationId, transitCost);
		}

		public void SetTariffPerKg(long applicationId, decimal? tariffPerKg)
		{
			_editor.SetTariffPerKg(applicationId, tariffPerKg);
		}

		public void SetFactureCostExEdited(long id, decimal? factureCostEx)
		{
			_editor.SetFactureCostExEdited(id, factureCostEx);
		}

		public void SetPickupCostEdited(long applicationId, decimal? pickupCost)
		{
			_editor.SetPickupCostEdited(applicationId, pickupCost);
		}

		public void SetFactureCostEdited(long applicationId, decimal? factureCost)
		{
			_editor.SetFactureCostEdited(applicationId, factureCost);
		}

		public void SetScotchCostEditedByTotalCost(long applicationId, decimal? totalScotchCost)
		{
			_editor.SetScotchCostEditedByTotalCost(applicationId, totalScotchCost);
		}

		public void SetSenderRate(long applicationId, decimal? senderRate)
		{
			_editor.SetSenderRate(applicationId, senderRate);
		}

		public void SetClass(long applicationId, int? classId)
		{
			_editor.SetClass(applicationId, classId);
		}
	}
}