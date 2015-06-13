using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
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

		public async Task<decimal> GetEuroToRuble(string url)
		{
			var bytes = await _httpClient.GetAsync(url);

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