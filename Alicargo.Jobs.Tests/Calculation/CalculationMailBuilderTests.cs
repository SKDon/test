//using System.Linq;
//using Alicargo.Contracts.Contracts;
//using Alicargo.Contracts.Contracts.User;
//using Alicargo.Jobs.Calculation;
//using Alicargo.TestHelpers;
//using FluentAssertions;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Ploeh.AutoFixture;

//namespace Alicargo.Jobs.Tests.Calculation
//{
//	[TestClass]
//	public class CalculationMailBuilderTests
//	{
//		private CalculationMailBuilder _builder;
//		private MockContainer _container;

//		[TestInitialize]
//		public void TestInitialize()
//		{
//			_container = new MockContainer();
//			_builder = _container.Create<CalculationMailBuilder>();
//		}

//		[TestMethod]
//		public void Test_Build()
//		{
//			var data = new CalculationData
//			{
//				AirWaybillDisplay = "AirWaybillDisplay",
//				FactoryName = "FactoryName",
//				ApplicationDisplay = "ApplicationDisplay",
//				ClientId = TestConstants.TestClientId1,
//				FactureCost = 2,
//				InsuranceCost = 3,
//				MarkName = "MarkName",
//				PickupCost = 4,
//				ScotchCost = 5,
//				TariffPerKg = 6,
//				TransitCost = 7,
//				Weight = 8
//			};

//			var client = _container.Build<ClientData>().With(x => x.ClientId, TestConstants.TestClientId1).Create();
//			var admins = _container.CreateMany<UserData>().ToArray();

//			const string subject = @"Расчет стоимости заявки #ApplicationDisplay FactoryName/MarkName";
//			const string text = @"AirWaybillDisplay
//
//8.00 kg * 6.00€ = 48.00€
//скотч - 5.00€
//страховка - 3.00€
//фактура - 2.00€
//доставка - 7.00€
//забор с фабрики - 4.00€
//
//Итого - 69.00€";

//			_container.AdminRepository.Setup(x => x.GetAll()).Returns(admins);
//			_container.ClientRepository.Setup(x => x.Get(TestConstants.TestClientId1)).Returns(client);

//			var message = _builder.Build(data);

//			message.Body.ShouldBeEquivalentTo(text);
//			message.Subject.ShouldBeEquivalentTo(subject);
//			message.To.ShouldAllBeEquivalentTo(client.Emails);
//			message.CopyTo.ShouldAllBeEquivalentTo(admins.Select(x => x.Email));
//		}
//	}
//}