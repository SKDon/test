using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
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

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext();

			_calculationRepository = new CalculationRepository(_context.UnitOfWork);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_UpdateConflict()
		{
			var data = GenerateData();

			var versionedData = AddNew(data, TestConstants.TestApplicationId);

			var result = _calculationRepository.SetState(versionedData.Version.Id, versionedData.Version.RowVersion,
				CalculationState.New);

			result.Should().NotBeNull();
			result.RowVersion.SequenceEqual(versionedData.Version.RowVersion).Should().BeFalse();

			try
			{
				_calculationRepository.SetState(versionedData.Version.Id, versionedData.Version.RowVersion,
					CalculationState.New);
			}
			catch (EntityUpdateConflict)
			{
				_calculationRepository.Get(CalculationState.New)
									  .SingleOrDefault(x => x.Data.AirWaybillDisplay == data.AirWaybillDisplay)
									  .Should().NotBeNull();

				return;
			}

			Assert.Fail("Should not get here");
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

			var versionedData = AddNew(data, TestConstants.TestApplicationId);

			versionedData.Data.ShouldBeEquivalentTo(data);
			versionedData.Version.RowVersion.Should().NotBeNull();
			versionedData.Version.StateTimestamp.Should().NotBeNull();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_SetState()
		{
			var data = GenerateData();

			var versionedData = AddNew(data, TestConstants.TestApplicationId);

			var result = _calculationRepository.SetState(versionedData.Version.Id, versionedData.Version.RowVersion,
				CalculationState.Done);

			result.RowVersion.SequenceEqual(versionedData.Version.RowVersion).Should().BeFalse();
			result.StateTimestamp.Should().BeGreaterThan(versionedData.Version.StateTimestamp);
			result.Id.ShouldBeEquivalentTo(versionedData.Version.Id);
			result.State.ShouldBeEquivalentTo(CalculationState.Done);
		}

		private CalculationData GenerateData()
		{
			return _context.Fixture.Build<CalculationData>().With(x => x.ClientId, TestConstants.TestClientId1).Create();
		}

		private VersionedData<CalculationState, CalculationData> AddNew(CalculationData data, long applicationId)
		{
			_calculationRepository.Add(data, applicationId);
			_context.UnitOfWork.SaveChanges();

			return _calculationRepository.Get(CalculationState.New)
										 .Single(x => x.Data.AirWaybillDisplay == data.AirWaybillDisplay);
		}
	}
}