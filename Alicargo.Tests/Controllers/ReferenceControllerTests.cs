using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using Alicargo.DataAccess.DbContext;
using Alicargo.Tests.Properties;
using Alicargo.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.Tests.Controllers
{
	[TestClass]
	public class ReferenceControllerTests
	{
		private HttpClient _client;
		private AlicargoDataContext _db;
		private const long FirstStateId = 7;
		private const long DefaultStateId = 1;
		private WebTestContext _context;

		[TestInitialize]
		public void TestInitialize()
		{
			_db = new AlicargoDataContext(Settings.Default.MainConnectionString);
			_context = new WebTestContext();
			_client = _context.HttpClient;			
		}

		[TestMethod]
		public void Test_Edit()
		{
			var reference = _db.References.First();

			var model = _context
				.Build<ReferenceModel>()
				.With(x => x.Id, reference.Id)
				.With(x => x.BrockerId, reference.BrockerId)
				.With(x => x.StateId, reference.StateId)
				.With(x => x.StateChangeTimestamp, reference.StateChangeTimestamp)
				.Without(x => x.DateOfArrivalLocalString)
				.Without(x => x.DateOfDepartureLocalString)
				.Without(x => x.TotalCount)
				.Without(x => x.TotalWeight)
				.Without(x => x.State)
				.Without(x => x.CreationTimestamp)
				.Create();

			_client.PostAsJsonAsync("Reference/Edit/", model)
				.ContinueWith(task =>
				{
					Assert.AreEqual(HttpStatusCode.OK, task.Result.StatusCode);

					_db.Refresh(RefreshMode.OverwriteCurrentValues, reference);

					model.CreationTimestamp = reference.CreationTimestamp;
					model.StateChangeTimestamp = reference.StateChangeTimestamp;
					model.StateId = reference.StateId;

					var actual = new ReferenceModel(reference)
					{
						AWBFile = reference.AWBFileData.ToArray(),
						GTDFile = reference.GTDFileData.ToArray(),
						GTDAdditionalFile = reference.GTDAdditionalFileData.ToArray(),
						PackingFile = reference.PackingFileData.ToArray(),
						InvoiceFile = reference.InvoiceFileData.ToArray(),
					};

					_context.AreEquals(model, actual);
				})
				.Wait();
		}

		[TestMethod]
		public void Test_Create()
		{
			var brocker = _db.Brockers.First();
			var applicationData = _db.Applications.First(x => !x.ReferenceId.HasValue);

			var count = _db.References.Count();
			var state = _db.States.First(x => x.Id == DefaultStateId);

			var model = _context
				.Build<ReferenceModel>()
				.With(x => x.Id, 0)
				.With(x => x.BrockerId, brocker.Id)
				.With(x => x.StateId, state.Id)
				.Without(x => x.DateOfArrivalLocalString)
				.Without(x => x.DateOfDepartureLocalString)
				.Without(x => x.TotalCount)
				.Without(x => x.TotalWeight)
				.Without(x => x.State)
				.Without(x => x.CreationTimestamp)
				.Create();

			_client.PostAsJsonAsync("Reference/Create/" + applicationData.Id, model)
				.ContinueWith(task =>
				{
					//Console.WriteLine(task.Result.Content.ReadAsStringAsync().Result);
					Assert.AreEqual(HttpStatusCode.OK, task.Result.StatusCode);

					var reference = _db.References.Skip(count).Take(1).First();

					model.CreationTimestamp = reference.CreationTimestamp;
					model.StateChangeTimestamp = reference.StateChangeTimestamp;
					model.Id = reference.Id;
					model.StateId = FirstStateId;

					var actual = new ReferenceModel(reference)
					{
						AWBFile = reference.AWBFileData.ToArray(),
						GTDFile = reference.GTDFileData.ToArray(),
						PackingFile = reference.PackingFileData.ToArray(),
						InvoiceFile = reference.InvoiceFileData.ToArray(),
						GTDAdditionalFile = reference.GTDAdditionalFileData.ToArray(),
					};

					_context.AreEquals(model, actual);

					_db.Refresh(RefreshMode.OverwriteCurrentValues, applicationData);
					Assert.AreEqual(model.Id, applicationData.ReferenceId);
					Assert.AreEqual(FirstStateId, applicationData.StateId);

					applicationData.ReferenceId = null;
					_db.References.DeleteOnSubmit(reference);
					_db.SubmitChanges();
				})
				.Wait();
		}

		[TestMethod]
		public void Test_SetReference()
		{
			var application = _db.Applications.First(x => !x.ReferenceId.HasValue);
			var reference = _db.References.First();

			_client.PostAsync("Reference/SetReference/", new FormUrlEncodedContent(new Dictionary<string, string>
			{
				{"applicationId", application.Id.ToString(CultureInfo.InvariantCulture)},
				{"referenceId", reference.Id.ToString(CultureInfo.InvariantCulture)}
			})).ContinueWith(task =>
			{
				Assert.AreEqual(HttpStatusCode.OK, task.Result.StatusCode);

				_db.Refresh(RefreshMode.OverwriteCurrentValues, application);

				Assert.AreEqual(reference.Id, application.ReferenceId);
				application.ReferenceId = null;
				_db.SubmitChanges();
			}).Wait();
		}

		[TestMethod]
		public void Test_SetState()
		{
			var reference = _db.References.FirstOrDefault(x => x.Applications.Count() > 1 && x.Applications.All(y => y.State.Id != DefaultStateId));
			if (reference == null)
				Assert.Inconclusive("Cant find reference for test");

			var oldStateId = reference.Applications.First().StateId;

			_client.PostAsync("Reference/SetState/", new FormUrlEncodedContent(new Dictionary<string, string>
			{
				{"stateId", DefaultStateId.ToString(CultureInfo.InvariantCulture)},
				{"id", reference.Id.ToString(CultureInfo.InvariantCulture)}
			})).ContinueWith(task =>
			{
				Assert.AreEqual(HttpStatusCode.OK, task.Result.StatusCode);

				_db.Refresh(RefreshMode.OverwriteCurrentValues, reference);
				foreach (var application in reference.Applications)
				{
					_db.Refresh(RefreshMode.OverwriteCurrentValues, application);
				}

				Assert.IsTrue(reference.Applications.All(x => x.StateId == DefaultStateId));

				foreach (var application in reference.Applications)
				{
					application.StateId = oldStateId;
				}
				reference.StateId = oldStateId;

				_db.SubmitChanges();
			}).Wait();
		}
	}
}
