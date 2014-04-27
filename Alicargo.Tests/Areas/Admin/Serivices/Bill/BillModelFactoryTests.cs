using Alicargo.Areas.Admin.Serivices.Bill;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.Tests.Areas.Admin.Serivices.Bill
{
	[TestClass]
	public class BillModelFactoryTests
	{
		private MockContainer _container;
		private BillModelFactory _factory;

		[TestInitialize]
		public void TestInitialize()
		{
			_container = new MockContainer();
			_factory = _container.Create<BillModelFactory>();
		}

		[TestMethod]
		public void Test_GetBillModel()
		{
			var bill = _container.Create<BillData>();

			var model = _factory.GetBillModel(bill);

			model.ShouldBeEquivalentTo(bill, options => options.ExcludingMissingProperties());
			model.BankDetails.ShouldBeEquivalentTo(bill, options => options.ExcludingMissingProperties());
			model.PriceRuble.ShouldBeEquivalentTo(bill.Price * bill.EuroToRuble);
		}

		[TestMethod]
		public void Test_GetBillModelByApplication_WithCalc()
		{
			var applicationId = _container.Create<long>();
			var settings = _container.Create<BillSettings>();
			var application = _container.Create<ApplicationData>();
			var client = _container.Create<ClientData>();
			var calculation = _container.Create<CalculationData>();
			var money = CalculationDataHelper.GetMoney(calculation) * settings.EuroToRuble;

			_container.SettingRepository.Setup(x => x.GetData<BillSettings>(SettingType.Bill)).Returns(settings);
			_container.ApplicationRepository.Setup(x => x.Get(applicationId)).Returns(application);
			_container.ClientRepository.Setup(x => x.Get(application.ClientId)).Returns(client);
			_container.CalculationRepository.Setup(x => x.GetByApplication(applicationId)).Returns(calculation);

			var model = _factory.GetBillModelByApplication(applicationId);

			model.ShouldBeEquivalentTo(settings, options => options.ExcludingMissingProperties());
			model.BankDetails.ShouldBeEquivalentTo(settings, options => options.ExcludingMissingProperties());
			model.Count.ShouldBeEquivalentTo(1);
			model.PriceRuble.ShouldBeEquivalentTo(money);
			model.Goods.Should().Contain(application.GetApplicationDisplay());
			model.Client.Should().Contain(client.LegalEntity).And.Contain(client.LegalAddress).And.Contain(client.INN);

			_container.SettingRepository.Verify(x => x.GetData<BillSettings>(SettingType.Bill));
			_container.ApplicationRepository.Verify(x => x.Get(applicationId));
			_container.ClientRepository.Verify(x => x.Get(application.ClientId));
			_container.CalculationRepository.Verify(x => x.GetByApplication(applicationId));
		}

		[TestMethod]
		public void Test_GetBillModelByApplication_NoCalc()
		{
			var applicationId = _container.Create<long>();
			var settings = _container.Create<BillSettings>();
			var application = _container.Create<ApplicationData>();
			var client = _container.Create<ClientData>();

			_container.SettingRepository.Setup(x => x.GetData<BillSettings>(SettingType.Bill)).Returns(settings);
			_container.ApplicationRepository.Setup(x => x.Get(applicationId)).Returns(application);
			_container.ClientRepository.Setup(x => x.Get(application.ClientId)).Returns(client);
			_container.CalculationRepository.Setup(x => x.GetByApplication(applicationId)).Returns((CalculationData)null);

			var model = _factory.GetBillModelByApplication(applicationId);

			model.ShouldBeEquivalentTo(settings, options => options.ExcludingMissingProperties());
			model.BankDetails.ShouldBeEquivalentTo(settings, options => options.ExcludingMissingProperties());
			model.Count.ShouldBeEquivalentTo(1);
			model.PriceRuble.ShouldBeEquivalentTo(null);
			model.Goods.Should().Contain(application.GetApplicationDisplay());
			model.Client.Should().Contain(client.LegalEntity).And.Contain(client.LegalAddress).And.Contain(client.INN);

			_container.SettingRepository.Verify(x => x.GetData<BillSettings>(SettingType.Bill));
			_container.ApplicationRepository.Verify(x => x.Get(applicationId));
			_container.ClientRepository.Verify(x => x.Get(application.ClientId));
			_container.CalculationRepository.Verify(x => x.GetByApplication(applicationId));
		}
	}
}