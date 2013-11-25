using Alicargo.ViewModels.EmailTemplate;

namespace Alicargo.ViewModels.State
{
	public sealed class StateSettingsModel
	{
		public EmailTemplateSettingsModel Availabilities { get; set; }
		public EmailTemplateSettingsModel Recipients { get; set; }
		public EmailTemplateSettingsModel Visibilities { get; set; }		
	}
}