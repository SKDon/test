using Alicargo.Areas.Admin.Serivices;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.Tests.Areas.Admin.Serivices
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
		public void Test_Bill_Calc()
		{
			var applicationId = _container.Create<long>();
			var bill = _container.Create<BillEditData>();
			bill.Price = "";
			bill.Total = " ";
			var calculation = _container.Create<CalculationData>();
			var money = CalculationDataHelper.GetMoney(calculation).ToString("n2");

			_container.BillRepository.Setup(x => x.Get(applicationId)).Returns(bill);
			_container.CalculationRepository.Setup(x => x.GetByApplication(applicationId)).Returns(calculation);

			var model = _factory.GetModel(applicationId);

			model.ShouldBeEquivalentTo(bill,
				options => options.ExcludingMissingProperties().Excluding(x => x.Price).Excluding(x => x.Total));
			model.BankDetails.ShouldBeEquivalentTo(bill, options => options.ExcludingMissingProperties());
			model.Price.ShouldBeEquivalentTo(money);
			model.Total.ShouldBeEquivalentTo(money);

			_container.BillRepository.Verify(x => x.Get(applicationId));
			_container.CalculationRepository.Verify(x => x.GetByApplication(applicationId));
		}

		[TestMethod]
		public void Test_Bill_NoCalc()
		{
			var applicationId = _container.Create<long>();
			var bill = _container.Create<BillEditData>();

			_container.BillRepository.Setup(x => x.Get(applicationId)).Returns(bill);
			_container.CalculationRepository.Setup(x => x.GetByApplication(applicationId)).Returns((CalculationData)null);

			var model = _factory.GetModel(applicationId);

			model.ShouldBeEquivalentTo(bill, options => options.ExcludingMissingProperties());
			model.BankDetails.ShouldBeEquivalentTo(bill, options => options.ExcludingMissingProperties());

			_container.BillRepository.Verify(x => x.Get(applicationId));
			_container.CalculationRepository.Verify(x => x.GetByApplication(applicationId));
		}

		[TestMethod]
		public void Test_NoBill_Calc()
		{
			var applicationId = _container.Create<long>();
			var settings = _container.Create<BillSettings>();
			var application = _container.Create<ApplicationData>();
			var client = _container.Create<ClientData>();
			var calculation = _container.Create<CalculationData>();
			var money = CalculationDataHelper.GetMoney(calculation).ToString("n2");

			_container.SettingRepository.Setup(x => x.GetData<BillSettings>(SettingType.Bill)).Returns(settings);
			_container.ApplicationRepository.Setup(x => x.Get(applicationId)).Returns(application);
			_container.ClientRepository.Setup(x => x.Get(application.ClientId)).Returns(client);
			_container.BillRepository.Setup(x => x.Get(applicationId)).Returns((BillEditData)null);
			_container.CalculationRepository.Setup(x => x.GetByApplication(applicationId)).Returns(calculation);

			var model = _factory.GetModel(applicationId);

			model.ShouldBeEquivalentTo(settings, options => options.ExcludingMissingProperties());
			model.BankDetails.ShouldBeEquivalentTo(settings, options => options.ExcludingMissingProperties());
			model.Count.ShouldBeEquivalentTo("1");
			model.Price.ShouldBeEquivalentTo(money);
			model.Total.ShouldBeEquivalentTo(money);
			model.Goods.Should().Contain(application.GetApplicationDisplay());
			model.Client.Should().Contain(client.LegalEntity).And.Contain(client.LegalAddress).And.Contain(client.INN);

			_container.SettingRepository.Verify(x => x.GetData<BillSettings>(SettingType.Bill));
			_container.ApplicationRepository.Verify(x => x.Get(applicationId));
			_container.ClientRepository.Verify(x => x.Get(application.ClientId));
			_container.BillRepository.Verify(x => x.Get(applicationId));
			_container.CalculationRepository.Verify(x => x.GetByApplication(applicationId));
		}

		[TestMethod]
		public void Test_NoBill_NoCalc()
		{
			var applicationId = _container.Create<long>();
			var settings = _container.Create<BillSettings>();
			var application = _container.Create<ApplicationData>();
			var client = _container.Create<ClientData>();

			_container.SettingRepository.Setup(x => x.GetData<BillSettings>(SettingType.Bill)).Returns(settings);
			_container.ApplicationRepository.Setup(x => x.Get(applicationId)).Returns(application);
			_container.ClientRepository.Setup(x => x.Get(application.ClientId)).Returns(client);
			_container.BillRepository.Setup(x => x.Get(applicationId)).Returns((BillEditData)null);
			_container.CalculationRepository.Setup(x => x.GetByApplication(applicationId)).Returns((CalculationData)null);

			var model = _factory.GetModel(applicationId);

			model.ShouldBeEquivalentTo(settings, options => options.ExcludingMissingProperties());
			model.BankDetails.ShouldBeEquivalentTo(settings, options => options.ExcludingMissingProperties());
			model.Count.ShouldBeEquivalentTo("1");
			model.Price.ShouldBeEquivalentTo(null);
			model.Total.ShouldBeEquivalentTo(null);
			model.Goods.Should().Contain(application.GetApplicationDisplay());
			model.Client.Should().Contain(client.LegalEntity).And.Contain(client.LegalAddress).And.Contain(client.INN);

			_container.SettingRepository.Verify(x => x.GetData<BillSettings>(SettingType.Bill));
			_container.ApplicationRepository.Verify(x => x.Get(applicationId));
			_container.ClientRepository.Verify(x => x.Get(application.ClientId));
			_container.BillRepository.Verify(x => x.Get(applicationId));
			_container.CalculationRepository.Verify(x => x.GetByApplication(applicationId));
		}
	}
}