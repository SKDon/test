using System;
using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Localization;
using Alicargo.Core.Resources;

namespace Alicargo.ViewModels.Calculation.Admin
{
	public sealed class PaymentModel
	{
		[Required]
		[DisplayNameLocalized(typeof(Entities), "Money")]
		public float Money { get; set; }

		[DataType(DataType.MultilineText)]
		[DisplayNameLocalized(typeof(Entities), "Comment")]
		public string Comment { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		[DisplayNameLocalized(typeof(Entities), "Timestamp")]
		public DateTimeOffset Timestamp { get; set; }
	}
}