using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
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
        [TestMethod]
        public void Test_ApplicationGrouper_Group()
        {
            var fixture = new Fixture();
            var awbRepository = new Mock<IAwbRepository>(MockBehavior.Strict);

            const int appCount = 99;
            const int clientCount = 6;
            const int awbCount = 9;

            var ids = Enumerable.Range(0, awbCount).Select(x => (long)x).ToArray();
            var datas = ids.Select(i => fixture.Build<AirWaybillData>()
                                               .With(d => d.Id, i)
                                               .Create()).ToArray();
            awbRepository.Setup(x => x.Get(ids)).Returns(datas);
            awbRepository.Setup(x => x.GetAggregate(ids))
                         .Returns(ids.Select(x => new AirWaybillAggregate
                             {
                                 AirWaybillId = x,
                                 StateId = 0,
                                 TotalCount = (int) x,
                                 TotalWeight = x
                             }).ToArray());

            var grouper = new ApplicationGrouper(awbRepository.Object);

            var applications = fixture.CreateMany<ApplicationListItem>(appCount).ToArray();
            for (var i = 0; i < appCount; i++)
            {
                var item = applications[i];
                item.AirWaybillId = i % awbCount;
                item.Id = i;
                item.ClientLegalEntity = "Client " + i % clientCount;
                item.Count = i;
                item.Weigth = i;
            }

            var groups = grouper.Group(applications, new[]
                {
                    OrderType.AirWaybill,
                    OrderType.LegalEntity
                });

            groups.Count().ShouldBeEquivalentTo(awbCount);

            for (int index = 0; index < groups.Length; index++)
            {
                var @group = groups[index];
                @group.aggregates.Count.sum.ShouldBeEquivalentTo(index);
                @group.aggregates.Weigth.sum.ShouldBeEquivalentTo(index);
                @group.hasSubgroups.ShouldBeEquivalentTo(true);
                foreach (ApplicationGroup item in @group.items)
                {
                    item.hasSubgroups.ShouldBeEquivalentTo(false);
                    item.items.Count()
                        .Should().BeLessOrEqualTo(clientCount)
                        .And.BeGreaterOrEqualTo(clientCount - 1);
                }
            }
        }
    }
}