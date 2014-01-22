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
			_context.Kernel.Bind<ApplicationController>().ToSelf();

			_controller = _context.Kernel.Get<SenderApplicationController>();
		}

		[TestMethod]
		[TestCategory("black-box")]
		public void Test_Create_Post()
		{
			var model = _fixture.Create<ApplicationSenderModel>();

			var result = _controller.Create(TestConstants.TestClientId1, model);

			result.Should().BeOfType<RedirectToRouteResult>();

			using(var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				var data = connection.Query("select top 1 * from [dbo].[Application] order by [Id] desc").First();

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

				var countryId = connection.Query<long>("select [CountryId] from [dbo].[Sender] where [UserId] = @UserId",
					new { UserId = TestConstants.TestSenderUserId }).First();

				countryId.ShouldBeEquivalentTo((long)data.CountryId);
				TestConstants.DefaultStateId.ShouldBeEquivalentTo((long)data.StateId);
			}
		}
	}
}