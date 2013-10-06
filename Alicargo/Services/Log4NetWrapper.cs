using System;
using Alicargo.Core.Services;

namespace Alicargo.Services
{
	internal sealed class Log4NetWrapper : ILog
	{
		private readonly log4net.ILog _log;

		public Log4NetWrapper(log4net.ILog log)
		{
			_log = log;
			_log.Info("Log is created");
		}

		public void Error(string message, Exception exception)
		{
			_log.Error(message, exception);
		}
	}
}