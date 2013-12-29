namespace Alicargo.Contracts.Enums
{
	public enum EventType
	{
		ApplicationCreated = 1,
		ApplicationSetState = 2,
		
		CPFileUploaded = 3,
		InvoiceFileUploaded = 4,
		PackingFileUploaded = 5,
		SwiftFileUploaded = 6,
		DeliveryBillFileUploaded = 7,
		Torg12FileUploaded = 8,
		
		SetDateOfCargoReceipt = 9,
		SetTransitReference = 10,

		//CalculationFinished = 11,
		//CalculationCanceled = 12,

		//BalanceAdded = 13,
		//BalanceRemoved = 14
	}
}