using System;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Contracts.Contracts;
using Alicargo.Controllers.Awb;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.DbContext;
using Alicargo.TestHelpers;
using Alicargo.ViewModels.AirWaybill;
using Dapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ploeh.AutoFixture;

namespace Alicargo.BlackBox.Tests.Controllers
{
	[TestClass]
	public class AirWaybillControllerTests
	{
		private const long FirstStateId = 7;

		private CompositionHelper _composition;
		private AirWaybillController _controller;
		private AlicargoDataContext _db;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_db = new AlicargoDataContext(Settings.Default.MainConnectionString);

			_fixture = new Fixture();
			_composition = new CompositionHelper(Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString);
			_controller = _composition.Kernel.Get<AirWaybillController>();
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_composition.Dispose();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Edit()
		{
			var entity = _db.AirWaybills.First();

			var model = _fixture
				.Build<AwbAdminModel>()
				.With(x => x.BrokerId, entity.BrokerId)
				.With(x => x.DateOfArrivalLocalString, LocalizationHelper.GetDate(DateTimeOffset.UtcNow, CultureInfo.CurrentCulture))
				.With(x => x.DateOfDepartureLocalString, LocalizationHelper.GetDate(DateTimeOffset.UtcNow, CultureInfo.CurrentCulture))
				.Create();

			_controller.Edit(entity.Id, model);

			using (var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				var actual = connection.Query<AwbAdminModel>(
					"select *, GTDFileData as GTDFile, GTDAdditionalFileData as GTDAdditionalFile, PackingFileData as PackingFile," +
					"InvoiceFileData as InvoiceFile, AWBFileData as AWBFile " +
					"from [dbo].[AirWaybill] where id = @id", new { entity.Id }).First();

				var actualData = connection.Query<AirWaybillData>(
					"select * " +
					"from [dbo].[AirWaybill] where id = @id", new { entity.Id }).First();

				actual.DateOfDepartureLocalString = LocalizationHelper.GetDate(actualData.DateOfDeparture, CultureInfo.CurrentCulture);
				actual.DateOfArrivalLocalString = LocalizationHelper.GetDate(actualData.DateOfArrival, CultureInfo.CurrentCulture);

				model.ShouldBeEquivalentTo(actual);
			}
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Create()
		{
			var broker = _db.Brokers.First();
			var applicationData = _db.Applications.First(x => !x.AirWaybillId.HasValue);

			var count = _db.AirWaybills.Count();

			var model = _fixture
				.Build<AwbAdminModel>()
				.Without(x => x.GTD)
				.With(x => x.BrokerId, broker.Id)
				.With(x => x.DateOfArrivalLocalString, LocalizationHelper.GetDate(DateTimeOffset.UtcNow, CultureInfo.CurrentCulture))
				.With(x => x.DateOfDepartureLocalString, LocalizationHelper.GetDate(DateTimeOffset.UtcNow, CultureInfo.CurrentCulture))
				.Create();

			_controller.Create(applicationData.Id, model);

			var entity = _db.AirWaybills.Skip(count).Take(1).First();

			using (var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				var actual = connection.Query<AwbAdminModel>(
					"select *, GTDFileData as GTDFile, GTDAdditionalFileData as GTDAdditionalFile, PackingFileData as PackingFile," +
					"InvoiceFileData as InvoiceFile, AWBFileData as AWBFile " +
					"from [dbo].[AirWaybill] where id = @id", new { entity.Id }).First();

				var actualData = connection.Query<AirWaybillData>(
					"select * " +
					"from [dbo].[AirWaybill] where id = @id", new { entity.Id }).First();

				actual.DateOfDepartureLocalString = LocalizationHelper.GetDate(actualData.DateOfDeparture, CultureInfo.CurrentCulture);
				actual.DateOfArrivalLocalString = LocalizationHelper.GetDate(actualData.DateOfArrival, CultureInfo.CurrentCulture);

				model.ShouldBeEquivalentTo(actual);
			}

			_db.Refresh(RefreshMode.OverwriteCurrentValues, applicationData);
			Assert.AreEqual(FirstStateId, applicationData.StateId);

			applicationData.AirWaybillId = null;
			_db.AirWaybills.DeleteOnSubmit(entity);
			_db.SubmitChanges();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_SetAirWaybill()
		{
			var application = _db.Applications.First(x => !x.AirWaybillId.HasValue);
			var airWaybill = _db.AirWaybills.First();

			_controller.SetAirWaybill(application.Id, airWaybill.Id);

			_db.Refresh(RefreshMode.OverwriteCurrentValues, application);

			Assert.AreEqual(airWaybill.Id, application.AirWaybillId);

			application.AirWaybillId = null;

			_db.SubmitChanges();
		}	
	}
}