using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Alicargo.Jobs
{
	public sealed class JobRunnerHelper
	{
		private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
		private Task[] _tasks;

		public void RunJobs(IJobRunner[] runners)
		{
			_tasks = runners.Select(StartTask).ToArray();
		}

		private Task StartTask(IJobRunner runner)
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
