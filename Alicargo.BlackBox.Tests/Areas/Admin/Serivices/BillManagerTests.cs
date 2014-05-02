﻿using Alicargo.Areas.Admin.Models;
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

			_manager.SaveBill(TestConstants.TestApplicationId, number, model, DateTimeProvider.Now);

			var billData = _billRepository.Get(TestConstants.TestApplicationId);

			billData.Number.ShouldBeEquivalentTo(number);
			billData.Price.ShouldBeEquivalentTo(model.PriceRuble / billData.EuroToRuble);
			billData.ShouldBeEquivalentTo(model, options => options.ExcludingMissingProperties());
			billData.ShouldBeEquivalentTo(model.BankDetails, options => options.ExcludingMissingProperties());
		}

		[TestMethod]
		public void Test_UpdateBill()
		{
			_manager.SaveBill(TestConstants.TestApplicationId, _fixture.Create<int>(), _fixture.Create<BillModel>(), DateTimeProvider.Now);

			var model = _fixture.Create<BillModel>();
			var number = _fixture.Create<int>();
			_manager.SaveBill(TestConstants.TestApplicationId, number, model, DateTimeProvider.Now);

			var billData = _billRepository.Get(TestConstants.TestApplicationId);

			billData.Number.ShouldBeEquivalentTo(number);
			billData.Price.ShouldBeEquivalentTo(model.PriceRuble / billData.EuroToRuble);
			billData.ShouldBeEquivalentTo(model, options => options.ExcludingMissingProperties());
			billData.ShouldBeEquivalentTo(model.BankDetails, options => options.ExcludingMissingProperties());
		}
	}
}