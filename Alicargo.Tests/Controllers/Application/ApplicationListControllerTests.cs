using System.Collections.Generic;
using System.Linq;
using Alicargo.Controllers.Application;
using Alicargo.DataAccess.Contracts.Contracts.Awb;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
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
				_context.AdminRepository.Object,
				_context.AwbRepository.Object,
				_context.CarrierRepository.Object,
				_context.StateConfig.Object,
				_context.IdentityService.Object,
				_context.ForwarderRepository.Object);
		}

		[TestMethod]
		public void Test_Index_AdminCreator()
		{
			var cargoIsFlewStateId = _context.Create<long>();
			var clients = _context.CreateMany<ClientData>().ToArray();
			var admins = _context.CreateMany<UserData>().ToArray();
			var userId = _context.Create<long>();
			var awbs = _context.Build<AirWaybillData>()
				.With(x => x.IsActive, true)
				.With(x => x.StateId, cargoIsFlewStateId)
				.CreateMany()
				.ToArray();
			awbs[0].CreatorUserId = admins[0].UserId;

			var model = Test(userId, clients, awbs, admins, cargoIsFlewStateId);

			model.AirWaybills.Should().HaveCount(1);
			model.AirWaybills[awbs[0].Id].ShouldBeEquivalentTo(awbs[0].Bill);
		}

		[TestMethod]
		public void Test_Index_IsActive()
		{
			var cargoIsFlewStateId = _context.Create<long>();
			var clients = _context.CreateMany<ClientData>().ToArray();
			var admins = _context.CreateMany<UserData>().ToArray();
			var userId = _context.Create<long>();
			var awbs = _context.Build<AirWaybillData>()
				.With(x => x.IsActive, false)
				.With(x => x.StateId, cargoIsFlewStateId)
				.With(x => x.CreatorUserId, userId)
				.CreateMany()
				.ToArray();

			var model = Test(userId, clients, awbs, admins, cargoIsFlewStateId);

			model.AirWaybills.Should().HaveCount(0);
		}

		[TestMethod]
		public void Test_Index_Sender()
		{
			var cargoIsFlewStateId = _context.Create<long>();
			var clients = _context.CreateMany<ClientData>().ToArray();
			var admins = _context.CreateMany<UserData>().ToArray();
			var awbs = _context.Build<AirWaybillData>()
				.With(x => x.IsActive, true)
				.With(x => x.StateId, cargoIsFlewStateId)
				.CreateMany()
				.ToArray();
			var userId = _context.Create<long>();
			awbs[0].CreatorUserId = userId;

			var model = Test(userId, clients, awbs, admins, cargoIsFlewStateId);

			model.AirWaybills.Should().HaveCount(1);
			model.AirWaybills[awbs[0].Id].ShouldBeEquivalentTo(awbs[0].Bill);
		}

		private ApplicationIndexModel Test(long userId, ClientData[] clients, AirWaybillData[] awbs, UserData[] admins, long cargoIsFlewStateId)
		{
			_context.IdentityService.SetupGet(x => x.Id).Returns(userId);
			_context.ClientRepository.Setup(x => x.GetAll()).Returns(clients);
			_context.IdentityService.Setup(x => x.IsInRole(RoleType.Sender)).Returns(true);
			_context.AwbRepository.Setup(x => x.Get()).Returns(awbs);
			_context.AdminRepository.Setup(x => x.GetAll()).Returns(admins);
			_context.StateConfig.SetupGet(x => x.CargoIsFlewStateId).Returns(cargoIsFlewStateId);

			var result = _applicationListController.Index();

			var model = (ApplicationIndexModel)result.Model;

			_context.IdentityService.Verify(x => x.Id, Times.Once());
			_context.ClientRepository.Verify(x => x.GetAll(), Times.Once());
			_context.IdentityService.Verify(x => x.IsInRole(RoleType.Sender), Times.Once());
			_context.AwbRepository.Verify(x => x.Get(), Times.Once());
			_context.AdminRepository.Verify(x => x.GetAll(), Times.Once());
			_context.StateConfig.Verify(x => x.CargoIsFlewStateId, Times.Once());
			model.Clients.ShouldAllBeEquivalentTo(clients.ToDictionary(x => x.ClientId, x => x.Nic));

			return model;
		}
	}
}