using Alicargo.ViewModels.EmailTemplate;

namespace Alicargo.Areas.Admin.Models.State
{
	public sealed class StateSettingsModel
	{
		public EmailTemplateSettingsModel Availabilities { get; set; }
		public EmailTemplateSettingsModel Visibilities { get; set; }		
	}
}