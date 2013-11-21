namespace Alicargo.Contracts.Contracts
{
	public sealed class StateEmailTemplateData
	{
		public EmailTemplateLocalizationData[] Localizations { get; set; }

		public bool EnableEmailSend { get; set; }
	}
}