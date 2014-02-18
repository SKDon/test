using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Services.Application;
using Alicargo.ViewModels.Application;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace Alicargo.Tests.Services.Application
{
	[TestClass]
	public class ApplicationGrouperTests
	{
		private const int AppCount = 99;
		private const int ClientCount = 6;
		private const int AWBCount = 9;
		private ApplicationListItem[] _applications;
		private long[] _awbIds;
		private Mock<IAwbRepository> _awbRepository;
		private AirWaybillData[] _awbsData;
		private Fixture _fixture;
		private ApplicationGrouper _grouper;

		[TestInitialize]
		public void TestInitialize()
		{
			_fixture = new Fixture();
			_awbRepository = new Mock<IAwbRepository>(MockBehavior.Strict);
			_awbIds = Enumerable.Range(0, AWBCount).Select(x => (long)x).ToArray();
			_awbsData = _awbIds.Select(i => _fixture.Build<AirWaybillData>()
				.With(d => d.Id, i)
				.Create()).ToArray();
			_awbRepository.Setup(x => x.Get(_awbIds)).Returns(_awbsData);

			_applications = _fixture.CreateMany<ApplicationListItem>(AppCount).ToArray();
			for(var i = 0; i < AppCount; i++)
			{
				var item = _applications[i];
				item.AirWaybillId = i % AWBCount;
				item.Id = i;
				item.ClientNic = "Client " + i % ClientCount;
				item.Count = i;
				item.Weight = i;
				item.Value = i;
				item.Volume = i;
			}

			_grouper = new ApplicationGrouper(_awbRepository.Object);
		}

		[TestMethod]
		public void Test_ApplicationGrouper_GroupWithAirWaybill()
		{
			_awbRepository.Setup(x => x.GetAggregate(_awbIds, null, null, null, null))
				.Returns(_awbIds.Select(x => new AirWaybillAggregate
				{
					AirWaybillId = x,
					StateId = 0,
					TotalCount = (int)x,
					TotalWeight = x,
					TotalValue = x,
					TotalVolume = x
				}).ToArray());
			_awbRepository.Setup(x => x.GetTotalCountWithouAwb(null, null, null, null)).Returns(0);
			_awbRepository.Setup(x => x.GetTotalWeightWithouAwb(null, null, null, null)).Returns(0);
			_awbRepository.Setup(x => x.GetTotalValueWithouAwb(null, null, null, null)).Returns(0);
			_awbRepository.Setup(x => x.GetTotalVolumeWithouAwb(null, null, null, null)).Returns(0);

			var groups = _grouper.Group(_applications, new[]
			{
				OrderType.AirWaybill,
				OrderType.Client
			});

			groups.Count().ShouldBeEquivalentTo(AWBCount);

			for(var index = 0; index < groups.Length; index++)
			{
				var @group = groups[index];
				@group.aggregates.Count.sum.ShouldBeEquivalentTo(index);
				@group.aggregates.Weight.sum.ShouldBeEquivalentTo(index);
				@group.aggregates.Value.sum.ShouldBeEquivalentTo(index);
				@group.aggregates.Volume.sum.ShouldBeEquivalentTo(index);
				@group.hasSubgroups.ShouldBeEquivalentTo(true);
				foreach(ApplicationGroup item in @group.items)
				{
					item.hasSubgroups.ShouldBeEquivalentTo(false);
					item.items.Count()
						.Should().BeLessOrEqualTo(ClientCount)
						.And.BeGreaterOrEqualTo(ClientCount - 1);
				}
			}
		}

		[TestMethod]
		public void Test_ApplicationGrouper_GroupWithoutAirWaybill()
		{
			var count = _fixture.Create<int>();
			var weidht = _fixture.Create<float>();
			var value = _fixture.Create<decimal>();
			var volume = _fixture.Create<float>();
			_awbRepository.Setup(x => x.GetAggregate(_awbIds, null, null, null, null)).Returns(new AirWaybillAggregate[0]);
			_awbRepository.Setup(x => x.GetTotalCountWithouAwb(null, null, null, null)).Returns(count);
			_awbRepository.Setup(x => x.GetTotalWeightWithouAwb(null, null, null, null)).Returns(weidht);
			_awbRepository.Setup(x => x.GetTotalValueWithouAwb(null, null, null, null)).Returns(value);
			_awbRepository.Setup(x => x.GetTotalVolumeWithouAwb(null, null, null, null)).Returns(volume);

			var groups = _grouper.Group(_applications, new[]
			{
				OrderType.AirWaybill,
				OrderType.Client
			});

			groups.Count().ShouldBeEquivalentTo(AWBCount);

			foreach(var @group in groups)
			{
				@group.aggregates.Count.sum.ShouldBeEquivalentTo(count);
				@group.aggregates.Weight.sum.ShouldBeEquivalentTo(weidht);
				@group.aggregates.Value.sum.ShouldBeEquivalentTo(value);
				@group.aggregates.Volume.sum.ShouldBeEquivalentTo(volume);
				@group.hasSubgroups.ShouldBeEquivalentTo(true);
				foreach(ApplicationGroup item in @group.items)
				{
					item.hasSubgroups.ShouldBeEquivalentTo(false);
					item.items.Count()
						.Should().BeLessOrEqualTo(ClientCount)
						.And.BeGreaterOrEqualTo(ClientCount - 1);
				}
			}
		}
	}
}