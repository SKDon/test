using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Alicargo.Jobs
{
	public sealed class JobRunnerAggregator
	{
		private readonly IJobRunner[] _runners;

		public JobRunnerAggregator(params IJobRunner[] runners)
		{
			_runners = runners;
		}

		public void Run(CancellationTokenSource tokenSource)
		{
			var tasks = _runners.Select(x => Task.Factory.StartNew(() => x.Run(tokenSource), tokenSource.Token)).ToArray();

			Task.WaitAll(tasks);
		}
	}
}