using System.Collections.Generic;
using Alicargo.Utilities.Localization;
using Resources;

namespace Alicargo.ViewModels.Application
{
    public sealed class ApplicationIndexModel
	{
		public Dictionary<long, string> Clients { get; set; }

		[DisplayNameLocalized(typeof(Pages), "AirWaybillSelect")]
		public IDictionary<long, string> AirWaybills { get; set; }
	}
}