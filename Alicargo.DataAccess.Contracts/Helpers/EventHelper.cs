using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Helpers
{
	public static class EventHelper
	{
		public static readonly EventType[] ApplicationEventTypes =
		{
			EventType.ApplicationCreated,
			EventType.ApplicationSetState,
			EventType.SetDateOfCargoReceipt,
			EventType.SetTransitReference,
			EventType.SetSender,
			EventType.SetForwarder,
			EventType.SetCarrier,
			EventType.SetAwb,
			EventType.CPFileUploaded,
			EventType.InvoiceFileUploaded,
			EventType.PackingFileUploaded,
			EventType.SwiftFileUploaded,
			EventType.DeliveryBillFileUploaded,
			EventType.Torg12FileUploaded,
			EventType.T1FileUploaded,
			EventType.Ex1FileUploaded,
			EventType.OtherApplFileUploaded,
			EventType.Calculate,
			EventType.CalculationCanceled
		};

		public static readonly EventType[] AwbEventTypes =
		{
			EventType.AwbCreated,
			EventType.SetBroker,
			EventType.GTDFileUploaded,
			EventType.OtherAwbFileUploaded,
			EventType.GTDAdditionalFileUploaded,
			EventType.AwbPackingFileUploaded,
			EventType.AwbInvoiceFileUploaded,
			EventType.AWBFileUploaded,
			EventType.DrawFileUploaded
		};

		public static readonly EventType[] ClientEventTypes =
		{
			EventType.BalanceIncreased,
			EventType.BalanceDecreased,
			EventType.ClientAdded
		};
	}
}