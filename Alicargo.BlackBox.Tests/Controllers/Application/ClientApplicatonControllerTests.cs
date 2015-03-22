using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Controllers.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.TestHelpers;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;
using Dapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ploeh.AutoFixture;

namespace Alicargo.BlackBox.Tests.Controllers.Application
{
	[TestClass]
	public class ClientApplicatonControllerTests
	{
		private CompositionHelper _context;
		private ClientApplicationController _controller;
		private Fixture _fixture;

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Dispose();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_fixture = new Fixture();
			_context = new CompositionHelper(Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString,
				RoleType.Client);

			_controller = _context.Kernel.Get<ClientApplicationController>();
		}

		[TestMethod]
		public void Test_Create_Post()
		{
			var model = _fixture.Create<ApplicationClientModel>();
			model.CountryId = TestConstants.TestCountryId;
			var transit = _fixture.Create<TransitEditModel>();
			transit.CityId = TestConstants.TestCityId1;

			var result = _controller.Create(model, transit);

			result.Should().BeOfType<RedirectToRouteResult>();

			using(var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				var data = connection.Query("select top 1 * from [dbo].[Application] order by [Id] desc").First();

				var forwarders = connection.Query<long>(
					"select [ForwarderId] from [dbo].[ForwarderCity] where [CityId] = @CityId",
					new { transit.CityId }).ToArray();

				forwarders.Should().Contain((long)data.ForwarderId);
				model.Count.ShouldBeEquivalentTo((int)data.Count);
				model.FactoryName.ShouldBeEquivalentTo((string)data.FactoryName);
				model.FactoryPhone.ShouldBeEquivalentTo((string)data.FactoryPhone);
				model.FactoryEmail.ShouldBeEquivalentTo((string)data.FactoryEmail);
				model.FactoryContact.ShouldBeEquivalentTo((string)data.FactoryContact);
				model.MarkName.ShouldBeEquivalentTo((string)data.MarkName);
				model.Characteristic.ShouldBeEquivalentTo((string)data.Characteristic);
				model.MethodOfDelivery.ShouldBeEquivalentTo((MethodOfDelivery)data.MethodOfDeliveryId);
				model.AddressLoad.ShouldBeEquivalentTo((string)data.AddressLoad);
				model.Invoice.ShouldBeEquivalentTo((string)data.Invoice);
				model.WarehouseWorkingTime.ShouldBeEquivalentTo((string)data.WarehouseWorkingTime);
				model.TermsOfDelivery.ShouldBeEquivalentTo((string)data.TermsOfDelivery);
				model.Weight.ShouldBeEquivalentTo((float)data.Weight);
				model.Volume.ShouldBeEquivalentTo((float)data.Volume);
				model.Currency.CurrencyId.ShouldBeEquivalentTo((int)data.CurrencyId);
				model.Currency.Value.ShouldBeEquivalentTo((decimal)data.Value);
				model.CountryId.ShouldBeEquivalentTo((long)data.CountryId);
				((object)data.SenderId).Should().BeNull();
				TestConstants.DefaultStateId.ShouldBeEquivalentTo((long)data.StateId);
			}
		}
	}
}