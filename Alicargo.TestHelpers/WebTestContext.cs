using System;
using System.Net;
using System.Net.Http;
using Alicargo.ViewModels;
using FluentAssertions;

namespace Alicargo.TestHelpers
{
	public sealed class WebTestContext : TestContext
	{
		public WebTestContext(string baseAddress, string login, string password)
		{
			var cookies = new CookieContainer();
			var handler = new HttpClientHandler
			{
				CookieContainer = cookies,
				UseCookies = true,
				MaxRequestContentBufferSize = int.MaxValue
			};

			HttpClient = new HttpClient(handler) { BaseAddress = new Uri(baseAddress) };

			HttpClient.PostAsJsonAsync("Authentication/Login", new SignIdModel
			{
				Login = login,
				Password = password
			}).ContinueWith(task =>
			{
				var responseMessage = task.Result;

				responseMessage.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
			}).Wait();
		}

		public HttpClient HttpClient { get; private set; }
	}
}
