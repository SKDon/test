namespace Alicargo.Contracts.Contracts
{
	public sealed class EmailTemplateLocalizationData
	{
		public string TwoLetterISOLanguageName { get; set; }

		public string Subject { get; set; }
	
		public string Body { get; set; }

		public bool IsBodyHtml { get; set; }		
	}
}