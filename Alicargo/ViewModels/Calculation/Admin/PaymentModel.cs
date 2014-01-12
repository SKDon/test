using System;
using System.ComponentModel.DataAnnotations;
using Alicargo.Contracts.Resources;
using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;

namespace Alicargo.ViewModels.Calculation.Admin
{
	public sealed class PaymentModel
	{
		[Required]
		[DataType(DataType.Currency)]
		[DisplayNameLocalized(typeof(Entities), "Money")]
		public decimal? Money { get; set; }

		[DataType(DataType.MultilineText)]
		[DisplayNameLocalized(typeof(Entities), "Comment")]
		public string Comment { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		[DisplayNameLocalized(typeof(Entities), "Timestamp")]
		public DateTimeOffset Timestamp { get; set; }
	}
}