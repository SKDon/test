using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Localization;
using Resources;

namespace Alicargo.ViewModels
{
	public sealed class CarrierSelectModel
	{
		[Required]
		[DisplayNameLocalized(typeof(Entities), "CarrierName")]
		public string CarrierName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "NewCarrierName")]
		public string NewCarrierName { get; set; }

		public Dictionary<string, string> Carriers { get; set; }
	}
}