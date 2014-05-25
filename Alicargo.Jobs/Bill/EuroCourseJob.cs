using System;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.Jobs.Bill.Helpers;
using Alicargo.Jobs.Core;
using Alicargo.Utilities;

namespace Alicargo.Jobs.Bill
{
	public sealed class EuroCourseJob : IJob
	{
		private readonly ICourseSource _courseSource;
		private readonly IHolder<DateTimeOffset> _previousTime;
		private readonly ISerializer _serializer;
		private readonly ISettingRepository _settings;

		public EuroCourseJob(
			ISettingRepository settings,
			ICourseSource courseSource,
			ISerializer serializer,
			IHolder<DateTimeOffset> previousTime)
		{
			_settings = settings;
			_courseSource = courseSource;
			_serializer = serializer;
			_previousTime = previousTime;
		}

		public void Work()
		{
			var data = _settings.Get(SettingType.Bill);
			var settings = _serializer.Deserialize<BillSettings>(data.Data);

			if(!settings.AutoUpdatePeriod.HasValue || string.IsNullOrWhiteSpace(settings.SourceUrl))
			{
				return;
			}

			var nextRunTime = _previousTime.Get().Add(settings.AutoUpdatePeriod.Value);
			if(nextRunTime > DateTimeProvider.Now)
			{
				return;
			}

			settings.EuroToRuble = _courseSource.GetEuroToRuble(settings.SourceUrl);

			data.Data = _serializer.Serialize(settings);

			Update(data);

			_previousTime.Set(DateTimeProvider.Now);
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