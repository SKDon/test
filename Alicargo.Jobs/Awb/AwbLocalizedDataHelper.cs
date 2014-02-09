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
	internal sealed class AwbLocalizedDataHelper : ILocalizedDataHelper
	{
		private readonly IApplicationRepository _applications;
		private readonly IAwbRepository _awbs;
		private readonly ISerializer _serializer;

		public AwbLocalizedDataHelper(IApplicationRepository applications, IAwbRepository awbs, ISerializer serializer)
		{
			_applications = applications;
			_awbs = awbs;
			_serializer = serializer;
		}

		public IDictionary<string, string> Get(string language, EventDataForEntity eventData)
		{
			var awbId = eventData.EntityId;
			var applicationId = _serializer.Deserialize<long>(eventData.Data);

			var application = _applications.Get(applicationId);
			var awb = _awbs.Get(awbId).Single();
			var aggregate = _awbs.GetAggregate(new[] { awb.Id }).Single();

			var displayNumber = ApplicationHelper.GetDisplayNumber(applicationId, application.Count);
			var culture = CultureInfo.GetCultureInfo(language);

			return new Dictionary<string, string>
			{
				{ "ApplicationDisplay", displayNumber },
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