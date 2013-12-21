using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.User;
using Alicargo.DataAccess.BlackBox.Tests.Helpers;
using Alicargo.DataAccess.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class CarrierRepositoryTests
	{
		private CarrierRepository _repository;
		private DbTestContext _context;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext();
			_fixture = new Fixture();
			_repository = new CarrierRepository(_context.UnitOfWork);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		private CarrierData CreateTestCarrier()
		{
			var data = _fixture.Create<CarrierData>();

			var id = _repository.Add(data);
			_context.UnitOfWork.SaveChanges();

			data.Id = id();

			return data;
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_CarrierRepository_Add_Get()
		{
			var carrier = CreateTestCarrier();

			var actual = _repository.Get(carrier.Name);

			actual.ShouldBeEquivalentTo(carrier);

			var carriers = _repository.GetAll();

			Assert.IsTrue(carriers.Any(x => x.Id == carrier.Id));
		}
	}
}
