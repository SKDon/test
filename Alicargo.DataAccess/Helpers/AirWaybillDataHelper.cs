using Alicargo.Contracts.Contracts;

namespace Alicargo.DataAccess.Helpers
{
	// todo: test
	internal static class AirWaybillDataHelper
	{
		public static void CopyTo(this AirWaybillData @from, DbContext.AirWaybill to,
			byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile, byte[] invoiceFile, byte[] awbFile)
		{
			if (to.Id == 0)
			{
				to.Id = @from.Id;
				to.CreationTimestamp = @from.CreationTimestamp;
				to.StateId = @from.StateId;
				to.StateChangeTimestamp = @from.StateChangeTimestamp;
			}

			to.ArrivalAirport = @from.ArrivalAirport;
			to.Bill = @from.Bill;
			to.BrockerId = @from.BrockerId;
			to.DateOfArrival = @from.DateOfArrival;
			to.DateOfDeparture = @from.DateOfDeparture;
			to.DepartureAirport = @from.DepartureAirport;
			to.GTD = @from.GTD;
			
			FileDataHelper.SetFile(gtdFile, from.GTDFileName,
				bytes => to.GTDFileData = bytes, s => to.GTDFileName = s);

			FileDataHelper.SetFile(gtdAdditionalFile, from.GTDAdditionalFileName,
				bytes => to.GTDAdditionalFileData = bytes, s => to.GTDAdditionalFileName = s);

			FileDataHelper.SetFile(packingFile, from.PackingFileName,
				bytes => to.PackingFileData = bytes, s => to.PackingFileName = s);

			FileDataHelper.SetFile(invoiceFile, from.InvoiceFileName,
				bytes => to.InvoiceFileData = bytes, s => to.InvoiceFileName = s);

			FileDataHelper.SetFile(awbFile, from.AWBFileName,
				bytes => to.AWBFileData = bytes, s => to.AWBFileName = s);
		}
	}
}
