using System;
using System.Net;
using System.Net.Http;
using Alicargo.Tests.Properties;
using Alicargo.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.Tests
{
	class WebTestContext : TestContext
	{
		public WebTestContext()
		{
			var cookies = new CookieContainer();
			var handler = new HttpClientHandler
			{
				CookieContainer = cookies,
				UseCookies = true,
				MaxRequestContentBufferSize = int.MaxValue
			};

			HttpClient = new HttpClient(handler) { BaseAddress = new Uri(Settings.Default.BaseAddress) };

			HttpClient.PostAsJsonAsync("User/Login", new SignIdModel
			{
				Login = Settings.Default.Login,
				Password = Settings.Default.Password
			}).ContinueWith(task =>
			{
				var responseMessage = task.Result;
				Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
			}).Wait();
		}

		public HttpClient HttpClient { get; private set; }
	}
}
