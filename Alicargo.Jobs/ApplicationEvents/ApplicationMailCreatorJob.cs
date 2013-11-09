using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class ApplicationMailCreatorJob : IJob
	{
		private readonly IEmailMessageRepository _emails;
		private readonly IMessageFactory _messageFactory;
		private readonly IApplicationEventRepository _events;
		private readonly ShardSettings _shard;

		public ApplicationMailCreatorJob(IEmailMessageRepository emails, IMessageFactory messageFactory, IApplicationEventRepository events, ShardSettings shard)
		{
			_emails = emails;
			_messageFactory = messageFactory;
			_events = events;
			_shard = shard;
		}

		public void Run()
		{
			ApplicationEventJobHelper.Run(_events, _shard, ProcessEvent, ApplicationEventState.New);
		}

		private void ProcessEvent(ApplicationEventData data)
		{
			var message = _messageFactory.Get(data.EventType, data.Data);

			_emails.Add(message);

			_events.SetState(data.Id, ApplicationEventState.EmailPrepared);
		}
	}
}
