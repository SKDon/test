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

				Delete(data);
				data = GetNext();
			}
		}

		private void Delete(ApplicationEventData data)
		{
			try
			{
				_events.Delete(data.Id, data.RowVersion);
			}
			catch (EntityNotFoundException) { }
		}

		private void ProcessEvent(ApplicationEventData data)
		{
		}

		public ApplicationEventData GetNext()
		{
			var data = _events.GetNext();

			try
			{
				data.RowVersion = _events.Touch(data.Id, data.RowVersion);

				return data;
			}
			catch (EntityUpdateConflict)
			{
				return null;
			}
		}
	}
}
