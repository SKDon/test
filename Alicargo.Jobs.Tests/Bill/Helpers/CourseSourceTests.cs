using System.Text;
using Alicargo.Core.Contracts.Common;
using Alicargo.Jobs.Bill.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace Alicargo.Jobs.Tests.Bill.Helpers
{
	[TestClass]
	public class CourseSourceTests
	{
		private CourseSource _courseSource;
		private Fixture _fixture;
		private Mock<IHttpClient> _httpClient;

		[TestInitialize]
		public void TestInitialize()
		{
			_fixture = new Fixture();
			_httpClient = new Mock<IHttpClient>(MockBehavior.Strict);

			_courseSource = new CourseSource(_httpClient.Object);
		}

		[TestMethod]
		public void TestWork()
		{
			var url = _fixture.Create<string>();
			var bytes = Encoding.ASCII.GetBytes("EUR;2014-05-15;;;;47.6173;;;1");
			_httpClient.Setup(x => x.Get(url)).Returns(bytes);

			var actual = _courseSource.GetEuroToRuble(url);

			actual.ShouldBeEquivalentTo((decimal)47.6173);
			_httpClient.Verify(x => x.Get(url), Times.Once);
		}
	}
}