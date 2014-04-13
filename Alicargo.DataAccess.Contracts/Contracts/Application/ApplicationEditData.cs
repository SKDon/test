using System;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Contracts.Application
{
	public class ApplicationEditData
	{
		public static ApplicationEditData Copy(ApplicationEditData data)
		{
			return new ApplicationEditData
			{
				Invoice = data.Invoice,
				Characteristic = data.Characteristic,
				AddressLoad = data.AddressLoad,
				WarehouseWorkingTime = data.WarehouseWorkingTime,
				Weight = data.Weight,
				Count = data.Count,
				Volume = data.Volume,
				TermsOfDelivery = data.TermsOfDelivery,
				Value = data.Value,
				CurrencyId = data.CurrencyId,
				CountryId = data.CountryId,
				DateInStock = data.DateInStock,
				DateOfCargoReceipt = data.DateOfCargoReceipt,
				FactoryName = data.FactoryName,
				FactoryPhone = data.FactoryPhone,
				FactoryEmail = data.FactoryEmail,
				FactoryContact = data.FactoryContact,
				MarkName = data.MarkName,
				TransitReference = data.TransitReference,
				StateId = data.StateId,
				MethodOfDelivery = data.MethodOfDelivery,
				ClientId = data.ClientId,
				TransitId = data.TransitId,
				AirWaybillId = data.AirWaybillId,
				SenderId = data.SenderId,
				ForwarderId = data.ForwarderId,
				Class = data.Class,
				FactureCost = data.FactureCost,
				FactureCostEx = data.FactureCostEx,
				PickupCost = data.PickupCost,
				FactureCostEdited = data.FactureCostEdited,
				FactureCostExEdited = data.FactureCostExEdited,
				TransitCostEdited = data.TransitCostEdited,
				ScotchCostEdited = data.ScotchCostEdited,
				PickupCostEdited = data.PickupCostEdited,
				TariffPerKg = data.TariffPerKg,
				TransitCost = data.TransitCost,
				SenderRate = data.SenderRate,
				CalculationTotalTariffCost = data.CalculationTotalTariffCost,
				CalculationProfit = data.CalculationProfit,
				InsuranceRate = data.InsuranceRate
			};
		}

		public string Invoice { get; set; }
		public string Characteristic { get; set; }
		public string AddressLoad { get; set; }
		public string WarehouseWorkingTime { get; set; }
		public float? Weight { get; set; }
		public int? Count { get; set; }
		public float Volume { get; set; }
		public string TermsOfDelivery { get; set; }
		public decimal Value { get; set; }
		public int CurrencyId { get; set; }
		public long CountryId { get; set; }
		public DateTimeOffset? DateInStock { get; set; }
		public DateTimeOffset? DateOfCargoReceipt { get; set; }
		public string FactoryName { get; set; }
		public string FactoryPhone { get; set; }
		public string FactoryEmail { get; set; }
		public string FactoryContact { get; set; }
		public string MarkName { get; set; }
		public string TransitReference { get; set; }
		public long StateId { get; set; }
		public MethodOfDelivery MethodOfDelivery { get; set; }
		public long ClientId { get; set; }
		public long TransitId { get; set; }
		public long? AirWaybillId { get; set; }
		public long SenderId { get; set; }
		public long ForwarderId { get; set; }
		public ClassType? Class { get; set; }

		public decimal? FactureCost { get; set; }
		public decimal? FactureCostEx { get; set; }
		public decimal? PickupCost { get; set; }
		public decimal? FactureCostEdited { get; set; }
		public decimal? FactureCostExEdited { get; set; }
		public decimal? TransitCostEdited { get; set; }
		public decimal? ScotchCostEdited { get; set; }
		public decimal? PickupCostEdited { get; set; }
		public decimal? TariffPerKg { get; set; }
		public decimal? TransitCost { get; set; }
		public decimal? SenderRate { get; set; }
		public decimal? CalculationTotalTariffCost { get; set; }
		public decimal? CalculationProfit { get; set; }
		public float InsuranceRate { get; set; }
	}
}