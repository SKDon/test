using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Jobs.Core;
using Alicargo.Jobs.Helpers;

namespace Alicargo.Jobs.Balance
{
	public sealed class BalanceEmailCreatorProcessor : IEventProcessor
	{
		private readonly ITemplateRepositoryWrapper _templates;

		public BalanceEmailCreatorProcessor(ITemplateRepositoryWrapper templates)
		{
			_templates = templates;
		}

		public void ProcessEvent(EventType type, EventData data)
		{
			var templateId = _templates.GetTemplateId(type);

			if (!templateId.HasValue)
			{
				return;
			}

			//_templates.GetLocalization(templateId.Value,)
		}
	}
}