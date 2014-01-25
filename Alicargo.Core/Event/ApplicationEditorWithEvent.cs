using System;
using Alicargo.Core.Contracts.Event;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Repositories.Application;

namespace Alicargo.Core.Event
{
	public sealed class ApplicationEditorWithEvent : IApplicationEditor
	{
		private readonly IEventFacade _events;
		private readonly IApplicationEditor _editor;

		public ApplicationEditorWithEvent(IEventFacade events, IApplicationEditor editor)
		{
			_events = events;
			_editor = editor;
		}

		public void Update(ApplicationData application)
		{
			_editor.Update(application);
		}

		public long Add(ApplicationData application)
		{
			return _editor.Add(application);
		}

		public void Delete(long id)
		{
			_editor.Delete(id);
		}

		public void SetAirWaybill(long applicationId, long? airWaybillId)
		{
			_editor.SetAirWaybill(applicationId, airWaybillId);
		}

		public void SetState(long id, long stateId)
		{
			_editor.SetState(id, stateId);
		}

		public void SetDateInStock(long applicationId, DateTimeOffset dateTimeOffset)
		{
			_editor.SetDateInStock(applicationId, dateTimeOffset);
		}

		public void SetTransitReference(long id, string transitReference)
		{
			_editor.SetTransitReference(id, transitReference);
		}

		public void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt)
		{
			_editor.SetDateOfCargoReceipt(id, dateOfCargoReceipt);
		}

		public void SetTransitCost(long id, decimal? transitCost)
		{
			_editor.SetTransitCost(id, transitCost);
		}

		public void SetTransitCostEdited(long id, decimal? transitCost)
		{
			_editor.SetTransitCostEdited(id, transitCost);
		}

		public void SetTariffPerKg(long id, decimal? tariffPerKg)
		{
			_editor.SetTariffPerKg(id, tariffPerKg);
		}

		public void SetPickupCostEdited(long id, decimal? pickupCost)
		{
			_editor.SetPickupCostEdited(id, pickupCost);
		}

		public void SetFactureCostEdited(long id, decimal? factureCost)
		{
			_editor.SetFactureCostEdited(id, factureCost);
		}

		public void SetScotchCostEdited(long id, decimal? scotchCost)
		{
			_editor.SetScotchCostEdited(id, scotchCost);
		}

		public void SetSenderRate(long id, decimal? senderRate)
		{
			_editor.SetSenderRate(id, senderRate);
		}

		public void SetClass(long id, int? classId)
		{
			_editor.SetClass(id, classId);
		}
	}
}
