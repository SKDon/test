using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class ApplicationMailCreatorJob : IJob
	{
		private readonly IEmailMessageRepository _emails;
		private readonly IApplicationEventRepository _events;
		private readonly IMessageFactory _messageFactory;
		private readonly ISerializer _serializer;
		private readonly ShardSettings _shard;

		public ApplicationMailCreatorJob(IEmailMessageRepository emails, IMessageFactory messageFactory,
			IApplicationEventRepository events, ShardSettings shard, ISerializer serializer)
		{
			_emails = emails;
			_messageFactory = messageFactory;
			_events = events;
			_shard = shard;
			_serializer = serializer;
		}

		public void Run()
		{
			ApplicationEventJobHelper.Run(_events, _shard, ProcessEvent, ApplicationEventState.New);
		}

		private void ProcessEvent(ApplicationEventData data)
		{
			var messages = _messageFactory.Get(data.ApplicationId, data.EventType, data.Data);

			if (messages != null)
			{
				foreach (var message in messages)
				{
					var files = _serializer.Serialize(message.Files);

					_emails.Add(_shard.ZeroBasedIndex, message.From, message.To, message.CopyTo,
						message.Subject, message.Body, message.IsBodyHtml, files);
				}
			}

			_events.SetState(data.Id, ApplicationEventState.EmailPrepared);
		}
	}
}