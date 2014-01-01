namespace Alicargo.Contracts.Enums
{
	public enum EventType
	{
		// Application events
		ApplicationCreated = 1,
		ApplicationSetState = 2,
		SetDateOfCargoReceipt = 9,
		SetTransitReference = 10,

		CPFileUploaded = 3,
		InvoiceFileUploaded = 4,
		PackingFileUploaded = 5,
		SwiftFileUploaded = 6,
		DeliveryBillFileUploaded = 7,
		Torg12FileUploaded = 8,


		// Calculation events
		Calculate = 11,
		CalculationCanceled = 12,


		// Balance events
		BalanceIncreased = 13,
		BalanceDecreased = 14
	}
}