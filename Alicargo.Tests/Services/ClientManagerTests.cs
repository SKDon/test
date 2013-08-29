using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.Services.Client;
using Alicargo.TestHelpers;
using Alicargo.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Tests.Services
{
    [TestClass]
    public class ClientManagerTests
    {
        [TestMethod, ExpectedException(typeof(AccessForbiddenException))]
        public void Test_Update_Not_HaveAccessToClient()
        {
            var container = new MockContainer();
            var clientId = container.Create<long>();
            var data = container.Create<ClientData>();

            container.ClientRepository.Setup(x => x.Get(clientId)).Returns(new[] { data });
            container.ClientPermissions.Setup(x => x.HaveAccessToClient(data)).Returns(false);

            var manager = new ClientManager(It.IsAny<IIdentityService>(), container.ClientRepository.Object,
                                            container.ClientPermissions.Object, It.IsAny<ITransitService>(),
                                            It.IsAny<IAuthenticationRepository>(), It.IsAny<IUnitOfWork>());

            manager.Update(clientId, It.IsAny<ClientModel>(), It.IsAny<CarrierSelectModel>(),
                           It.IsAny<TransitEditModel>(), It.IsAny<AuthenticationModel>());
        }
    }
}