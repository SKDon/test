using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Services.Abstract;
using Alicargo.Services.Application;
using Alicargo.TestHelpers;
using Alicargo.ViewModels.Application;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace Alicargo.Tests.Services.Application
{
    [TestClass]
    public class ApplicationListPresenterTests
    {
        private MockContainer _context;
        private IApplicationListPresenter _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = new MockContainer();

            _service = _context.Create<ApplicationListPresenter>();
        }

        [TestMethod]
        public void Test_List()
        {
            var states = new[] { 0L };
            var orders = new[]
                {
                    new Order
                        {
                            OrderType = OrderType.LegalEntity,
                            Desc = false
                        }
                };
            const long identity = 1;
            var data = _context.CreateMany<ApplicationListItemData>().ToArray();
            var map = _context.CreateMany<ApplicationListItem>().ToArray();
            _context.IdentityService.SetupGet(x => x.Id).Returns(identity);
            _context.StateService.Setup(x => x.GetVisibleStates()).Returns(states);
            _context.IdentityService.Setup(x => x.IsInRole(RoleType.Client)).Returns(true);
            _context.ApplicationListItemMapper.Setup(x => x.Map(data)).Returns(map);
            _context.ApplicationGrouper.Setup(x => x.Group(map, new[] { OrderType.LegalEntity }))
                    .Returns(new ApplicationGroup[0]);
            _context.ApplicationRepository.Setup(x => x.Count(states, identity)).Returns(It.IsAny<long>());
            _context.ApplicationRepository.Setup(
                x =>
                x.List(0, 0, states,
                       It.Is<Order[]>(y => y.Last().OrderType == OrderType.Id && y.Last().Desc),
                       identity)).Returns(data);

            var collection = _service.List(0, 0, orders);

            collection.Data.Should().BeNull();
            collection.Groups.Should().NotBeNull();

            _context.IdentityService.Verify(x => x.Id);
            _context.StateService.Verify(x => x.GetVisibleStates(), Times.Once());
            _context.IdentityService.Verify(x => x.IsInRole(RoleType.Client));
            _context.ApplicationListItemMapper.Verify(x => x.Map(data));
            _context.ApplicationGrouper.Verify(x => x.Group(map, new[] { OrderType.LegalEntity }), Times.Once());
            _context.ApplicationRepository.Verify(x => x.Count(states, identity), Times.Once());
            _context.ApplicationRepository.Verify(x =>
                x.List(0, 0, states,
                       It.Is<Order[]>(y => y.Last().OrderType == OrderType.Id && y.Last().Desc),
                       identity), Times.Once());
        }
    }
}