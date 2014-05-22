using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;

namespace Alicargo.Areas.Admin.Models.State
{
	public sealed class StateCreateModel
	{
		[Required, DisplayNameLocalized(typeof(Entities), "StateName")]
		public string Name { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Position")]
		public int Position { get; set; }
	}
}