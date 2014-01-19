using System;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.Utilities;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.AirWaybill
{
	internal static class AwbMapper
	{
		public static AirWaybillData Map(AwbSenderModel model, long cargoIsFlewStateId)
		{
			return new AirWaybillData
			{
				StateId = cargoIsFlewStateId,
				CreationTimestamp = DateTimeProvider.Now,
				StateChangeTimestamp = DateTimeProvider.Now,
				Id = 0,
				PackingFileName = model.PackingFileName,
				InvoiceFileName = null,
				AWBFileName = model.AWBFileName,
				ArrivalAirport = model.ArrivalAirport,
				Bill = model.Bill,
				DepartureAirport = model.DepartureAirport,
				BrokerId = model.BrokerId,
				DateOfArrival = DateTimeOffset.Parse(model.DateOfArrivalLocalString),
				DateOfDeparture = DateTimeOffset.Parse(model.DateOfDepartureLocalString),
				GTD = null,
				GTDAdditionalFileName = null,
				GTDFileName = null,
				AdditionalCost = null,
				BrokerCost = null,
				CustomCost = null,
				FlightCost = model.FlightCost,
				TotalCostOfSenderForWeight = model.TotalCostOfSenderForWeight
			};
		}

		public static AirWaybillData Map(AwbAdminModel model, long cargoIsFlewStateId)
		{
			return new AirWaybillData
			{
				StateId = cargoIsFlewStateId,
				CreationTimestamp = DateTimeProvider.Now,
				StateChangeTimestamp = DateTimeProvider.Now,
				Id = 0,
				PackingFileName = model.PackingFileName,
				InvoiceFileName = model.InvoiceFileName,
				AWBFileName = model.AWBFileName,
				ArrivalAirport = model.ArrivalAirport,
				Bill = model.Bill,
				DepartureAirport = model.DepartureAirport,
				BrokerId = model.BrokerId,
				DateOfArrival = DateTimeOffset.Parse(model.DateOfArrivalLocalString),
				DateOfDeparture = DateTimeOffset.Parse(model.DateOfDepartureLocalString),
				GTD = null,
				GTDAdditionalFileName = model.GTDAdditionalFileName,
				GTDFileName = model.GTDFileName,
				AdditionalCost = model.AdditionalCost,
				BrokerCost = model.BrokerCost,
				CustomCost = model.CustomCost,
				FlightCost = model.FlightCost,
				TotalCostOfSenderForWeight = model.TotalCostOfSenderForWeight
			};
		}
	}
}