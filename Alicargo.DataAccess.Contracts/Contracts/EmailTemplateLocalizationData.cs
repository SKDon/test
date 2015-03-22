namespace Alicargo.DataAccess.Contracts.Contracts
{
	public sealed class EmailTemplateLocalizationData
	{
		public string Subject { get; set; }
	
		public string Body { get; set; }

		public bool IsBodyHtml { get; set; }		
	}
}