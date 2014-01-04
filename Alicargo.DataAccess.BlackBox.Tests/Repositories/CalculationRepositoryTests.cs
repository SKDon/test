using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.DataAccess.BlackBox.Tests.Helpers;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.DbContext;
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

			_calculationRepository = new CalculationRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
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
		public void Test_Add_GetByApplication()
		{
			var data = GenerateData();

			var actual = AddNew(data, TestConstants.TestApplicationId);

			actual.ShouldBeEquivalentTo(data);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Add_GetByClient()
		{
			var data = GenerateData();

			_calculationRepository.Add(data, TestConstants.TestApplicationId);

			var actual = _calculationRepository.GetByClient(data.ClientId)
				.Single(x => x.ApplicationDisplay == data.ApplicationDisplay);

			actual.ShouldBeEquivalentTo(data);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_RemoveByApplication()
		{
			var data = GenerateData();

			AddNew(data, TestConstants.TestApplicationId).Should().NotBeNull();

			_calculationRepository.RemoveByApplication(TestConstants.TestApplicationId);

			_calculationRepository.GetByApplication(TestConstants.TestApplicationId).Should().BeNull();
		}

		private CalculationData GenerateData()
		{
			return _fixture.Build<CalculationData>().With(x => x.ClientId, TestConstants.TestClientId1).Create();
		}

		private CalculationData AddNew(CalculationData data, long applicationId)
		{
			_calculationRepository.Add(data, applicationId);

			return _calculationRepository.GetByApplication(applicationId);
		}
	}
}