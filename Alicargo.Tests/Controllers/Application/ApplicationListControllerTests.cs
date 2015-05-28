using System.Linq;
using Alicargo.Controllers.Application;
using Alicargo.DataAccess.Contracts.Contracts.Awb;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.TestHelpers;
using Alicargo.ViewModels.Application;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace Alicargo.Tests.Controllers.Application
{
	[TestClass]
	public class ApplicationListControllerTests
	{
		private ApplicationListController _applicationListController;
		private MockContainer _context;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new MockContainer();
			_applicationListController = new ApplicationListController(
				_context.ApplicationListPresenter.Object,
				_context.ClientRepository.Object,
				_context.SenderRepository.Object,
				_context.AwbRepository.Object,
				_context.CarrierRepository.Object,
				_context.StateConfig.Object,
				_context.IdentityService.Object,
				_context.ForwarderRepository.Object);
		}

		[TestMethod]
		public void Test_Index_IsActive()
		{
			var cargoIsFlewStateId = _context.Create<long>();
			var clients = _context.Build<ClientData>()
				.With(x => x.DefaultSenderId, TestConstants.TestSenderId)
				.CreateMany()
				.ToArray();
			var userId = _context.Create<long>();
			var awbs = _context.Build<AirWaybillData>()
				.With(x => x.IsActive, false)
				.With(x => x.StateId, cargoIsFlewStateId)
				.With(x => x.CreatorUserId, userId)
				.CreateMany()
				.ToArray();

			var model = Test(clients, awbs, cargoIsFlewStateId);

			model.AirWaybills.Should().HaveCount(0);
		}

		[TestMethod]
		public void Test_Index_Sender()
		{
			var cargoIsFlewStateId = _context.Create<long>();
			var clients = _context.Build<ClientData>()
				.With(x => x.DefaultSenderId, TestConstants.TestSenderId)
				.CreateMany()
				.ToArray();
			var awbs = _context.Build<AirWaybillData>()
				.With(x => x.IsActive, true)
				.With(x => x.StateId, cargoIsFlewStateId)
				.CreateMany()
				.ToArray();
			awbs[0].SenderUserId = TestConstants.TestSenderUserId;

			var model = Test(clients, awbs, cargoIsFlewStateId);

			model.AirWaybills.Should().HaveCount(1);
		}

		private ApplicationIndexModel Test(ClientData[] clients, AirWaybillData[] awbs, long cargoIsFlewStateId)
		{
			_context.IdentityService.SetupGet(x => x.Id).Returns(TestConstants.TestSenderUserId);
			_context.ClientRepository.Setup(x => x.GetAll()).Returns(clients);
			_context.AwbRepository.Setup(x => x.Get()).Returns(awbs);
			_context.StateConfig.SetupGet(x => x.CargoIsFlewStateId).Returns(cargoIsFlewStateId);
			_context.SenderRepository.Setup(x => x.GetByUserId(TestConstants.TestSenderUserId))
				.Returns(TestConstants.TestSenderId);

			var result = _applicationListController.Index();

			var model = (ApplicationIndexModel)result.Model;

			_context.IdentityService.Verify(x => x.Id, Times.Once());
			_context.ClientRepository.Verify(x => x.GetAll(), Times.Once());
			_context.AwbRepository.Verify(x => x.Get(), Times.Once());
			_context.StateConfig.Verify(x => x.CargoIsFlewStateId, Times.Once());
			model.Clients.ShouldAllBeEquivalentTo(clients.ToDictionary(x => x.ClientId, x => x.Nic));
			_context.SenderRepository.Verify(x => x.GetByUserId(TestConstants.TestSenderUserId), Times.Once());

			return model;
		}
	}
}