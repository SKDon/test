using System;
using System.Threading;
using Alicargo.Jobs.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Tests
{
	[TestClass]
	public class JobRunnerHelperTests
	{
		[TestMethod, ExpectedException(typeof(AggregateException))]
		public void Test_ExceptionOnStart()
		{
			var helper = new RunnerController();

			var runner = new Mock<IRunner>(MockBehavior.Strict);
			runner.Setup(x => x.Run(It.IsAny<CancellationTokenSource>())).Throws(new Exception("Test_ExceptionOnStart"));

			helper.Run(new[] { runner.Object });

			try
			{
				helper.StopAndWait(TimeSpan.FromMinutes(1));
			}
			catch (AggregateException e)
			{
				e.InnerExceptions.Count.ShouldBeEquivalentTo(1);
				e.InnerException.Message.ShouldBeEquivalentTo("Test_ExceptionOnStart");
				throw;
			}
		}
	}
}