using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class ApplicationStateHistoryJob : IJob
	{
		private readonly IEventRepository _events;
		private readonly ShardSettings _shard;

		public ApplicationStateHistoryJob(
			IEventRepository events,
			ShardSettings shard)
		{
			_events = events;
			_shard = shard;
		}

		public void Run()
		{
			EventJobHelper.Run(_events, _shard, ProcessEvent, EventState.StateHistorySaving);
		}

		private void ProcessEvent(EventData data)
		{
			_events.Delete(data.Id);
		}
	}
}
