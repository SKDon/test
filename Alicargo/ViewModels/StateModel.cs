using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Localization;
using Alicargo.Core.Resources;

namespace Alicargo.ViewModels
{
	public sealed class StateModel
	{
		[Required, DisplayNameLocalized(typeof (Entities), "Name")]
		public string Name { get; set; }

		[Required, DisplayNameLocalized(typeof (Entities), "Position")]
		public int Position { get; set; }

		[DisplayNameLocalized(typeof (Entities), "Subject")]
		public string Subject { get; set; }

		[DisplayNameLocalized(typeof (Entities), "Body")]
		[DataType(DataType.MultilineText)]
		public string Body { get; set; }

		[Required, DisplayNameLocalized(typeof (Entities), "LocalizedName")]
		public string LocalizedName { get; set; }

		[Required, DisplayNameLocalized(typeof (Entities), "Language")]
		public string Language { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "EnableEmailSend")]
		public bool EnableEmailSend { get; set; }
	}
}