using Alicargo.Utilities.Localization;

namespace Alicargo.DataAccess.Contracts.Enums
{
	public enum EventType
	{
		// Application events
		ApplicationCreated = 1,
		ApplicationSetState = 2,
		SetDateOfCargoReceipt = 9,
		SetTransitReference = 10,

		// Application's file events
		CPFileUploaded = 3,
		InvoiceFileUploaded = 4,
		PackingFileUploaded = 5,
		SwiftFileUploaded = 6,
		DeliveryBillFileUploaded = 7,
		Torg12FileUploaded = 8,

		// Application's calculation events
		Calculate = 11,
		CalculationCanceled = 12,


		// Client balance events
		[DisplayNameLocalized(typeof(Resources.EventType), "BalanceIncreased")]
		BalanceIncreased = 13,

		[DisplayNameLocalized(typeof(Resources.EventType), "BalanceDecreased")]
		BalanceDecreased = 14
	}
}