using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alicargo.Jobs.Core;

namespace Alicargo.App_Start.Jobs
{
	public sealed class RunnerHelper
	{
		private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
		private Task[] _tasks;

		public void RunJobs(IRunner[] runners)
		{
			_tasks = runners.Select(StartTask).ToArray();
		}

		private Task StartTask(IRunner runner)
		{
			return Task.Factory.StartNew(() => runner.Run(_tokenSource), CancellationToken.None);
		}

		public bool StopAndWait(TimeSpan stopTimeout)
		{
			_tokenSource.Cancel(false);

			return Task.WaitAll(_tasks, stopTimeout);
		}
	}
}