﻿using Alicargo.Utilities.Localization;

namespace Alicargo.DataAccess.Contracts.Enums
{
	public enum EventType
	{
		// Application events
		[DisplayNameLocalized(typeof(Resources.EventType), "ApplicationCreated")] ApplicationCreated = 1,

		[DisplayNameLocalized(typeof(Resources.EventType), "ApplicationSetState")] ApplicationSetState = 2,

		[DisplayNameLocalized(typeof(Resources.EventType), "SetDateOfCargoReceipt")] SetDateOfCargoReceipt = 9,

		[DisplayNameLocalized(typeof(Resources.EventType), "SetTransitReference")] SetTransitReference = 10,

		[DisplayNameLocalized(typeof(Resources.EventType), "SetSender")] SetSender = 15,

		[DisplayNameLocalized(typeof(Resources.EventType), "SetForwarder")] SetForwarder = 16,

		[DisplayNameLocalized(typeof(Resources.EventType), "SetCarrier")] SetCarrier = 17,

		[DisplayNameLocalized(typeof(Resources.EventType), "SetAwb")] SetAwb = 20,


		// Application's file events
		[DisplayNameLocalized(typeof(Resources.EventType), "CPFileUploaded")] CPFileUploaded = 3,

		[DisplayNameLocalized(typeof(Resources.EventType), "InvoiceFileUploaded")] InvoiceFileUploaded = 4,

		[DisplayNameLocalized(typeof(Resources.EventType), "PackingFileUploaded")] PackingFileUploaded = 5,

		[DisplayNameLocalized(typeof(Resources.EventType), "SwiftFileUploaded")] SwiftFileUploaded = 6,

		[DisplayNameLocalized(typeof(Resources.EventType), "DeliveryBillFileUploaded")] DeliveryBillFileUploaded = 7,

		[DisplayNameLocalized(typeof(Resources.EventType), "Torg12FileUploaded")] Torg12FileUploaded = 8,


		// Application's calculation events
		[DisplayNameLocalized(typeof(Resources.EventType), "Calculate")] Calculate = 11,

		[DisplayNameLocalized(typeof(Resources.EventType), "CalculationCanceled")] CalculationCanceled = 12,


		// Client events
		[DisplayNameLocalized(typeof(Resources.EventType), "BalanceIncreased")] BalanceIncreased = 13,

		[DisplayNameLocalized(typeof(Resources.EventType), "BalanceDecreased")] BalanceDecreased = 14,

		[DisplayNameLocalized(typeof(Resources.EventType), "ClientAdded")] ClientAdded = 18,


		// Awb events
		[DisplayNameLocalized(typeof(Resources.EventType), "AwbCreated")] AwbCreated = 19,

		[DisplayNameLocalized(typeof(Resources.EventType), "GTDFileUploaded")] GTDFileUploaded = 21,

		[DisplayNameLocalized(typeof(Resources.EventType), "GTDAdditionalFileUploaded")] GTDAdditionalFileUploaded = 22,

		[DisplayNameLocalized(typeof(Resources.EventType), "AwbPackingFileUploaded")] AwbPackingFileUploaded = 23,

		[DisplayNameLocalized(typeof(Resources.EventType), "AwbInvoiceFileUploaded")] AwbInvoiceFileUploaded = 24,

		[DisplayNameLocalized(typeof(Resources.EventType), "AWBFileUploaded")] AWBFileUploaded = 25,

		[DisplayNameLocalized(typeof(Resources.EventType), "DrawFileUploaded")] DrawFileUploaded = 26
	}
}