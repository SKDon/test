using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Awb;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.Awb
{
	public sealed class AwbEventLocalizedDataHelper : ILocalizedDataHelper
	{
		private readonly IAwbRepository _awbs;
		private readonly ISenderRepository _senders;

		public AwbEventLocalizedDataHelper(IAwbRepository awbs, ISenderRepository senders)
		{
			_awbs = awbs;
			_senders = senders;
		}

		public IDictionary<string, string> Get(string language, EventDataForEntity eventData)
		{
			var awb = _awbs.Get(eventData.EntityId).Single();
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
				{ "TotalVolume", aggregate.TotalVolume.ToString("N2") },
				{ "SenderName", GetSenderName(awb) }
			};
		}

		private string GetSenderName(AirWaybillEditData awb)
		{
			if(awb.SenderUserId == null) return null;

			var senderId = _senders.GetByUserId(awb.SenderUserId.Value);

			return senderId != null ? _senders.Get(senderId.Value).Name : null;
		}
	}
}