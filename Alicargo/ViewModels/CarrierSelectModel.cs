using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Localization;
using Resources;

namespace Alicargo.ViewModels
{
	public sealed class CarrierSelectModel
	{
		[Required, DisplayNameLocalized(typeof(Entities), "CarrierName")]
		public long CarrierId { get; set; }

		[DisplayNameLocalized(typeof(Entities), "NewCarrierName")]
		public string NewCarrierName { get; set; }
	}
}