using System.Data.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Controllers.Awb;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.DbContext;
using Alicargo.TestHelpers;
using Alicargo.Utilities;
using Alicargo.Utilities.Localization;
using Alicargo.ViewModels.AirWaybill;
using Dapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ploeh.AutoFixture;

namespace Alicargo.BlackBox.Tests.Controllers
{
	[TestClass]
	public class AdminAwbControllerTests
	{
		private const long FirstStateId = 7;

		private CompositionHelper _composition;
		private AdminAwbController _controller;
		private AlicargoDataContext _db;
		private Fixture _fixture;
		private readonly CultureInfo _currentCulture = CultureProvider.GetCultureInfo();

		[TestInitialize]
		public void TestInitialize()
		{
			_db = new AlicargoDataContext(Settings.Default.MainConnectionString);

			_fixture = new Fixture();
			_composition = new CompositionHelper(Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString);
			_controller = _composition.Kernel.Get<AdminAwbController>();
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_composition.Dispose();
		}

		[TestMethod]
		public void Test_Edit()
		{
			var entity = _db.AirWaybills.First();

			var model = _fixture
				.Build<AwbAdminModel>()
				.With(x => x.BrokerId, entity.BrokerId)
				.With(x => x.DateOfArrivalLocalString, LocalizationHelper.GetDate(DateTimeProvider.Now, _currentCulture))
				.With(x => x.DateOfDepartureLocalString, LocalizationHelper.GetDate(DateTimeProvider.Now, _currentCulture))
				.Create();

			_controller.Edit(entity.Id, model);

			Validate(model, entity.Id);
		}

		private void Validate(AwbAdminModel expectation, long id)
		{
			using(var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				var actual = connection.Query<AwbAdminModel>(
					"select * " +
					"from [dbo].[AirWaybill] where id = @id",
					new { id }).First();

				var actualData = connection.Query<dynamic>(
					"select DateOfDeparture, DateOfArrival " +
					"from [dbo].[AirWaybill] where id = @id",
					new { id }).First();

				actual.DateOfDepartureLocalString = LocalizationHelper.GetDate(actualData.DateOfDeparture, _currentCulture);
				actual.DateOfArrivalLocalString = LocalizationHelper.GetDate(actualData.DateOfArrival, _currentCulture);

				actual.ShouldBeEquivalentTo(expectation);
			}
		}

		[TestMethod]
		public void Test_Create()
		{
			var broker = _db.Brokers.First();
			var applicationData = _db.Applications.First(x => !x.AirWaybillId.HasValue);

			var count = _db.AirWaybills.Count();

			var model = _fixture
				.Build<AwbAdminModel>()
				.Without(x => x.GTD)
				.With(x => x.BrokerId, broker.Id)
				.With(x => x.DateOfArrivalLocalString, LocalizationHelper.GetDate(DateTimeProvider.Now, _currentCulture))
				.With(x => x.DateOfDepartureLocalString, LocalizationHelper.GetDate(DateTimeProvider.Now, _currentCulture))
				.Create();

			_controller.Create(applicationData.Id, model);

			var entity = _db.AirWaybills.Skip(count).Take(1).First();

			Validate(model, entity.Id);

			_db.Refresh(RefreshMode.OverwriteCurrentValues, applicationData);
			Assert.AreEqual(FirstStateId, applicationData.StateId);

			applicationData.AirWaybillId = null;
			_db.AirWaybills.DeleteOnSubmit(entity);
			_db.SubmitChanges();
		}
	}
}