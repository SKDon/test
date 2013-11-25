namespace Alicargo.Contracts.Contracts
{
	public sealed class StateEmailTemplateData
	{
		public long EmailTemplateId { get; set; }
		public bool EnableEmailSend { get; set; }
		public bool UseApplicationEventTemplate { get; set; }
	}
}