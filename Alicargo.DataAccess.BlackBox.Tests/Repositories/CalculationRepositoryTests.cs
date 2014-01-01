using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.DataAccess.BlackBox.Tests.Helpers;
using Alicargo.DataAccess.Repositories;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class CalculationRepositoryTests
	{
		private CalculationRepository _calculationRepository;
		private DbTestContext _context;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext();
			_fixture = new Fixture();

			_calculationRepository = new CalculationRepository(_context.UnitOfWork);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestCategory("black-box"), TestMethod, ExpectedException(typeof (DublicateException))]
		public void Test_Uniqueness()
		{
			var data1 = GenerateData();
			var data2 = GenerateData();
			data2.ClientId = data1.ClientId;

			var result = AddNew(data1, TestConstants.TestApplicationId);
			result.Should().NotBeNull();

			AddNew(data2, TestConstants.TestApplicationId);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_AddGet()
		{
			var data = GenerateData();

			var actual = AddNew(data, TestConstants.TestApplicationId);

			actual.ShouldBeEquivalentTo(data);
		}

		private CalculationData GenerateData()
		{
			return _fixture.Build<CalculationData>().With(x => x.ClientId, TestConstants.TestClientId1).Create();
		}

		private CalculationData AddNew(CalculationData data, long applicationId)
		{
			_calculationRepository.Add(data, applicationId);
			_context.UnitOfWork.SaveChanges();

			return _calculationRepository.GetByClientId(data.ClientId)
				.Single(x => x.AirWaybillDisplay == data.AirWaybillDisplay);
		}
	}
}