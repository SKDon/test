using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Alicargo.Contracts.Resources;
using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;

namespace Alicargo.ViewModels.State
{
	public sealed class StateEditModel
	{
		[HiddenInput]
		public long Id { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "StateName")]
		public string Name { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Position")]
		public int Position { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Subject")]
		public string Subject { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Body")]
		[DataType(DataType.MultilineText)]
		public string Body { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "LocalizedName")]
		public string LocalizedName { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Language")]
		public string Language { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "EnableEmailSend")]
		public bool EnableEmailSend { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "UseEventTemplate")]
		public bool UseEventTemplate { get; set; }
	}
}