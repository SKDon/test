using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services;

namespace Alicargo.Jobs.Core
{
	using TProcessorCollection = IDictionary<EventType, IDictionary<EventState, IEventProcessor>>;

	public sealed class SequentialEventJob : IJob
	{
		private readonly IEventRepository _events;
		private readonly int _partitionId;
		private readonly TProcessorCollection _processors;

		public SequentialEventJob(IEventRepository events, int partitionId, TProcessorCollection processors)
		{
			_events = events;
			_partitionId = partitionId;
			_processors = processors;
		}

		public void Work()
		{
			foreach (var typeProcessors in _processors)
			{
				var type = typeProcessors.Key;

				var data = _events.GetNext(type, _partitionId);

				if (data == null) continue;

				var stateProcessors = typeProcessors.Value
					.SkipWhile(x => x.Key != data.State)
					.ToArray();

				for (var i = 0; i < stateProcessors.Length; i++)
				{
					var processor = stateProcessors[i].Value;
					try
					{
						processor.ProcessEvent(type, data);

						if (i + 1 != stateProcessors.Length)
						{
							var nextState = stateProcessors[i + 1].Key;

							_events.SetState(data.Id, nextState);
						}
						else
						{
							_events.Delete(data.Id);

							break;
						}
					}
					catch (BreakJobException)
					{
						break;
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