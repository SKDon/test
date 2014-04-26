namespace Alicargo.Areas.Admin.Models
{
	public sealed class BillModel
	{
		public BankDetails BankDetails { get; set; }

		public string Client { get; set; }
		public string Goods { get; set; }
		public string Count { get; set; }
		public string Price { get; set; }
		public string Total { get; set; }
		public string Accountant { get; set; }
		public string Head { get; set; }
		public string HeaderText { get; set; }
		public string Shipper { get; set; }
	}
}