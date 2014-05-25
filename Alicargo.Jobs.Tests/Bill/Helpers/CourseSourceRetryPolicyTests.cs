using System;
using Alicargo.Core.Contracts.Common;
using Alicargo.Jobs.Bill.Helpers;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace Alicargo.Jobs.Tests.Bill.Helpers
{
	[TestClass]
	public class CourseSourceRetryPolicyTests
	{
		private Mock<ICourseSource> _courseSource;
		private Fixture _fixture;
		private Mock<ILog> _log;

		[TestInitialize]
		public void TestInitialize()
		{
			_courseSource = new Mock<ICourseSource>(MockBehavior.Strict);
			_log = new Mock<ILog>(MockBehavior.Strict);
			_fixture = new Fixture();
		}

		[TestMethod]
		public void Test_GetEuroToRuble()
		{
			var url = _fixture.Create<string>();
			var expected = _fixture.Create<decimal>();
			_courseSource.Setup(x => x.GetEuroToRuble(url)).Returns(expected);

			var policy = new CourseSourceRetryPolicy(_courseSource.Object, 2, _log.Object, TimeSpan.FromMilliseconds(1));
			var actual = policy.GetEuroToRuble(url);

			actual.ShouldBeEquivalentTo(expected);
			_courseSource.Verify(x => x.GetEuroToRuble(url), Times.Once);
		}

		[TestMethod]
		[ExpectedException(typeof(TestException))]
		public void Test_OneAttempt()
		{
			var url = _fixture.Create<string>();
			_courseSource.Setup(x => x.GetEuroToRuble(url)).Throws(new TestException());

			var policy = new CourseSourceRetryPolicy(_courseSource.Object, 1, _log.Object, TimeSpan.FromMilliseconds(1));
			try
			{
				policy.GetEuroToRuble(url);
			}
			catch(TestException)
			{
				_courseSource.Verify(x => x.GetEuroToRuble(url), Times.Once);

				throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(TestException))]
		public void Test_TwoAttempts()
		{
			var url = _fixture.Create<string>();
			_courseSource.Setup(x => x.GetEuroToRuble(url)).Throws(new TestException());
			_log.Setup(x => x.Warning(It.IsAny<string>()));

			var policy = new CourseSourceRetryPolicy(_courseSource.Object, 2, _log.Object, TimeSpan.FromMilliseconds(1));
			try
			{
				policy.GetEuroToRuble(url);
			}
			catch(TestException)
			{
				_courseSource.Verify(x => x.GetEuroToRuble(url), Times.Exactly(2));
				_log.Verify(x => x.Warning(It.IsAny<string>()), Times.Once);

				throw;
			}
		}
	}
}