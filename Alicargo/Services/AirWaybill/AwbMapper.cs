using System;
using System.Globalization;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.Utilities;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.AirWaybill
{
	internal static class AwbMapper
	{
		public static AwbAdminModel GetAdminModel(AirWaybillData data)
		{
			return new AwbAdminModel
			{
				ArrivalAirport = data.ArrivalAirport,
				Bill = data.Bill,
				DateOfArrivalLocalString = LocalizationHelper.GetDate(data.DateOfArrival, CultureInfo.CurrentCulture),
				DateOfDepartureLocalString = LocalizationHelper.GetDate(data.DateOfDeparture, CultureInfo.CurrentCulture),
				DepartureAirport = data.DepartureAirport,
				GTD = data.GTD,
				BrokerId = data.BrokerId,
				AdditionalCost = data.AdditionalCost,
				BrokerCost = data.BrokerCost,
				CustomCost = data.CustomCost,
				FlightCost = data.FlightCost,
				TotalCostOfSenderForWeight = data.TotalCostOfSenderForWeight,
				PackingFileName = data.PackingFileName,
				PackingFile = null,
				InvoiceFileName = data.InvoiceFileName,
				InvoiceFile = null,
				AWBFileName = data.AWBFileName,
				AWBFile = null,
				GTDFileName = data.GTDFileName,
				GTDFile = null,
				GTDAdditionalFileName = data.GTDAdditionalFileName,
				GTDAdditionalFile = null,
				DrawFileName = data.DrawFileName,
				DrawFile = null
			};
		}

		public static AwbBrokerModel GetBrokerModel(AirWaybillData data)
		{
			return new AwbBrokerModel
			{
				GTD = data.GTD,
				BrokerCost = data.BrokerCost,
				CustomCost = data.CustomCost,
				PackingFileName = data.PackingFileName,
				PackingFile = null,
				InvoiceFileName = data.InvoiceFileName,
				InvoiceFile = null,
				GTDFileName = data.GTDFileName,
				GTDFile = null,
				GTDAdditionalFileName = data.GTDAdditionalFileName,
				GTDAdditionalFile = null,
				DrawFileName = data.DrawFileName,
				DrawFile = null
			};
		}

		public static AirWaybillData GetData(AwbSenderModel model, long cargoIsFlewStateId)
		{
			return new AirWaybillData
			{
				Id = 0,
				StateId = cargoIsFlewStateId,
				CreationTimestamp = DateTimeProvider.Now,
				StateChangeTimestamp = DateTimeProvider.Now,
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
				TotalCostOfSenderForWeight = model.TotalCostOfSenderForWeight,
				PackingFileName = model.PackingFileName,
				InvoiceFileName = null,
				AWBFileName = model.AWBFileName,
				DrawFileName = null,
				GTDAdditionalFileName = null,
				GTDFileName = null
			};
		}

		public static AirWaybillData GetData(AwbAdminModel model, long cargoIsFlewStateId)
		{
			return new AirWaybillData
			{
				Id = 0,
				StateId = cargoIsFlewStateId,
				CreationTimestamp = DateTimeProvider.Now,
				StateChangeTimestamp = DateTimeProvider.Now,
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
				TotalCostOfSenderForWeight = model.TotalCostOfSenderForWeight,
				PackingFileName = model.PackingFileName,
				InvoiceFileName = model.InvoiceFileName,
				AWBFileName = model.AWBFileName,
				DrawFileName = model.DrawFileName,
				GTDAdditionalFileName = model.GTDAdditionalFileName,
				GTDFileName = model.GTDFileName
			};
		}

		public static AwbSenderModel GetSenderModel(AirWaybillData data)
		{
			return new AwbSenderModel
			{
				ArrivalAirport = data.ArrivalAirport,
				Bill = data.Bill,
				BrokerId = data.BrokerId,
				DateOfArrivalLocalString = LocalizationHelper.GetDate(data.DateOfArrival, CultureInfo.CurrentCulture),
				DateOfDepartureLocalString = LocalizationHelper.GetDate(data.DateOfDeparture, CultureInfo.CurrentCulture),
				DepartureAirport = data.DepartureAirport,
				FlightCost = data.FlightCost,
				TotalCostOfSenderForWeight = data.TotalCostOfSenderForWeight,
				PackingFileName = data.PackingFileName,
				PackingFile = null,
				AWBFileName = data.AWBFileName,
				AWBFile = null
			};
		}

		public static void Map(AwbAdminModel from, AirWaybillData to)
		{
			to.PackingFileName = from.PackingFileName;
			to.InvoiceFileName = from.InvoiceFileName;
			to.AWBFileName = from.AWBFileName;
			to.ArrivalAirport = from.ArrivalAirport;
			to.Bill = from.Bill;
			to.DepartureAirport = from.DepartureAirport;
			to.GTD = from.GTD;
			to.GTDFileName = from.GTDFileName;
			to.DrawFileName = from.DrawFileName;
			to.BrokerId = from.BrokerId;
			to.GTDAdditionalFileName = from.GTDAdditionalFileName;
			to.DateOfArrival = DateTimeOffset.Parse(from.DateOfArrivalLocalString);
			to.DateOfDeparture = DateTimeOffset.Parse(from.DateOfDepartureLocalString);
			to.AdditionalCost = from.AdditionalCost;
			to.BrokerCost = from.BrokerCost;
			to.CustomCost = from.CustomCost;
			to.FlightCost = from.FlightCost;
			to.TotalCostOfSenderForWeight = from.TotalCostOfSenderForWeight;
		}

		public static void Map(AwbSenderModel from, AirWaybillData to)
		{
			to.Bill = from.Bill;
			to.ArrivalAirport = from.ArrivalAirport;
			to.DepartureAirport = from.DepartureAirport;
			to.DateOfArrival = DateTimeOffset.Parse(from.DateOfArrivalLocalString);
			to.DateOfDeparture = DateTimeOffset.Parse(from.DateOfDepartureLocalString);
			to.BrokerId = from.BrokerId;
			to.PackingFileName = from.PackingFileName;
			to.AWBFileName = from.AWBFileName;
			to.FlightCost = from.FlightCost;
			to.TotalCostOfSenderForWeight = from.TotalCostOfSenderForWeight;
		}

		public static void Map(AwbBrokerModel from, AirWaybillData to)
		{
			to.PackingFileName = from.PackingFileName;
			to.InvoiceFileName = from.InvoiceFileName;
			to.GTD = from.GTD;
			to.GTDFileName = from.GTDFileName;
			to.DrawFileName = from.DrawFileName;
			to.GTDAdditionalFileName = from.GTDAdditionalFileName;
			to.BrokerCost = from.BrokerCost;
			to.CustomCost = from.CustomCost;
		}
	}
}