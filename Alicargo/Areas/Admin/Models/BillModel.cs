using Alicargo.Core.Contracts.Calculation;

namespace Alicargo.Areas.Admin.Models
{
	public sealed class BillModel
	{
		public BillSettings Settings { get; set; }
		public string Client { get; set; }
		public string Goods { get; set; }
		public string Count { get; set; }
		public string Price { get; set; }
		public string Total { get; set; }
	}
}