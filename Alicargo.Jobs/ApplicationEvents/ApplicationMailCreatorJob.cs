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
		private readonly IEventRepository _events;
		private readonly int _partitionId;
		private readonly IMessageFactory _messageFactory;
		private readonly ISerializer _serializer;

		public ApplicationMailCreatorJob(IEmailMessageRepository emails, IMessageFactory messageFactory,
			IEventRepository events, int partitionId, ISerializer serializer)
		{
			_emails = emails;
			_messageFactory = messageFactory;
			_events = events;
			_partitionId = partitionId;
			_serializer = serializer;
		}

		public void Run()
		{
			EventJobHelper.Run(_events, _partitionId, ProcessEvent, EventState.ApplicationEmailing);
		}

		private void ProcessEvent(EventData data)
		{
			var applicationEventData = _serializer.Deserialize<EventDataForApplication>(data.Data);

			var messages = _messageFactory.Get(applicationEventData.ApplicationId, data.EventType, applicationEventData.Data);

			if (messages != null)
			{
				foreach (var message in messages)
				{
					var files = _serializer.Serialize(message.Files);

					_emails.Add(_partitionId, message.From, message.To, message.CopyTo,
						message.Subject, message.Body, message.IsBodyHtml, files);
				}
			}

			_events.SetState(data.Id, EventState.StateHistorySaving);
		}
	}
}