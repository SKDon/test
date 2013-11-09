using System;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services;
using Alicargo.Core.Services.Abstract;
using Alicargo.Jobs.ApplicationEvents;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs
{
	public sealed class MailSenderJob : IJob
	{
		private readonly IEmailMessageRepository _messages;
		private readonly ShardSettings _shard;
		private readonly IMailSender _sender;
		private readonly IMessageFactory _factory;

		public MailSenderJob(IEmailMessageRepository messages, ShardSettings shard, IMailSender sender, IMessageFactory factory)
		{
			_messages = messages;
			_shard = shard;
			_sender = sender;
			_factory = factory;
		}

		public void Run()
		{
			var data = _messages.GetNext(EmailMessageState.New, _shard.ZeroBasedIndex);

			while (data != null)
			{
				try
				{
					var message = _factory.Get(data);

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

				data = _messages.GetNext(EmailMessageState.New, _shard.ZeroBasedIndex);
			}
		}
	}
}
