namespace Alicargo.Contracts.Enums
{
	public enum EventType
	{
		// Application events
		ApplicationCreated,
		ApplicationSetState,
		SetDateOfCargoReceipt,
		SetTransitReference,

		CPFileUploaded,
		InvoiceFileUploaded,
		PackingFileUploaded,
		SwiftFileUploaded,
		DeliveryBillFileUploaded,
		Torg12FileUploaded,


		// Calculation events
		Calculate,
		CalculationCanceled,


		// Balance events
		BalanceIncreased,
		BalanceDecreased
	}
}