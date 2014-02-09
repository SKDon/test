using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Jobs.Helpers.Abstract;
using Alicargo.Utilities;

namespace Alicargo.Jobs.Awb
{
	public sealed class AwbEventLocalizedDataHelper : ILocalizedDataHelper
	{
		private readonly IAwbRepository _awbs;
		private readonly ISerializer _serializer;

		public AwbEventLocalizedDataHelper(IAwbRepository awbs, ISerializer serializer)
		{
			_awbs = awbs;
			_serializer = serializer;
		}

		public IDictionary<string, string> Get(string language, EventDataForEntity eventData)
		{
			var awb = _serializer.Deserialize<AirWaybillData>(eventData.Data);
			var aggregate = _awbs.GetAggregate(new[] { awb.Id }).Single();
			var culture = CultureInfo.GetCultureInfo(language);

			return new Dictionary<string, string>
			{
				{ "DepartureAirport", awb.DepartureAirport },
				{ "DateOfDeparture", LocalizationHelper.GetDate(awb.DateOfDeparture, culture) },
				{ "ArrivalAirport", awb.ArrivalAirport },
				{ "DateOfArrival", LocalizationHelper.GetDate(awb.DateOfArrival, culture) },
				{ "TotalWeight", aggregate.TotalWeight.ToString("N2") },
				{ "TotalCount", aggregate.TotalCount.ToString("N2") },
				{ "TotalValue", aggregate.TotalValue.ToString("N2") },
				{ "AirWaybill", awb.Bill },
				{ "TotalVolume", aggregate.TotalVolume.ToString("N2") }
			};
		}
	}
}