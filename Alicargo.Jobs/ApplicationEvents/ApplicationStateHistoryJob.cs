using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class ApplicationStateHistoryJob : IJob
	{
		private readonly IEventRepository _events;
		private readonly int _partitionId;

		public ApplicationStateHistoryJob(
			IEventRepository events,
			int partitionId)
		{
			_events = events;
			_partitionId = partitionId;
		}

		public void Run()
		{
			EventJobHelper.Run(_events, _partitionId, ProcessEvent, EventState.StateHistorySaving);
		}

		private void ProcessEvent(EventData data)
		{
			_events.Delete(data.Id);
		}
	}
}