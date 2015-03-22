using System;
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
			var bytes = _httpClient.Get("http://export.rbc.ru/free/cb.0/free.fcgi?period=DAILY&tickers=EUR&lastdays=1&separator=;&data_format=BROWSER&header=0");

			var result = Encoding.ASCII.GetString(bytes);
			if(string.IsNullOrWhiteSpace(result))
			{
				Assert.Inconclusive();
			}

			result.Should().StartWith("EUR;" + DateTime.Now.ToString("yyyy-"));
		}
	}
}