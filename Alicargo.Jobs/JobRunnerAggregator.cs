using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Alicargo.Jobs
{
	public sealed class JobRunnerAggregator
	{
		private readonly CancellationTokenSource _failedTokenSource = new CancellationTokenSource();
		private readonly TimeSpan _cancellationTimeout;
		private readonly IJobRunner[] _runners;
		private AggregateException _aggregateException = new AggregateException();
		private Task[] _tasks;

		public JobRunnerAggregator(TimeSpan cancellationTimeout, params IJobRunner[] runners)
		{
			_cancellationTimeout = cancellationTimeout;
			_runners = runners;
		}

		public void Run(CancellationTokenSource tokenSource)
		{
			tokenSource.Token.Register(() =>
			{
				_failedTokenSource.Token.WaitHandle.WaitOne(_cancellationTimeout);

				if (!_failedTokenSource.IsCancellationRequested)
				{
					_failedTokenSource.Cancel(false);
				}
			});

			_tasks = _runners.Select(runner => StartTask(runner, tokenSource)).ToArray();

			try
			{
				Task.WaitAll(_tasks, _failedTokenSource.Token);
			}
			catch (OperationCanceledException e)
			{
				CollectException(e);

				throw _aggregateException.Flatten();
			}
		}

		private Task StartTask(IJobRunner runner, CancellationTokenSource tokenSource)
		{
			try
			{
				return Task.Factory.StartNew(state => Run(runner, (CancellationTokenSource)state), tokenSource,
											 _failedTokenSource.Token);
			}
			catch
			{
				Cancel(tokenSource);

				throw;
			}
		}

		private void Run(IJobRunner runner, CancellationTokenSource tokenSource)
		{
			try
			{
				runner.Run(tokenSource);
			}
			catch (Exception e)
			{
				CollectException(e);

				Cancel(tokenSource);
			}
		}

		private static void Cancel(CancellationTokenSource tokenSource)
		{
			if (!tokenSource.IsCancellationRequested)
			{
				tokenSource.Cancel(false);				
			}
		}

		private void CollectException(Exception e)
		{
			lock (_aggregateException)
			{
				_aggregateException = _aggregateException == null
					? new AggregateException(e)
					: new AggregateException(_aggregateException, e);
			}
		}
	}
}