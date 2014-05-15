using System;
using System.Globalization;
using System.Text;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.Jobs.Core;
using Alicargo.Utilities;

namespace Alicargo.Jobs.Bill
{
	public sealed class EuroCourseJob : IJob
	{
		private const int EurPosition = 5;
		private readonly IHttpClient _httpClient;
		private readonly IHolder<DateTimeOffset> _previousTime;
		private readonly ISerializer _serializer;
		private readonly ISettingRepository _settings;

		public EuroCourseJob(
			ISettingRepository settings, IHttpClient httpClient, ISerializer serializer, IHolder<DateTimeOffset> previousTime)
		{
			_settings = settings;
			_httpClient = httpClient;
			_serializer = serializer;
			_previousTime = previousTime;
		}

		public void Work()
		{
			var data = _settings.Get(SettingType.Bill);
			var settings = _serializer.Deserialize<BillSettings>(data.Data);

			if(!settings.AutoUpdatePeriod.HasValue)
			{
				return;
			}

			var nextRunTime = _previousTime.Get().Add(settings.AutoUpdatePeriod.Value);
			if(nextRunTime > DateTimeProvider.Now)
			{
				return;
			}

			settings.EuroToRuble = GetEuroToRuble(settings.SourceUrl);

			data.Data = _serializer.Serialize(settings);

			Update(data);

			_previousTime.Set(DateTimeProvider.Now);
		}

		private decimal GetEuroToRuble(string sourceUrl)
		{
			// todo: send email on fail (279, 280)
			var bytes = _httpClient.Get(sourceUrl);
			var response = Encoding.ASCII.GetString(bytes);
			var part = response.Split(new[] { ';' }, StringSplitOptions.None)[EurPosition];

			return decimal.Parse(part, new NumberFormatInfo { CurrencyDecimalSeparator = "." });
		}

		private void Update(Setting data)
		{
			try
			{
				_settings.AddOrReplace(data);
			}
			catch(UpdateConflictException)
			{
			}
		}
	}
}