using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Models;
using Alicargo.Core.Services;
using Alicargo.Core.Services.Abstract;

namespace Alicargo.Services.Email
{
    internal sealed class SilentMailSender : IMailSender
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

		public string DefaultFrom { get { return _sender.DefaultFrom; } }
	}
}