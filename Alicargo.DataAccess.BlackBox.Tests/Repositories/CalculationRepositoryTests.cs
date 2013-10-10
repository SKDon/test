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

		[TestMethod]
		public void Test_SetState()
		{
			var data = _context.Fixture.Build<CalculationData>()
							   .With(x => x.ClientId, TestConstants.TestClientId1)
							   .Create();

			_calculationRepository.Add(data, TestConstants.TestApplicationId);
			_context.UnitOfWork.SaveChanges();

			var versionedData = _calculationRepository.Get(CalculationState.New)
													  .Single(x => x.Data.AirWaybillDisplay == data.AirWaybillDisplay);

			versionedData.Data.ShouldBeEquivalentTo(data);
			versionedData.Version.RowVersion.Should().NotBeNull();
			versionedData.Version.StateTimestamp.Should().NotBeNull();

			var result = _calculationRepository.SetState(versionedData.Version.Id, versionedData.Version.RowVersion,
														 CalculationState.Done);

			result.RowVersion.SequenceEqual(versionedData.Version.RowVersion).Should().BeFalse();
			result.StateTimestamp.Should().BeGreaterThan(versionedData.Version.StateTimestamp);
			result.Id.ShouldBeEquivalentTo(versionedData.Version.Id);
			result.State.ShouldBeEquivalentTo(CalculationState.Done);

			try
			{
				_calculationRepository.SetState(versionedData.Version.Id, versionedData.Version.RowVersion,
												CalculationState.Sended);
			}
			catch (EntityUpdateConflict)
			{
				_calculationRepository.Get(CalculationState.Done)
									  .SingleOrDefault(x => x.Data.AirWaybillDisplay == data.AirWaybillDisplay)
									  .Should().NotBeNull();

				return;
			}

			Assert.Fail("Should not get here");
		}
	}
}