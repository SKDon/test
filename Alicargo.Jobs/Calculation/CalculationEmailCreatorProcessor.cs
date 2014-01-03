using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Jobs.Core;
using Alicargo.Jobs.Helpers;

namespace Alicargo.Jobs.Calculation
{
	public sealed class CalculationEmailCreatorProcessor : IEventProcessor
	{
		private readonly ITemplateRepositoryWrapper _templates;

		public CalculationEmailCreatorProcessor(ITemplateRepositoryWrapper templates)
		{
			_templates = templates;
		}

		public void ProcessEvent(EventType type, EventData data)
		{
			var template = _templates.GetByEventType(type);
		}
	}
}
