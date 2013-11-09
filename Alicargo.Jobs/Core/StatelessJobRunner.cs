using System;
using System.Threading;
using Alicargo.Core.Services;
using Alicargo.Core.Services.Abstract;

namespace Alicargo.Jobs.Core
{
	public sealed class StatelessJobRunner : IJobRunner
	{
		private readonly Action _action;
		private readonly ILog _log;
		private readonly TimeSpan _pausePeriod;

		public StatelessJobRunner(
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
					if (e.IsCritical())
					{
						tokenSource.Cancel(false);
						_log.Error("CRITICAL ERROR occurred during a job running in the runner " + Name, e);
						// todo: restart application, send email to support
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