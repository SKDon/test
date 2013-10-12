using Alicargo.Contracts.Contracts;
using Alicargo.Core.Localization;
using Resources;

namespace Alicargo.Jobs.Calculation
{
	internal sealed class CalculationExcelRow
	{
		private readonly CalculationData _data;

		public CalculationExcelRow(CalculationData data)
		{
			_data = data;
		}

		[DisplayNameLocalized(typeof(Entities), "AirWaybill")]
		public string AirWaybillDisplay
		{
			get { return _data.AirWaybillDisplay; }
		}

		[DisplayNameLocalized(typeof(Entities), "DisplayNumber")]
		public string ApplicationDisplay
		{
			get { return _data.ApplicationDisplay; }
		}

		[DisplayNameLocalized(typeof(Entities), "Mark")]
		public string MarkName
		{
			get { return _data.MarkName; }
		}

		[DisplayNameLocalized(typeof(Entities), "Weigth")]
		public float Weight
		{
			get { return _data.Weight; }
		}

		[DisplayNameLocalized(typeof(Entities), "TariffPerKg")]
		public decimal TariffPerKg
		{
			get { return _data.TariffPerKg; }
		}

		[DisplayNameLocalized(typeof(Entities), "TotalTariffCost")]
		public decimal TotalTariffCost
		{
			get { return _data.TariffPerKg * (decimal)_data.Weight; }
		}

		[DisplayNameLocalized(typeof(Entities), "ScotchCost")]
		public decimal ScotchCost
		{
			get { return _data.ScotchCost; }
		}

		[DisplayNameLocalized(typeof(Entities), "Insurance")]
		public decimal InsuranceCost
		{
			get { return _data.InsuranceCost; }
		}

		[DisplayNameLocalized(typeof(Entities), "FactureCost")]
		public decimal FactureCost
		{
			get { return _data.FactureCost; }
		}

		[DisplayNameLocalized(typeof(Entities), "Total")]
		public decimal Total
		{
			get { return TotalTariffCost + _data.ScotchCost + _data.FactureCost + _data.InsuranceCost; }
		}
	}
}