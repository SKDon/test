using System;
using System.Threading;
using Alicargo.Core.Services;
using Alicargo.Jobs;
using Ninject;

namespace Alicargo.App_Start
{
	internal sealed class StatelessJobRunner : IJobRunner
	{
		private readonly ILog _log;
		private readonly IKernel _kernel;
		private readonly string _jobName;
		private readonly TimeSpan _pausePeriod;

		public StatelessJobRunner(
			ILog log,
			IKernel kernel,
			string jobName,
			TimeSpan pausePeriod)
		{
			_log = log;
			_kernel = kernel;
			_jobName = jobName;
			_pausePeriod = pausePeriod;
		}

		public void Run(CancellationTokenSource tokenSource)
		{
			while (!tokenSource.IsCancellationRequested)
			{
				try
				{
					var job = _kernel.Get<IJob>(_jobName);

					job.Run();
				}
				catch (Exception e)
				{
					if (e.IsCritical())
					{
						throw;
					}

					_log.Error("An error occurred during '" + _jobName + "' job running", e);
				}

				tokenSource.Token.WaitHandle.WaitOne(_pausePeriod);
			}
		}
	}
}