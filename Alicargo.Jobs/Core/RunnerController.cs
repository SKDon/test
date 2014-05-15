using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Alicargo.Jobs.Core
{
	public sealed class RunnerController
	{
		private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
		private Task[] _tasks;

		public void Run(IRunner[] runners)
		{
			_tasks = runners.Select(Start).ToArray();
		}

		public bool StopAndWait(TimeSpan stopTimeout)
		{
			_tokenSource.Cancel(false);

			return Task.WaitAll(_tasks, stopTimeout);
		}

		private Task Start(IRunner runner)
		{
			return Task.Run(() => runner.Run(_tokenSource), CancellationToken.None);
		}
	}
}