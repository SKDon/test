using System.ComponentModel.DataAnnotations;
using Alicargo.Contracts.Resources;
using Alicargo.Utilities.Localization;

namespace Alicargo.ViewModels.State
{
	public sealed class StateCreateModel
	{
		[Required, DisplayNameLocalized(typeof(Entities), "StateName")]
		public string Name { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Position")]
		public int Position { get; set; }
	}
}