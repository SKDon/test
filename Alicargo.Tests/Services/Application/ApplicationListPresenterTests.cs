using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.DataAccess.Repositories;
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
		private Order[] _orders;
		private IApplicationListPresenter _service;
		private long[] _stateIds;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new MockContainer();
			_service = _context.Create<ApplicationListPresenter>();
			_orders = new[]
			{
				new Order
				{
					OrderType = OrderType.LegalEntity,
					Desc = false
				}
			};
			_stateIds = new[] { 0L };
		}

		[TestMethod]
		public void Test_List()
		{
			const long clientId = 1;
			var data = _context.CreateMany<ApplicationListItemData>().ToArray();
			var map = _context.CreateMany<ApplicationListItem>().ToArray();
			_context.StateService.Setup(x => x.GetVisibleStates()).Returns(_stateIds);
			_context.ApplicationListItemMapper.Setup(x => x.Map(data)).Returns(map);
			_context.ApplicationGrouper.Setup(x => x.Group(map, new[] { OrderType.LegalEntity }))
				.Returns(new ApplicationGroup[0]);
			_context.ApplicationRepository.Setup(x => x.Count(_stateIds, clientId, null, null, null, null)).Returns(It.IsAny<long>());
			_context.ApplicationRepository.Setup(x =>
					x.List(_stateIds, _orders, 0, 0, clientId, null, null, null, null))
				.Returns(data);

			var collection = _service.List(0, 0, _orders, clientId);

			collection.Data.Should().BeNull();
			collection.Groups.Should().NotBeNull();

			_context.StateService.Verify(x => x.GetVisibleStates(), Times.Once());
			_context.ApplicationListItemMapper.Verify(x => x.Map(data));
			_context.ApplicationGrouper.Verify(x => x.Group(map, new[] { OrderType.LegalEntity }), Times.Once());
			_context.ApplicationRepository.Verify(x => x.Count(_stateIds, clientId, null, null, null, null), Times.Once());
			_context.ApplicationRepository.Verify(x =>
				x.List(_stateIds, _orders, 0, 0, clientId, null, null, null, null),
				Times.Once());
		}
	}
}