using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Services.Abstract;
using Alicargo.Utilities;

namespace Alicargo.Core.Services.Email
{
	public sealed class SilentMailSender : IMailSender
	{
		private readonly IMailSender _sender;
		private readonly ILog _log;

		public SilentMailSender(IMailSender sender, ILog log)
		{
			_sender = sender;
			_log = log;
		}

		public void Send(params EmailMessage[] messages)
		{
			try
			{
				_sender.Send(messages);
			}
			catch (Exception ex)
			{
				if (ex.IsCritical()) throw;

				_log.Error("Failed to send a message", ex);
			}
		}
	}
}