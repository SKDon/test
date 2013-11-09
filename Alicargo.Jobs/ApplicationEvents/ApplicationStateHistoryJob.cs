using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class ApplicationStateHistoryJob : IJob
	{
		private readonly IApplicationEventRepository _events;
		private readonly ShardSettings _shard;

		public ApplicationStateHistoryJob(
			IApplicationEventRepository events,
			ShardSettings shard)
		{
			_events = events;
			_shard = shard;
		}

		public void Run()
		{
			ApplicationEventJobHelper.Run(_events, _shard, ProcessEvent, ApplicationEventState.EmailPrepared);
		}

		private void ProcessEvent(ApplicationEventData data)
		{
			_events.Delete(data.Id);
		}
	}
}
