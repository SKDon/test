using System;
using System.Linq;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Jobs.Tests
{
	[TestClass]
	public class JobRunnerAggregatorTests
	{
		private JobRunnerAggregator _aggregator;
		private Mock<IJobRunner> _runner1;
		private Mock<IJobRunner> _runner2;

		[TestInitialize]
		public void TestInitialize()
		{
			_runner1 = new Mock<IJobRunner>(MockBehavior.Strict);
			_runner2 = new Mock<IJobRunner>(MockBehavior.Strict);

			_aggregator = new JobRunnerAggregator(TimeSpan.FromSeconds(2), _runner1.Object, _runner2.Object);

		}

		[TestMethod, Timeout(5000)]
		public void Test_Ok()
		{
			var tokenSource = new CancellationTokenSource();
			
			_runner1.Setup(x => x.Run(tokenSource)).Callback(() => Thread.Sleep(TimeSpan.FromSeconds(2)));
			_runner2.Setup(x => x.Run(tokenSource)).Callback(() => Thread.Sleep(TimeSpan.FromSeconds(1)));

			_aggregator.Run(tokenSource);
		}

		[TestMethod, Timeout(5000)]
		public void Test_RunnerCancel()
		{
			var tokenSource = new CancellationTokenSource();

			_runner1.Setup(x => x.Run(tokenSource)).Callback(() => tokenSource.Token.WaitHandle.WaitOne());
			_runner2.Setup(x => x.Run(tokenSource)).Callback(() =>
			{
				Thread.Sleep(TimeSpan.FromSeconds(1));
				tokenSource.Cancel();
			});

			_aggregator.Run(tokenSource);
		}

		[TestMethod, Timeout(5000)]
		public void Test_OutCancel()
		{
			var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(2));

			_runner1.Setup(x => x.Run(tokenSource)).Callback(() => tokenSource.Token.WaitHandle.WaitOne());
			_runner2.Setup(x => x.Run(tokenSource)).Callback(() => tokenSource.Token.WaitHandle.WaitOne());

			_aggregator.Run(tokenSource);
		}

		[TestMethod, Timeout(5000)]
		public void Test_OneRunnerFails()
		{
			var tokenSource = new CancellationTokenSource();

			var runner1Finished = false;
			_runner1.Setup(x => x.Run(tokenSource)).Callback(() =>
			{
				while (!tokenSource.IsCancellationRequested)
				{
					Thread.Sleep(TimeSpan.FromSeconds(1));
				}

				runner1Finished = true;
			});
			_runner2.Setup(x => x.Run(tokenSource)).Callback(() =>
			{
				Thread.Sleep(TimeSpan.FromSeconds(1));
				throw new Exception("test");
			});

			try
			{
				_aggregator.Run(tokenSource);
			}
			catch (AggregateException e)
			{
				e.InnerExceptions.Count.ShouldBeEquivalentTo(1);
			}

			runner1Finished.Should().BeTrue();
		}

		[TestMethod, Timeout(5000)]
		public void Test_FreezedRunner()
		{
			var tokenSource = new CancellationTokenSource();

			_runner1.Setup(x => x.Run(tokenSource)).Callback(() =>
			{
				while (true)
				{
					Thread.Sleep(TimeSpan.FromSeconds(1));
				} // ReSharper disable FunctionNeverReturns
			}); // ReSharper restore FunctionNeverReturns
			_runner2.Setup(x => x.Run(tokenSource)).Callback(() =>
			{
				Thread.Sleep(TimeSpan.FromSeconds(1));
				throw new Exception("test");
			});

			try
			{
				_aggregator.Run(tokenSource);
			}
			catch (AggregateException e)
			{
				e.InnerExceptions.Count.ShouldBeEquivalentTo(2);
			}
		}

		[TestMethod, Timeout(5000)]
		public void Test_AllRunnersFail()
		{
			var tokenSource = new CancellationTokenSource();

			_runner1.Setup(x => x.Run(tokenSource)).Throws(new Exception("test"));
			_runner2.Setup(x => x.Run(tokenSource)).Throws(new Exception("test"));

			try
			{
				_aggregator.Run(tokenSource);
			}
			catch (AggregateException e)
			{
				e.InnerExceptions.Count(x => x.Message == "test").ShouldBeEquivalentTo(2);
			}
		}
	}
}