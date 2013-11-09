using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class ApplicationMailCreatorJob : IJob
	{
		private readonly IEmailMessageRepository _emailMessages;
		private readonly IMessageFactory _messageFactory;
		private readonly IApplicationEventRepository _events;
		private readonly ShardSettings _shard;

		public ApplicationMailCreatorJob(IEmailMessageRepository emailMessages, IMessageFactory messageFactory, IApplicationEventRepository events, ShardSettings shard)
		{
			_emailMessages = emailMessages;
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

			_emailMessages.Add(message);

			_events.SetState(data.Id, ApplicationEventState.EmailPrepared);
		}
	}
}
