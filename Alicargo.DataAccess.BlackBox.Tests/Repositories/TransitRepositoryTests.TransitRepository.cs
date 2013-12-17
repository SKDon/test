using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.DataAccess.BlackBox.Tests.Helpers;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class TransitRepositoryTests
	{
		private DbTestContext _context;
		private Fixture _fixture;
		private TransitRepository _transitRepository;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext();
			_fixture = new Fixture();

			_transitRepository = new TransitRepository(_context.UnitOfWork);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_TransitRepository_Add_Get()
		{
			var transit = CreateTestTransit();

			var actual = _transitRepository.Get(transit.Id).First();

			transit.ShouldBeEquivalentTo(actual);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_TransitRepository_Update()
		{
			var oldData = CreateTestTransit();

			var newData = _fixture.Create<TransitData>();
			newData.CarrierId = oldData.CarrierId;
			newData.Id = oldData.Id;
			_transitRepository.Update(newData);
			_context.UnitOfWork.SaveChanges();
			var actual = _transitRepository.Get(newData.Id).First();

			oldData.Should().NotBeSameAs(actual);
			newData.ShouldBeEquivalentTo(actual);
		}

		private TransitData CreateTestTransit()
		{
			var db = (AlicargoDataContext) _context.UnitOfWork.Context;

			var carrier = _fixture.Build<Carrier>()
				.Without(x => x.Transits)
				.Create();
			db.Carriers.InsertOnSubmit(carrier);
			db.SubmitChanges();

			var transit = _fixture.Build<Transit>()
				.Without(x => x.Applications)
				.Without(x => x.Clients)
				.With(x => x.Carrier, carrier)
				.Create();
			db.Transits.InsertOnSubmit(transit);
			db.SubmitChanges();

			return new TransitData
			{
				City = transit.City,
				Address = transit.Address,
				RecipientName = transit.RecipientName,
				Phone = transit.Phone,
				MethodOfTransitId = transit.MethodOfTransitId,
				DeliveryTypeId = transit.DeliveryTypeId,
				CarrierId = transit.CarrierId,
				WarehouseWorkingTime = transit.WarehouseWorkingTime,
				Id = transit.Id
			};
		}
	}
}