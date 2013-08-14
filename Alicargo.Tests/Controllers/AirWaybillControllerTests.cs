//using System.Collections.Generic;
//using System.Data.Linq;
//using System.Globalization;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using Alicargo.DataAccess.DbContext;
//using Alicargo.TestHelpers;
//using Alicargo.Tests.Properties;
//using Alicargo.ViewModels;
//using FluentAssertions;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Ploeh.AutoFixture;

//namespace Alicargo.Tests.Controllers
//{
//	[TestClass]
//	public class AirWaybillControllerTests
//	{
//		private const long FirstStateId = 7;
//		private const long DefaultStateId = 1;

//		private HttpClient _client;
//		private AlicargoDataContext _db;		
//		private WebTestContext _context;

//		[TestInitialize]
//		public void TestInitialize()
//		{
//			_db = new AlicargoDataContext(Settings.Default.MainConnectionString);
//			_context = new WebTestContext(Settings.Default.BaseAddress, Settings.Default.AdminLogin, Settings.Default.AdminPassword);
//			_client = _context.HttpClient;
//		}

//		[TestMethod, TestCategory("black-box")]
//		public void Test_Edit()
//		{
//			var AirWaybill = _db.AirWaybills.First();

//			var model = _context
//				.Build<AirWaybillEditModel>()
//				.With(x => x.Id, AirWaybill.Id)
//				.With(x => x.BrockerId, AirWaybill.BrockerId)
//				.With(x => x.StateId, AirWaybill.StateId)
//				.With(x => x.StateChangeTimestamp, AirWaybill.StateChangeTimestamp)
//				.Without(x => x.DateOfArrivalLocalString)
//				.Without(x => x.DateOfDepartureLocalString)
//				.Without(x => x.TotalCount)
//				.Without(x => x.TotalWeight)
//				.Without(x => x.State)
//				.Without(x => x.CreationTimestamp)
//				.Create();

//			_client.PostAsJsonAsync("AirWaybill/Edit/", model)
//				.ContinueWith(task =>
//				{
//					Assert.AreEqual(HttpStatusCode.OK, task.Result.StatusCode);

//					_db.Refresh(RefreshMode.OverwriteCurrentValues, AirWaybill);

//					model.CreationTimestamp = AirWaybill.CreationTimestamp;
//					model.StateChangeTimestamp = AirWaybill.StateChangeTimestamp;
//					model.StateId = AirWaybill.StateId;

//					var actual = new AirWaybillEditModel(AirWaybill)
//					{
//						AWBFile = AirWaybill.AWBFileData.ToArray(),
//						GTDFile = AirWaybill.GTDFileData.ToArray(),
//						GTDAdditionalFile = AirWaybill.GTDAdditionalFileData.ToArray(),
//						PackingFile = AirWaybill.PackingFileData.ToArray(),
//						InvoiceFile = AirWaybill.InvoiceFileData.ToArray(),
//					};

//					model.ShouldBeEquivalentTo(actual);
//				})
//				.Wait();
//		}

//		[TestMethod, TestCategory("black-box")]
//		public void Test_Create()
//		{
//			var brocker = _db.Brockers.First();
//			var applicationData = _db.Applications.First(x => !x.AirWaybillId.HasValue);

//			var count = _db.AirWaybills.Count();
//			var state = _db.States.First(x => x.Id == DefaultStateId);

//			var model = _context
//				.Build<AirWaybillEditModel>()
//				.With(x => x.Id, 0)
//				.With(x => x.BrockerId, brocker.Id)
//				.With(x => x.StateId, state.Id)
//				.Without(x => x.DateOfArrivalLocalString)
//				.Without(x => x.DateOfDepartureLocalString)
//				.Without(x => x.TotalCount)
//				.Without(x => x.TotalWeight)
//				.Without(x => x.State)
//				.Without(x => x.CreationTimestamp)
//				.Create();

//			_client.PostAsJsonAsync("AirWaybill/Create/" + applicationData.Id, model)
//				.ContinueWith(task =>
//				{
//					//Console.WriteLine(task.Result.Content.ReadAsStringAsync().Result);
//					Assert.AreEqual(HttpStatusCode.OK, task.Result.StatusCode);

//					var AirWaybill = _db.AirWaybills.Skip(count).Take(1).First();

//					model.CreationTimestamp = AirWaybill.CreationTimestamp;
//					model.StateChangeTimestamp = AirWaybill.StateChangeTimestamp;
//					model.Id = AirWaybill.Id;
//					model.StateId = FirstStateId;

//					var actual = new AirWaybillEditModel(AirWaybill)
//					{
//						AWBFile = AirWaybill.AWBFileData.ToArray(),
//						GTDFile = AirWaybill.GTDFileData.ToArray(),
//						PackingFile = AirWaybill.PackingFileData.ToArray(),
//						InvoiceFile = AirWaybill.InvoiceFileData.ToArray(),
//						GTDAdditionalFile = AirWaybill.GTDAdditionalFileData.ToArray(),
//					};

//					model.ShouldBeEquivalentTo(actual);

//					_db.Refresh(RefreshMode.OverwriteCurrentValues, applicationData);
//					Assert.AreEqual(model.Id, applicationData.AirWaybillId);
//					Assert.AreEqual(FirstStateId, applicationData.StateId);

//					applicationData.AirWaybillId = null;
//					_db.AirWaybills.DeleteOnSubmit(AirWaybill);
//					_db.SubmitChanges();
//				})
//				.Wait();
//		}

//		[TestMethod, TestCategory("black-box")]
//		public void Test_SetAirWaybill()
//		{
//			var application = _db.Applications.First(x => !x.AirWaybillId.HasValue);
//			var AirWaybill = _db.AirWaybills.First();

//			_client.PostAsync("AirWaybill/SetAirWaybill/", new FormUrlEncodedContent(new Dictionary<string, string>
//			{
//				{"applicationId", application.Id.ToString(CultureInfo.InvariantCulture)},
//				{"AirWaybillId", AirWaybill.Id.ToString(CultureInfo.InvariantCulture)}
//			})).ContinueWith(task =>
//			{
//				Assert.AreEqual(HttpStatusCode.OK, task.Result.StatusCode);

//				_db.Refresh(RefreshMode.OverwriteCurrentValues, application);

//				Assert.AreEqual(AirWaybill.Id, application.AirWaybillId);
//				application.AirWaybillId = null;
//				_db.SubmitChanges();
//			}).Wait();
//		}

//		[TestMethod, TestCategory("black-box")]
//		public void Test_SetState()
//		{
//			var AirWaybill = _db.AirWaybills.FirstOrDefault(
//				x => x.Applications.Count() > 1 && x.Applications.All(y => y.State.Id != DefaultStateId));
//			if (AirWaybill == null)
//				Assert.Inconclusive("Cant find AirWaybill for test");

//			var oldStateId = AirWaybill.Applications.First().StateId;

//			_client.PostAsync("AirWaybill/SetState/", new FormUrlEncodedContent(new Dictionary<string, string>
//			{
//				{"stateId", DefaultStateId.ToString(CultureInfo.InvariantCulture)},
//				{"id", AirWaybill.Id.ToString(CultureInfo.InvariantCulture)}
//			})).ContinueWith(task =>
//			{
//				Assert.AreEqual(HttpStatusCode.OK, task.Result.StatusCode);

//				_db.Refresh(RefreshMode.OverwriteCurrentValues, AirWaybill);
//				foreach (var application in AirWaybill.Applications)
//				{
//					_db.Refresh(RefreshMode.OverwriteCurrentValues, application);
//				}

//				Assert.IsTrue(AirWaybill.Applications.All(x => x.StateId == DefaultStateId));

//				foreach (var application in AirWaybill.Applications)
//				{
//					application.StateId = oldStateId;
//				}
//				AirWaybill.StateId = oldStateId;

//				_db.SubmitChanges();
//			}).Wait();
//		}
//	}
//}
