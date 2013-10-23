using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
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

            container.IdentityService.Setup(x => x.IsInRole(RoleType.Admin)).Returns(false);
            container.IdentityService.Setup(x => x.IsInRole(RoleType.Sender)).Returns(false);
            container.IdentityService.Setup(x => x.Id).Returns(data.UserId);

            permissions.HaveAccessToClient(data).Should().BeTrue();
        }

        [TestMethod]
        public void Test_HaveAccessToClient_ByIdentity_False()
        {
            var container = new MockContainer();
            var permissions = container.Create<ClientPermissions>();
            var data = container.Create<ClientData>();

            container.IdentityService.Setup(x => x.IsInRole(RoleType.Admin)).Returns(false);
            container.IdentityService.Setup(x => x.IsInRole(RoleType.Sender)).Returns(false);
            container.IdentityService.Setup(x => x.Id).Returns(data.UserId + 1);

            permissions.HaveAccessToClient(data).Should().BeFalse();
        }
    }
}