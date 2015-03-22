using System;
using System.Diagnostics;
using Alicargo.Areas.Admin.Models;
using Alicargo.Areas.Admin.Serivices.Bill;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.TestHelpers;
using Alicargo.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ploeh.AutoFixture;

namespace Alicargo.BlackBox.Tests.Areas.Admin.Serivices
{
	[TestClass]
	public class BillManagerTests
	{
		private IBillRepository _billRepository;
		private CompositionHelper _composition;
		private Fixture _fixture;
		private BillManager _manager;

		[TestCleanup]
		public void TestCleanup()
		{
			_composition.Dispose();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_fixture = new Fixture();
			_composition = new CompositionHelper(Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString);

			_manager = _composition.Kernel.Get<BillManager>();
			_billRepository = _composition.Kernel.Get<IBillRepository>();
		}

		[TestMethod]
		public void Test_AddBill()
		{
			var model = _fixture.Create<BillModel>();
			var number = _fixture.Create<int>();

			_manager.Save(TestConstants.TestApplicationId, number, model, DateTimeProvider.Now, null);

			var billData = _billRepository.Get(TestConstants.TestApplicationId);

			billData.Number.ShouldBeEquivalentTo(number);
			billData.SendDate.Should().NotHaveValue();
			Debug.Assert(model.PriceRuble != null, "model.PriceRuble != null");
			billData.Price.Should().BeApproximately(model.PriceRuble.Value / billData.EuroToRuble, (decimal)0.0001);
			billData.ShouldBeEquivalentTo(model, options => options.ExcludingMissingProperties());
			billData.ShouldBeEquivalentTo(model.BankDetails, options => options.ExcludingMissingProperties());
		}

		[TestMethod]
		public void Test_UpdateBill()
		{
			_manager.Save(TestConstants.TestApplicationId, _fixture.Create<int>(), _fixture.Create<BillModel>(), DateTimeProvider.Now, DateTimeProvider.Now);

			var model = _fixture.Create<BillModel>();
			var number = _fixture.Create<int>();
			var sendDate = new DateTimeOffset(new DateTime(2000, 1, 1));
			_manager.Save(TestConstants.TestApplicationId, number, model, DateTimeProvider.Now, sendDate);

			var billData = _billRepository.Get(TestConstants.TestApplicationId);

			billData.Number.ShouldBeEquivalentTo(number);
			billData.SendDate.ShouldBeEquivalentTo(sendDate);
			Debug.Assert(model.PriceRuble != null, "model.PriceRuble != null");
			billData.Price.Should().BeApproximately(model.PriceRuble.Value / billData.EuroToRuble, (decimal)0.0001);
			billData.ShouldBeEquivalentTo(model, options => options.ExcludingMissingProperties());
			billData.ShouldBeEquivalentTo(model.BankDetails, options => options.ExcludingMissingProperties());
		}
	}
}