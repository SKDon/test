using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services;

namespace Alicargo.Jobs.Core
{
	using TProcessorCollection = IDictionary<EventType, IDictionary<EventState, IEventProcessor>>;

	public sealed class DefaultEventJob : IJob
	{
		private readonly IEventRepository _events;
		private readonly int _partitionId;
		private readonly TProcessorCollection _processors;

		public DefaultEventJob(IEventRepository events, int partitionId, TProcessorCollection processors)
		{
			_events = events;
			_partitionId = partitionId;
			_processors = processors;
		}

		public void Work()
		{
			foreach (var processorsForType in _processors)
			{
				var type = processorsForType.Key;

				var data = _events.GetNext(type, _partitionId);

				if (data == null) continue;

				var processorsForState = processorsForType.Value
					.SkipWhile(x => x.Key != data.State)
					.Select(x => x.Value);

				foreach (var processor in processorsForState)
				{
					try
					{
						processor.ProcessEvent(type, data);
					}
					catch (Exception e)
					{
						if (!e.IsCritical())
						{
							_events.SetState(data.Id, EventState.Failed);
						}

						throw new JobException("Failed to process the event " + data.Id + " at the state " + data.State, e);
					}
				}
			}
		}
	}
}