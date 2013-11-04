using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class ApplicationMailCreatorJob : IJob
	{
		private readonly IApplicationEventRepository _events;

		public ApplicationMailCreatorJob(IApplicationEventRepository events)
		{
			_events = events;
		}

		public void Run()
		{
			var data = GetNext();

			while (data != null)
			{
				ProcessEvent(data);

				_events.Delete(data.Id, data.RowVersion);
				data = GetNext();
			}
		}

		private void ProcessEvent(ApplicationEventData data)
		{
		}

		public ApplicationEventData GetNext()
		{
			var data = _events.GetNext();

			try
			{
				if (data != null)
				{
					data.RowVersion = _events.Touch(data.Id, data.RowVersion);
				}

				return data;
			}
			catch (EntityUpdateConflict)
			{
				return null;
			}
		}
	}
}
