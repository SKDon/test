using System.Text;
using Alicargo.Core.Common;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.Core.BlackBox.Tests.Common
{
	[TestClass]
	public class HttpClientTests
	{
		private HttpClient _httpClient;

		[TestInitialize]
		public void TestInitialize()
		{
			_httpClient = new HttpClient();
		}

		[TestMethod]
		public void TestGet()
		{
			var bytes = _httpClient.Get("http://export.rbc.ru/free/cb.0/free.fcgi?period=DAILY&tickers=EUR&separator=;&data_format=BROWSER");

			var result = Encoding.UTF8.GetString(bytes);

			result.Should().NotBeNull();
		}
	}
}