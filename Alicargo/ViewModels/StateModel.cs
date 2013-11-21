using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Localization;
using Alicargo.Core.Resources;

namespace Alicargo.ViewModels
{
	public sealed class StateModel
	{
		[Required, DisplayNameLocalized(typeof(Entities), "Name")]
		public string Name { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Position")]
		public int Position { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Subject")]
		public string Subject { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Body")]
		public string Body { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "LocalizedName")]
		public string LocalizedName { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Culture")]
		public string Culture { get; set; }
	}
}