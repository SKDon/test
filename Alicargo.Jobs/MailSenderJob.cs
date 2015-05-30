using System;
using Alicargo.Core.Contracts.Email;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.Jobs.Core;
using Alicargo.Utilities;

namespace Alicargo.Jobs
{
	public sealed class MailSenderJob : IJob
	{
		private readonly IEmailMessageRepository _messages;
		private readonly int _partitionId;
		private readonly IMailSender _sender;
		private readonly ISerializer _serializer;

		public MailSenderJob(
			IEmailMessageRepository messages,
			int partitionId,
			IMailSender sender,
			ISerializer serializer)
		{
			_messages = messages;
			_partitionId = partitionId;
			_sender = sender;
			_serializer = serializer;
		}

		public void Work()
		{
			var data = _messages.GetNext(EmailMessageState.New, _partitionId);

			while (data != null)
			{
				try
				{
					var message = Get(data);

					_sender.Send(message);

					_messages.SetState(data.Id, EmailMessageState.Sent);
				}
				catch (Exception e)
				{
					if (!e.IsCritical())
					{
						_messages.SetState(data.Id, EmailMessageState.Failed);
					}

					throw new JobException("Failed to send the message " + data.Id, e);
				}

				data = _messages.GetNext(EmailMessageState.New, _partitionId);
			}
		}

		private EmailMessage Get(EmailMessageData data)
		{
			return new EmailMessage(data.Subject, data.Body, data.From, EmailMessageData.Split(data.To), data.EmailSenderUserId)
			{
				CopyTo = EmailMessageData.Split(data.CopyTo),
				Files = _serializer.Deserialize<FileHolder[]>(data.Files),
				IsBodyHtml = data.IsBodyHtml
			};
		}
	}
}