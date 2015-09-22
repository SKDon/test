using System.Collections.Generic;
using Alicargo.Core.Calculation;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.Core.Tests.Calculation
{
	[TestClass]
	public class CalculationHelperTests
	{
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_fixture = new Fixture();
		}

		[TestMethod]
		public void GetProfit_ScotchCostEdited_Null_Test()
		{
			var data = _fixture.Create<ApplicationEditData>();
			data.CalculationProfit = null;
			data.CalculationTotalTariffCost = 1;
			data.TariffPerKg = 2;
			data.Weight = 3;
			data.Count = 4;
			data.Value = 5;
			data.InsuranceRate = 0.6f;
			data.FactureCostExEdited = 7;
			data.FactureCostEdited = 8;
			data.PickupCostEdited = 9;
			data.TransitCostEdited = 10;
			data.ScotchCostEdited = null;
			data.SenderId = 1;
			var tariffs = new Dictionary<long, decimal>
			{
				{ 1, 12 }
			};

			var actual = CalculationHelper.GetProfit(data, tariffs);

			actual.ShouldBeEquivalentTo(86);
		}

		[TestMethod]
		public void GetProfit_ScotchCostEdited_NotNull_Test()
		{
			var data = _fixture.Create<ApplicationEditData>();
			data.CalculationProfit = null;
			data.CalculationTotalTariffCost = 1;
			data.TariffPerKg = 2;
			data.Weight = 3;
			data.Count = 4;
			data.Value = 5;
			data.InsuranceRate = 0.6f;
			data.FactureCostExEdited = 7;
			data.FactureCostEdited = 8;
			data.PickupCostEdited = 9;
			data.TransitCostEdited = 10;
			data.ScotchCostEdited = 11;

			var actual = CalculationHelper.GetProfit(data, null);

			actual.ShouldBeEquivalentTo(82);
		}
	}
}