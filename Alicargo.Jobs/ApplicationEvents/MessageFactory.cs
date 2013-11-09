using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class MessageFactory : IMessageFactory
	{
		private readonly ISerializer _serializer;

		public MessageFactory(ISerializer serializer)
		{
			_serializer = serializer;
		}

		public EmailMessage[] Get(ApplicationEventType eventType, byte[] data)
		{
			throw new System.NotImplementedException();
		}

		public EmailMessage Get(EmailMessageData data)
		{
			return new EmailMessage(data.Subject, data.Body, EmailMessageData.Split(data.To))
			{
				CopyTo = EmailMessageData.Split(data.CopyTo),
				Files = _serializer.Deserialize<FileHolder[]>(data.Files),
				From = data.From,
				IsBodyHtml = data.IsBodyHtml
			};
		}
	}
}