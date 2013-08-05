using System;
using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Contracts;
using Alicargo.Core.Localization;
using Alicargo.ViewModels.Application;
using Resources;

namespace Alicargo.ViewModels
{
	public sealed class ReferenceModel : ReferenceData
	{
		public ReferenceModel() { }

		public ReferenceModel(IReferenceData data) : base(data) { }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "DateOfDeparture")]
		public string DateOfDepartureLocalString
		{
			get { return DateOfDeparture.LocalDateTime.ToShortDateString(); }
			set
			{
				// todo: test
				DateOfDeparture = DateTimeOffset.Parse(value);
			}
		}

		[Required]
		[DisplayNameLocalized(typeof(Entities), "DateOfArrival")]
		public string DateOfArrivalLocalString
		{
			get { return DateOfArrival.LocalDateTime.ToShortDateString(); }
			set
			{
				// todo: test
				DateOfArrival = DateTimeOffset.Parse(value);
			}
		}

		public string CreationTimestampLocalString
		{
			get { return CreationTimestamp.LocalDateTime.ToShortDateString(); }
		}

		public string StateChangeTimestampLocalString { get { return StateChangeTimestamp.LocalDateTime.ToShortDateString(); } }

		[DisplayNameLocalized(typeof(Entities), "GTD")]
		public byte[] GTDFile { get; set; }

		[DisplayNameLocalized(typeof(Entities), "GTDAdditional")]
		public byte[] GTDAdditionalFile { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Packing")]
		public byte[] PackingFile { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Invoice")]
		public byte[] InvoiceFile { get; set; }

		[DisplayNameLocalized(typeof(Entities), "AWB")]
		public byte[] AWBFile { get; set; }

		[DisplayNameLocalized(typeof(Entities), "TotalCount")]
		public int? TotalCount { get; set; }

		[DisplayNameLocalized(typeof(Entities), "TotalWeight")]
		public float? TotalWeight { get; set; }

		public ApplicationStateModel State { get; set; }
	}
}