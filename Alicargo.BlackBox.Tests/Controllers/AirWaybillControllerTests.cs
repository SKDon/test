using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Core.Services;
using Alicargo.DataAccess.DbContext;
using Alicargo.TestHelpers;
using Alicargo.ViewModels.AirWaybill;
using EmitMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.BlackBox.Tests.Controllers
{
	[TestClass]
	public class AirWaybillControllerTests
	{
		private const long FirstStateId = 7;
		private const long DefaultStateId = 1;

		private HttpClient _client;
		private AlicargoDataContext _db;
		private WebTestContext _context;

		[TestInitialize]
		public void TestInitialize()
		{
			_db = new AlicargoDataContext(Settings.Default.MainConnectionString);
			_context = new WebTestContext(Settings.Default.BaseAddress, Settings.Default.AdminLogin, Settings.Default.AdminPassword);
			_client = _context.HttpClient;
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Edit()
		{
			var entity = _db.AirWaybills.First();

			var model = _context
				.Build<AwbAdminModel>()
				.With(x => x.BrokerId, entity.BrokerId)
				.With(x => x.DateOfArrivalLocalString, DateTimeOffset.UtcNow.ToLocalShortDateString())
				.With(x => x.DateOfDepartureLocalString, DateTimeOffset.UtcNow.ToLocalShortDateString())
				.Create();

			_client.PostAsJsonAsync("AirWaybill/Edit/", new { entity.Id, model })
				.ContinueWith(task =>
				{
					Assert.AreEqual(HttpStatusCode.OK, task.Result.StatusCode);

					_db.Refresh(RefreshMode.OverwriteCurrentValues, entity);

					var actual = Map(entity);

					model.ShouldBeEquivalentTo(actual);
				})
				.Wait();
		}

		private static AwbAdminModel Map(AirWaybill entity)
		{
			var actual = ObjectMapperManager.DefaultInstance.GetMapper<AirWaybill, AwbAdminModel>().Map(entity);

			actual.DateOfDepartureLocalString = entity.DateOfDeparture.ToLocalShortDateString();
			actual.DateOfArrivalLocalString = entity.DateOfArrival.ToLocalShortDateString();
			actual.GTDFile = entity.GTDFileData.ToArray();
			actual.GTDAdditionalFile = entity.GTDAdditionalFileData.ToArray();
			actual.PackingFile = entity.PackingFileData.ToArray();
			actual.InvoiceFile = entity.InvoiceFileData.ToArray();
			actual.AWBFile = entity.AWBFileData.ToArray();

			return actual;
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Create()
		{
			var broker = _db.Brokers.First();
			var applicationData = _db.Applications.First(x => !x.AirWaybillId.HasValue);

			var count = _db.AirWaybills.Count();

			var model = _context
				.Build<AwbAdminModel>()
				.Without(x => x.GTD)
				.With(x => x.BrokerId, broker.Id)
				.With(x => x.DateOfArrivalLocalString, DateTimeOffset.UtcNow.ToLocalShortDateString())
				.With(x => x.DateOfDepartureLocalString, DateTimeOffset.UtcNow.ToLocalShortDateString())
				.Create();

			_client.PostAsJsonAsync("AirWaybill/Create/" + applicationData.Id, model)
				.ContinueWith(task =>
				{
					if (task.Result.StatusCode != HttpStatusCode.OK) { Console.WriteLine(task.Result.Content.ReadAsStringAsync().Result); }
					Assert.AreEqual(HttpStatusCode.OK, task.Result.StatusCode);

					var entity = _db.AirWaybills.Skip(count).Take(1).First();

					var actual = Map(entity);

					model.ShouldBeEquivalentTo(actual);

					_db.Refresh(RefreshMode.OverwriteCurrentValues, applicationData);
					Assert.AreEqual(FirstStateId, applicationData.StateId);

					applicationData.AirWaybillId = null;
					_db.AirWaybills.DeleteOnSubmit(entity);
					_db.SubmitChanges();
				})
				.Wait();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_SetAirWaybill()
		{
			var application = _db.Applications.First(x => !x.AirWaybillId.HasValue);
			var airWaybill = _db.AirWaybills.First();

			_client.PostAsync("AirWaybill/SetAirWaybill/", new FormUrlEncodedContent(new Dictionary<string, string>
			{
				{"applicationId", application.Id.ToString(CultureInfo.InvariantCulture)},
				{"AirWaybillId", airWaybill.Id.ToString(CultureInfo.InvariantCulture)}
			})).ContinueWith(task =>
			{
				Assert.AreEqual(HttpStatusCode.OK, task.Result.StatusCode);

				_db.Refresh(RefreshMode.OverwriteCurrentValues, application);

				Assert.AreEqual(airWaybill.Id, application.AirWaybillId);
				application.AirWaybillId = null;
				_db.SubmitChanges();
			}).Wait();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_SetState()
		{
			var entity = _db.AirWaybills.FirstOrDefault(
				x => x.Applications.Count() > 1 && x.Applications.All(y => y.State.Id != DefaultStateId));
			if (entity == null)
				Assert.Inconclusive("Cant find AirWaybill for test");

			var oldStateId = entity.Applications.First().StateId;

			_client.PostAsync("AirWaybill/SetState/", new FormUrlEncodedContent(new Dictionary<string, string>
			{
				{"stateId", DefaultStateId.ToString(CultureInfo.InvariantCulture)},
				{"id", entity.Id.ToString(CultureInfo.InvariantCulture)}
			})).ContinueWith(task =>
			{
				Assert.AreEqual(HttpStatusCode.OK, task.Result.StatusCode);

				_db.Refresh(RefreshMode.OverwriteCurrentValues, entity);
				foreach (var application in entity.Applications)
				{
					_db.Refresh(RefreshMode.OverwriteCurrentValues, application);
				}

				Assert.IsTrue(entity.Applications.All(x => x.StateId == DefaultStateId));

				foreach (var application in entity.Applications)
				{
					application.StateId = oldStateId;
				}
				entity.StateId = oldStateId;

				_db.SubmitChanges();
			}).Wait();
		}
	}
}
