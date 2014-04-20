using System;
using Alicargo.Core.Contracts.Common;

namespace Alicargo.Services
{
	internal sealed class Log4NetWrapper : ILog
	{
		private readonly log4net.ILog _log;

		public Log4NetWrapper(log4net.ILog log)
		{
			_log = log;

			Info("The logger is created");
		}

		public void Error(string message, Exception exception)
		{
			_log.Error(message, exception);
		}

		public void Info(string message)
		{
			_log.Info(message);
		}
	}
}