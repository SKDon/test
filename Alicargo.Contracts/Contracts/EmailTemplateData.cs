using System;

namespace Alicargo.Contracts.Contracts
{
	[Obsolete]
	public sealed class EmailTemplateData
	{
		public string TemplateName { get; set; }

		public EmailTemplateLocalizationData[] Localizations { get; set; }
	}
}