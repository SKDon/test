using System;
using System.Threading;
using Alicargo.Core.Contracts.Common;
using Alicargo.Utilities;

namespace Alicargo.Jobs.Core
{
	public sealed class DefaultRunner : IRunner
	{
		private readonly Action _action;
		private readonly ILog _log;
		private readonly TimeSpan _pausePeriod;

		public DefaultRunner(
			Action action,
			string name,
			ILog log,
			TimeSpan pausePeriod)
		{
			Name = name;
			_action = action;
			_log = log;
			_pausePeriod = pausePeriod;
		}

		public void Run(CancellationTokenSource tokenSource)
		{
			while (!tokenSource.IsCancellationRequested)
			{
				try
				{
					_action();
				}
				catch (Exception e)
				{
					// todo: 1. think again (263)
					if (e.IsCritical())
					{
						tokenSource.Cancel(false);
						_log.Error("CRITICAL ERROR occurred during a job running in the runner " + Name, e);
						// todo: restart application, send email to support (263)
						throw;
					}

					_log.Error("An error occurred during a job running in the runner " + Name, e);
				}

				tokenSource.Token.WaitHandle.WaitOne(_pausePeriod);
			}
		}

		public string Name { get; private set; }
	}
}