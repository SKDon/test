using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Contracts;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.Tests.Repositories
{
	public partial class Tests
	{
		[TestMethod]
		public void Test_ApplicationRepository_Add_Get()
		{
			var expected = CreateTestApplication();

			var actual = _applicationRepository.Get(expected.Id);

			Assert.IsNotNull(actual);

			expected.ShouldBeEquivalentTo(actual);
		}

		[TestMethod]
		public void Test_ApplicationRepository_Count()
		{
			var defaultState = _stateRepository.Get(DefaultStateId);
			var count = _applicationRepository.Count(new[] { defaultState.Id });

			CreateTestApplication(defaultState);

			var newCount = _applicationRepository.Count(new[] { defaultState.Id });

			Assert.AreEqual(newCount, count + 1);
		}

		[TestMethod]
		public void Test_ApplicationRepository_UpdateState()
		{
			var defaultState = _stateRepository.Get(DefaultStateId);
			var application = CreateTestApplication(defaultState);
			var state = _stateRepository.GetAll().First(x => x.Id != defaultState.Id);

			_applicationRepository.SetState(application.Id, state.Id);
			_unitOfWork.SaveChanges();

			var actual = _applicationRepository.Get(application.Id);

			Assert.AreEqual(state.Id, actual.StateId);
		}

		[TestMethod]
		public void Test_ApplicationRepository_Update()
		{
			var old = CreateTestApplication();

			var newData = _fixture.Create<ApplicationData>();
			newData.Id = old.Id;
			newData.StateId = old.StateId;
			newData.ClientId = old.ClientId;
			newData.TransitId = old.TransitId;
			newData.ReferenceId = old.ReferenceId;
			newData.CreationTimestamp = old.CreationTimestamp;
			newData.StateChangeTimestamp = old.StateChangeTimestamp;

			var swiftFile = RandomBytes();
			var invoiceFile = RandomBytes();
			var cpFile = RandomBytes();
			var deliveryBillFile = RandomBytes();
			var torg12File = RandomBytes();
			var packingFile = RandomBytes();

			_applicationRepository.Update(newData, swiftFile, invoiceFile, cpFile, deliveryBillFile, torg12File, packingFile);
			_unitOfWork.SaveChanges();

			var data = _applicationRepository.Get(old.Id);

			_applicationRepository.GetInvoiceFile(data.Id).FileData.ShouldBeEquivalentTo(invoiceFile);
			_applicationRepository.GetSwiftFile(data.Id).FileData.ShouldBeEquivalentTo(swiftFile);
			_applicationRepository.GetCPFile(data.Id).FileData.ShouldBeEquivalentTo(cpFile);
			_applicationRepository.GetDeliveryBillFile(data.Id).FileData.ShouldBeEquivalentTo(deliveryBillFile);
			_applicationRepository.GetTorg12File(data.Id).FileData.ShouldBeEquivalentTo(torg12File);
			_applicationRepository.GetPackingFile(data.Id).FileData.ShouldBeEquivalentTo(packingFile);
			data.ShouldBeEquivalentTo(newData);
		}

		private ApplicationData CreateTestApplication(StateData state = null)
		{
			var client = CreateTestClient();
			var transit = CreateTestTransit();

			if (state == null)
			{
				state = _stateRepository.Get(DefaultStateId);
			}

			var application = _fixture
				.Build<ApplicationData>()
				.With(x => x.ClientId, client.Id)
				.With(x => x.TransitId, transit.Id)
				.With(x => x.StateId, state.Id)
				.With(x => x.ReferenceId, null)
				//.With(x => x.InvoiceFileName, null)
				//.With(x => x.SwiftFileName, null)
				//.With(x => x.Torg12FileName, null)
				//.With(x => x.DeliveryBillFileName, null)
				//.With(x => x.CPFileName, null)
				.Create();

			var id = _applicationRepository.Add(application, _fixture.CreateMany<byte>().ToArray(),
				_fixture.CreateMany<byte>().ToArray(), _fixture.CreateMany<byte>().ToArray(), _fixture.CreateMany<byte>().ToArray(),
				_fixture.CreateMany<byte>().ToArray(), _fixture.CreateMany<byte>().ToArray());
			_unitOfWork.SaveChanges();

			application.Id = id();

			return application;
		}
	}
}
