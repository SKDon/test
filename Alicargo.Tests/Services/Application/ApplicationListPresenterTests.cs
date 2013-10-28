using System.Linq;
using Alicargo.Contracts.Contracts;
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
			const long clientId = 1;
			var data = _context.CreateMany<ApplicationListItemData>().ToArray();
			var map = _context.CreateMany<ApplicationListItem>().ToArray();
			_context.StateService.Setup(x => x.GetVisibleStates()).Returns(states);
			_context.ApplicationListItemMapper.Setup(x => x.Map(data)).Returns(map);
			_context.ApplicationGrouper.Setup(x => x.Group(map, new[] { OrderType.LegalEntity }))
					.Returns(new ApplicationGroup[0]);
			_context.ApplicationRepository.Setup(x => x.Count(states, clientId, null)).Returns(It.IsAny<long>());
			_context.ApplicationRepository.Setup(
				x =>
				x.List(0, 0, states,
					   It.Is<Order[]>(y => y.Last().OrderType == OrderType.Id && y.Last().Desc),
					   clientId, null)).Returns(data);

			var collection = _service.List(0, 0, orders, clientId);

			collection.Data.Should().BeNull();
			collection.Groups.Should().NotBeNull();

			_context.StateService.Verify(x => x.GetVisibleStates(), Times.Once());
			_context.ApplicationListItemMapper.Verify(x => x.Map(data));
			_context.ApplicationGrouper.Verify(x => x.Group(map, new[] { OrderType.LegalEntity }), Times.Once());
			_context.ApplicationRepository.Verify(x => x.Count(states, clientId, null), Times.Once());
			_context.ApplicationRepository.Verify(x =>
				x.List(0, 0, states,
					   It.Is<Order[]>(y => y.Last().OrderType == OrderType.Id && y.Last().Desc),
					   clientId, null), Times.Once());
		}
	}
}