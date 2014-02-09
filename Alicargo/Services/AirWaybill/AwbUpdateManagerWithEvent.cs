using Alicargo.Core.Contracts.Event;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.AirWaybill
{
	internal sealed class AwbUpdateManagerWithEvent : IAwbUpdateManager
	{
		private readonly IEventFacade _events;
		private readonly IAwbUpdateManager _manager;

		public AwbUpdateManagerWithEvent(
			IEventFacade events,
			IAwbUpdateManager manager)
		{
			_events = events;
			_manager = manager;
		}

		public void Update(long id, AwbAdminModel model)
		{
			_manager.Update(id, model);

			AddEvent(id, EventType.AWBFileUploaded, model.AWBFileName, model.AWBFile);
			AddEvent(id, EventType.GTDFileUploaded, model.GTDFileName, model.GTDFile);
			AddEvent(id, EventType.GTDAdditionalFileUploaded, model.GTDAdditionalFileName, model.GTDAdditionalFile);
			AddEvent(id, EventType.AwbPackingFileUploaded, model.PackingFileName, model.PackingFile);
			AddEvent(id, EventType.AwbInvoiceFileUploaded, model.InvoiceFileName, model.InvoiceFile);
			AddEvent(id, EventType.DrawFileUploaded, model.DrawFileName, model.DrawFile);
		}

		public void Update(long id, AwbBrokerModel model)
		{
			_manager.Update(id, model);

			AddEvent(id, EventType.GTDFileUploaded, model.GTDFileName, model.GTDFile);
			AddEvent(id, EventType.GTDAdditionalFileUploaded, model.GTDAdditionalFileName, model.GTDAdditionalFile);
			AddEvent(id, EventType.AwbPackingFileUploaded, model.PackingFileName, model.PackingFile);
			AddEvent(id, EventType.AwbInvoiceFileUploaded, model.InvoiceFileName, model.InvoiceFile);
			AddEvent(id, EventType.DrawFileUploaded, model.DrawFileName, model.DrawFile);
		}

		public void Update(long id, AwbSenderModel model)
		{
			_manager.Update(id, model);

			AddEvent(id, EventType.AWBFileUploaded, model.AWBFileName, model.AWBFile);
			AddEvent(id, EventType.AwbPackingFileUploaded, model.PackingFileName, model.PackingFile);
		}

		public void SetAdditionalCost(long awbId, decimal? additionalCost)
		{
			_manager.SetAdditionalCost(awbId, additionalCost);
		}

		private void AddEvent(long id, EventType eventType, string fileName, byte[] file)
		{
			if(file != null && file.Length != 0 && !string.IsNullOrEmpty(fileName))
			{
				_events.Add(id, eventType, EventState.Emailing, new FileHolder
				{
					Data = file,
					Name = fileName
				});
			}
		}
	}
}