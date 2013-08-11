using System;
using Alicargo.Core.Services;
using Alicargo.Services.Abstract;
using Alicargo.Services.Contract;
using log4net;

namespace Alicargo.Services.Email
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

		public void Send(params Message[] messages)
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

		public string DefaultFrom { get { return _sender.DefaultFrom; } }
	}
}