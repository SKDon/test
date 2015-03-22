using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.ViewModels.Files
{
	public sealed class ApplicationFilesEdit
	{
		public FileHolder[] Invoices { get; set; }
		public FileHolder[] Swifts { get; set; }
		public FileHolder[] CPs { get; set; }
		public FileHolder[] DeliveryBills { get; set; }
		public FileHolder[] Torg12 { get; set; }
		public FileHolder[] Packings { get; set; }
	}
}