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
			var data = _events.GetNext();

			while (data != null)
			{
				data = _events.GetNext();
			}
		}
	}
}
