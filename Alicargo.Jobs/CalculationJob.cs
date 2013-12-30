using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs
{
	public sealed class CalculationJob : IJob
	{
		private readonly IEventRepository _events;
		private readonly int _partitionId;

		public CalculationJob(IEventRepository events, int partitionId)
		{
			_events = events;
			_partitionId = partitionId;
		}

		public void Run()
		{
			EventJobHelper.Run(_events, _partitionId, ProcessEvent, EventState.Calculating);
		}

		private void ProcessEvent(EventData obj)
		{
			
		}
	}
}