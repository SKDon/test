using System;
using System.Globalization;
using System.IO;
using System.Text;
using Alicargo.Core.Contracts.Common;

namespace Alicargo.Jobs.Bill.Helpers
{
	public sealed class CourseSource : ICourseSource
	{
		private const int EurPosition = 5;
		private readonly IHttpClient _httpClient;

		public CourseSource(IHttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public decimal GetEuroToRuble(string url)
		{
			var bytes = _httpClient.Get(url);
			var response = Encoding.ASCII.GetString(bytes);
			if(string.IsNullOrWhiteSpace(response))
			{
				throw new InvalidDataException("Was no data on the EUR course. Source: " + url);
			}

			var part = response.Split(new[] { ';' }, StringSplitOptions.None)[EurPosition];

			return decimal.Parse(part, new NumberFormatInfo { CurrencyDecimalSeparator = "." });
		}
	}
}