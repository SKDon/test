using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Services.Users.Client;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Tests.Services.Client
{
    [TestClass]
    public class ClientPermissionsTests
    {
        [TestMethod]
        public void Test_HaveAccessToClient_Admin()
        {
            var container = new MockContainer();

            var permissions = container.Create<ClientPermissions>();

            container.IdentityService.Setup(x => x.IsInRole(RoleType.Admin)).Returns(true);            

            permissions.HaveAccessToClient(It.IsAny<ClientData>()).Should().BeTrue();
        }

        [TestMethod]
        public void Test_HaveAccessToClient_Sender()
        {
            var container = new MockContainer();
            var permissions = container.Create<ClientPermissions>();

            container.IdentityService.Setup(x => x.IsInRole(RoleType.Admin)).Returns(false);
            container.IdentityService.Setup(x => x.IsInRole(RoleType.Sender)).Returns(true);

            permissions.HaveAccessToClient(It.IsAny<ClientData>()).Should().BeTrue();
        }

        [TestMethod]
        public void Test_HaveAccessToClient_ByIdentity_True()
        {
            var container = new MockContainer();
            var data = container.Create<ClientData>();
            var permissions = container.Create<ClientPermissions>();
	        var userId = container.Create<long>();

	        container.IdentityService.Setup(x => x.IsInRole(RoleType.Admin)).Returns(false);
			container.IdentityService.Setup(x => x.IsInRole(RoleType.Manager)).Returns(false);
            container.IdentityService.Setup(x => x.IsInRole(RoleType.Sender)).Returns(false);
	        container.ClientRepository.Setup(x => x.GetByUserId(userId)).Returns(data);
            container.IdentityService.Setup(x => x.Id).Returns(userId);

            permissions.HaveAccessToClient(data).Should().BeTrue();
        }

        [TestMethod]
        public void Test_HaveAccessToClient_ByIdentity_False()
        {
            var container = new MockContainer();
            var permissions = container.Create<ClientPermissions>();
            var data = container.Create<ClientData>();
			var userId = container.Create<long>();

            container.IdentityService.Setup(x => x.IsInRole(RoleType.Admin)).Returns(false);
			container.IdentityService.Setup(x => x.IsInRole(RoleType.Manager)).Returns(false);
            container.IdentityService.Setup(x => x.IsInRole(RoleType.Sender)).Returns(false);
			container.ClientRepository.Setup(x => x.GetByUserId(userId)).Returns(container.Create<ClientData>());
			container.IdentityService.Setup(x => x.Id).Returns(userId);

            permissions.HaveAccessToClient(data).Should().BeFalse();
        }
    }
}