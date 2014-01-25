using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Controllers.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.TestHelpers;
using Alicargo.ViewModels.Application;
using Dapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ploeh.AutoFixture;

namespace Alicargo.BlackBox.Tests.Controllers.Application
{
	[TestClass]
	public class SenderApplicationControllerTests
	{
		private CompositionHelper _context;
		private SenderApplicationController _controller;
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
				RoleType.Sender);

			_controller = _context.Kernel.Get<SenderApplicationController>();
		}

		[TestMethod]
		public void Test_Create_Post()
		{
			var model = _fixture.Create<ApplicationSenderModel>();
			model.CountryId = TestConstants.TestCountryId;

			var result = _controller.Create(TestConstants.TestClientId1, model);

			result.Should().BeOfType<RedirectToRouteResult>();

			using(var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				var data = connection.Query("select top 1 * from [dbo].[Application] order by [Id] desc").First();

				var cityId = connection.Query<long>(
					"select f.[CityId] from [dbo].[Forwarder] f JOIN [dbo].[Application] a ON a.[ForwarderId] = f.[Id] WHERE a.[Id] = @Id",
					new { data.Id }).First();

				var clientCity = connection.Query<long>(
					"select t.[CityId] from [dbo].[Client] c JOIN [dbo].[Transit] t ON t.[Id] = c.[TransitId] WHERE c.[Id] = @ClientId",
					new { data.ClientId })
					.First();

				var countries = connection.Query<long>(
					"select c.[CountryId] from [dbo].[SenderCountry] c where c.[SenderId] = @SenderId",
					new { data.SenderId }).ToArray();

				cityId.ShouldBeEquivalentTo(clientCity);
				model.Count.ShouldBeEquivalentTo((int)data.Count);
				model.FactoryName.ShouldBeEquivalentTo((string)data.FactoryName);
				model.MarkName.ShouldBeEquivalentTo((string)data.MarkName);
				model.Invoice.ShouldBeEquivalentTo((string)data.Invoice);
				model.Weight.ShouldBeEquivalentTo((float)data.Weight);
				model.Volume.ShouldBeEquivalentTo((float)data.Volume);
				model.FactureCost.ShouldBeEquivalentTo((decimal)data.FactureCost);
				model.PickupCost.ShouldBeEquivalentTo((decimal)data.PickupCost);
				model.Currency.CurrencyId.ShouldBeEquivalentTo((int)data.CurrencyId);
				model.Currency.Value.ShouldBeEquivalentTo((decimal)data.Value);
				TestConstants.DefaultStateId.ShouldBeEquivalentTo((long)data.StateId);
				countries.Should().Contain((long)data.CountryId);
			}
		}
	}
}