using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Contracts.Calculation;

namespace Alicargo.Areas.Admin.Models
{
	public sealed class BillSettingsModel
	{
		public BillSettings Settings { get; set; }

		[Timestamp]
		public byte[] Version { get; set; }
	}
}