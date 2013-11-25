using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services;
using Alicargo.Core.Services.Abstract;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs
{
	public sealed class MailSenderJob : IJob
	{
		private readonly IEmailMessageRepository _messages;
		private readonly int _partitionId;
		private readonly IMailSender _sender;
		private readonly ISerializer _serializer;

		public MailSenderJob(
			IEmailMessageRepository messages, int partitionId, IMailSender sender, ISerializer serializer)
		{
			_messages = messages;
			_partitionId = partitionId;
			_sender = sender;
			_serializer = serializer;
		}

		public void Run()
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

					throw;
				}

				data = _messages.GetNext(EmailMessageState.New, _partitionId);
			}
		}

		private EmailMessage Get(EmailMessageData data)
		{
			return new EmailMessage(data.Subject, data.Body, data.From, EmailMessageData.Split(data.To))
			{
				CopyTo = EmailMessageData.Split(data.CopyTo),
				Files = _serializer.Deserialize<FileHolder[]>(data.Files),
				IsBodyHtml = data.IsBodyHtml
			};
		}
	}
}