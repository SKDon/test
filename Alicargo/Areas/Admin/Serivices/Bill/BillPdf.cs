using Alicargo.Areas.Admin.Serivices.Abstract;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Repositories.Application;

namespace Alicargo.Areas.Admin.Serivices.Bill
{
	internal sealed class BillPdf : IBillPdf
	{
		private readonly IBillRepository _bills;

		public BillPdf(IBillRepository bills)
		{
			_bills = bills;
		}

		public FileHolder Get(long applicationId)
		{
			var bill = _bills.Get(applicationId);

			return new FileHolder
			{
				Name = "bill-" + bill.Number + ".pdf"
			};
		}
	}
}