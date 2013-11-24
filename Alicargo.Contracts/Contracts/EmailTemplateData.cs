namespace Alicargo.Contracts.Contracts
{
	public sealed class EmailTemplateData
	{
		public EmailTemplateLocalizationData Localization { get; set; }

		public bool EnableEmailSend { get; set; }
	}
}