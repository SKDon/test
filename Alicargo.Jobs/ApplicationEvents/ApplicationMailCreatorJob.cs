using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class ApplicationMailCreatorJob : IJob
	{
		private readonly IApplicationEventRepository _events;
		private readonly TimeSpan _periodToProcessEvent;

		public ApplicationMailCreatorJob(IApplicationEventRepository events, TimeSpan periodToProcessEvent)
		{
			_events = events;
			_periodToProcessEvent = periodToProcessEvent;
		}

		public void Run()
		{
			var data = _events.GetNext(DateTimeOffset.UtcNow.Subtract(_periodToProcessEvent));

			while (data != null)
			{
				ProcessEvent(data);

				_events.Delete(data.Id, data.RowVersion);
				data = _events.GetNext(DateTimeOffset.UtcNow.Subtract(_periodToProcessEvent));
			}
		}

		private void ProcessEvent(ApplicationEventData data)
		{
		}

		public ApplicationEventData GetNext()
		{
			var data = _events.GetNext(DateTimeOffset.UtcNow.Subtract(_periodToProcessEvent));

			//try
			//{
			//	if (data != null)
			//	{
			//		data.RowVersion = _events.Touch(data.Id, data.RowVersion);
			//	}

			//	return data;
			//}
			//catch (EntityUpdateConflict)
			//{
			//	return null;
			//}
		}
	}
}
