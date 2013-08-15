using Alicargo.Contracts.Contracts;

namespace Alicargo.DataAccess.Helpers
{
	internal static class ApplicationDataHelper
	{
		// todo: test
		public static void CopyTo(this ApplicationData from, byte[] swiftFile, byte[] invoiceFile, 
			byte[] cpFile, byte[] deliveryBillFile, byte[] torg12File, byte[] packingFile, DbContext.Application to)
		{
			if (to.Id == 0)
			{
				to.Id = from.Id;
				to.CreationTimestamp = from.CreationTimestamp;
				to.StateChangeTimestamp = from.StateChangeTimestamp;
				to.StateId = from.StateId;
			}

			to.Characteristic = from.Characteristic;
			to.AddressLoad = from.AddressLoad;
			to.WarehouseWorkingTime = from.WarehouseWorkingTime;
			to.Weight = from.Weigth;
			to.Count = from.Count;
			to.Volume = from.Volume;
			to.TermsOfDelivery = from.TermsOfDelivery;
			to.Value = from.Value;
			to.CurrencyId = from.CurrencyId;
			to.MethodOfDeliveryId = from.MethodOfDeliveryId;
			to.DateInStock = from.DateInStock;
			to.DateOfCargoReceipt = from.DateOfCargoReceipt;
			to.TransitReference = from.TransitReference;

			to.ClientId = from.ClientId;
			to.TransitId = from.TransitId;
			to.AirWaybillId = from.AirWaybillId;
			to.CountryId = from.CountryId;

			to.FactoryName = from.FactoryName;
			to.FactoryPhone = from.FactoryPhone;
			to.FactoryEmail = from.FactoryEmail;
			to.FactoryContact = from.FactoryContact;
			to.MarkName = from.MarkName;

			to.Invoice = from.Invoice;

			// todo: separate repository for files
			FileDataHelper.SetFile(invoiceFile, from.InvoiceFileName,
				bytes => to.InvoiceFileData = bytes, s => to.InvoiceFileName = s);

			FileDataHelper.SetFile(packingFile, from.PackingFileName,
				bytes => to.PackingFileData = bytes, s => to.PackingFileName = s);

			FileDataHelper.SetFile(cpFile, from.CPFileName,
				bytes => to.CPFileData = bytes, s => to.CPFileName = s);

			FileDataHelper.SetFile(deliveryBillFile, from.DeliveryBillFileName,
				bytes => to.DeliveryBillFileData = bytes, s => to.DeliveryBillFileName = s);

			FileDataHelper.SetFile(torg12File, from.Torg12FileName,
				bytes => to.Torg12FileData = bytes, s => to.Torg12FileName = s);

			FileDataHelper.SetFile(swiftFile, from.SwiftFileName,
				bytes => to.SwiftFileData = bytes, s => to.SwiftFileName = s);
		}
	}
}