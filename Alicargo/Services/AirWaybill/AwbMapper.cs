using System;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts.Awb;
using Alicargo.Utilities.Localization;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.AirWaybill
{
	internal static class AwbMapper
	{
		public static AwbAdminModel GetAdminModel(AirWaybillEditData data)
		{
			var cultureInfo = CultureProvider.GetCultureInfo();

			return new AwbAdminModel
			{
				ArrivalAirport = data.ArrivalAirport,
				Bill = data.Bill,
				DateOfArrivalLocalString = LocalizationHelper.GetDate(data.DateOfArrival, cultureInfo),
				DateOfDepartureLocalString = LocalizationHelper.GetDate(data.DateOfDeparture, cultureInfo),
				DepartureAirport = data.DepartureAirport,
				GTD = data.GTD,
				BrokerId = data.BrokerId,
				AdditionalCost = data.AdditionalCost,
				BrokerCost = data.BrokerCost,
				CustomCost = data.CustomCost,
				FlightCost = data.FlightCost,
				TotalCostOfSenderForWeight = data.TotalCostOfSenderForWeight
			};
		}

		public static AwbBrokerModel GetBrokerModel(AirWaybillEditData data)
		{
			return new AwbBrokerModel
			{
				GTD = data.GTD,
				BrokerCost = data.BrokerCost,
				CustomCost = data.CustomCost
			};
		}

		public static AirWaybillEditData GetData(AwbSenderModel model)
		{
			return new AirWaybillEditData
			{
				ArrivalAirport = model.ArrivalAirport,
				Bill = model.Bill,
				DepartureAirport = model.DepartureAirport,
				BrokerId = model.BrokerId,
				DateOfArrival = DateTimeOffset.Parse(model.DateOfArrivalLocalString),
				DateOfDeparture = DateTimeOffset.Parse(model.DateOfDepartureLocalString),
				GTD = null,
				AdditionalCost = null,
				BrokerCost = null,
				CustomCost = null,
				FlightCost = model.FlightCost,
				TotalCostOfSenderForWeight = model.TotalCostOfSenderForWeight
			};
		}

		public static AirWaybillEditData GetData(AwbAdminModel model)
		{
			return new AirWaybillEditData
			{
				ArrivalAirport = model.ArrivalAirport,
				Bill = model.Bill,
				DepartureAirport = model.DepartureAirport,
				BrokerId = model.BrokerId,
				DateOfArrival = DateTimeOffset.Parse(model.DateOfArrivalLocalString),
				DateOfDeparture = DateTimeOffset.Parse(model.DateOfDepartureLocalString),
				GTD = null,
				AdditionalCost = model.AdditionalCost,
				BrokerCost = model.BrokerCost,
				CustomCost = model.CustomCost,
				FlightCost = model.FlightCost,
				TotalCostOfSenderForWeight = model.TotalCostOfSenderForWeight
			};
		}

		public static AwbSenderModel GetSenderModel(AirWaybillEditData data)
		{
			var currentCulture = CultureProvider.GetCultureInfo();

			return new AwbSenderModel
			{
				ArrivalAirport = data.ArrivalAirport,
				Bill = data.Bill,
				BrokerId = data.BrokerId,
				DateOfArrivalLocalString = LocalizationHelper.GetDate(data.DateOfArrival, currentCulture),
				DateOfDepartureLocalString = LocalizationHelper.GetDate(data.DateOfDeparture, currentCulture),
				DepartureAirport = data.DepartureAirport,
				FlightCost = data.FlightCost,
				TotalCostOfSenderForWeight = data.TotalCostOfSenderForWeight
			};
		}

		public static void Map(AwbAdminModel from, AirWaybillEditData to)
		{
			to.ArrivalAirport = from.ArrivalAirport;
			to.Bill = from.Bill;
			to.DepartureAirport = from.DepartureAirport;
			to.GTD = from.GTD;
			to.BrokerId = from.BrokerId;
			to.DateOfArrival = DateTimeOffset.Parse(from.DateOfArrivalLocalString);
			to.DateOfDeparture = DateTimeOffset.Parse(from.DateOfDepartureLocalString);
			to.AdditionalCost = from.AdditionalCost;
			to.BrokerCost = from.BrokerCost;
			to.CustomCost = from.CustomCost;
			to.FlightCost = from.FlightCost;
			to.TotalCostOfSenderForWeight = from.TotalCostOfSenderForWeight;
		}

		public static void Map(AwbSenderModel from, AirWaybillEditData to)
		{
			to.Bill = from.Bill;
			to.ArrivalAirport = from.ArrivalAirport;
			to.DepartureAirport = from.DepartureAirport;
			to.DateOfArrival = DateTimeOffset.Parse(from.DateOfArrivalLocalString);
			to.DateOfDeparture = DateTimeOffset.Parse(from.DateOfDepartureLocalString);
			to.BrokerId = from.BrokerId;
			to.FlightCost = from.FlightCost;
			to.TotalCostOfSenderForWeight = from.TotalCostOfSenderForWeight;
		}

		public static void Map(AwbBrokerModel from, AirWaybillEditData to)
		{
			to.GTD = from.GTD;
			to.BrokerCost = from.BrokerCost;
			to.CustomCost = from.CustomCost;
		}
	}
}