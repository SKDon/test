using System;
using Alicargo.Services.Client;
using Alicargo.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.Tests.Services.Client
{
    [TestClass]
    public class ClientPermissionsTests
    {
        [TestMethod]
        public void Test_HaveAccessToClient_Admin()
        {
            var container = new MockContainer();

            container.Create<ClientPermissions>();
        }
    }
}
