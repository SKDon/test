using Alicargo.Contracts.Contracts;
using Alicargo.Services.Contract;
using Alicargo.Services.Email;
using Alicargo.TestHelpers;
using Alicargo.ViewModels.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Tests.Services.Email
{
    [TestClass]
    public class ApplicationManagerWithMailingTests
    {
        private MockContainer _container;
        private long _cargoReceivedStateId;
        private ApplicationManagerWithMailing _manager;
        private long _cargoAtCustomsStateId;
        private ApplicationDetailsModel _details;
        private long _cargoIsCustomsClearedStateId;

        [TestInitialize]
        public void TestInitialize()
        {
            _container = new MockContainer(MockBehavior.Default);
            _cargoReceivedStateId = _container.Create<long>();
            _cargoAtCustomsStateId = _container.Create<long>();
            _cargoIsCustomsClearedStateId = _container.Create<long>();
            _container.StateConfig.SetupGet(x => x.CargoReceivedStateId).Returns(_cargoReceivedStateId);
            _container.StateConfig.SetupGet(x => x.CargoAtCustomsStateId).Returns(_cargoAtCustomsStateId);
            _container.StateConfig.SetupGet(x => x.CargoIsCustomsClearedStateId).Returns(_cargoIsCustomsClearedStateId);
            _details = _container.Create<ApplicationDetailsModel>();
            var authenticationData = _container.Create<AuthenticationData>();
            _container.ApplicationPresenter.Setup(x => x.GetDetails(It.IsAny<long>())).Returns(_details);
            _container.AuthenticationRepository.Setup(x => x.GetById(_details.ClientUserId)).Returns(authenticationData);

            _manager = _container.Create<ApplicationManagerWithMailing>();
        }

        [TestMethod]
        public void Test_SetState_CargoReceivedStateId()
        {
            _details.StateId = _cargoReceivedStateId;

            _manager.SetState(It.IsAny<long>(), It.IsAny<long>());

            _container.MessageBuilder.Verify(x => x.GetAdminEmails(), Times.Once());
            _container.MessageBuilder.Verify(x => x.GetForwarderEmails(), Times.Once());
            _container.ApplicationManager.Verify(x => x.SetState(It.IsAny<long>(), It.IsAny<long>()));
        }

        [TestMethod]
        public void Test_SetState_NotCargoReceivedStateId()
        {
            _details.StateId = _cargoReceivedStateId + 1;

            _manager.SetState(It.IsAny<long>(), It.IsAny<long>());

            _container.AuthenticationRepository.Verify(x => x.GetById(It.IsAny<long>()), Times.Once());
            _container.MailSender.Verify(x => x.Send(It.IsAny<Message[]>()));
            _container.MessageBuilder.Verify(x => x.GetAdminEmails(), Times.Never());
            _container.MessageBuilder.Verify(x => x.GetForwarderEmails(), Times.Never());
            _container.ApplicationManager.Verify(x => x.SetState(It.IsAny<long>(), It.IsAny<long>()));
        }

        [TestMethod]
        public void Test_SetState_CargoAtCustomsStateId()
        {
            _details.StateId = _cargoAtCustomsStateId;

            _manager.SetState(It.IsAny<long>(), It.IsAny<long>());

            _container.AuthenticationRepository.Verify(x => x.GetById(It.IsAny<long>()), Times.Once());
            _container.ApplicationManager.Verify(x => x.SetState(It.IsAny<long>(), It.IsAny<long>()));
            _container.MailSender.Verify(x => x.Send(It.IsAny<Message[]>()));
            _container.MessageBuilder.Verify(x => x.GetAdminEmails(), Times.Once());
            _container.MessageBuilder.Verify(x => x.GetForwarderEmails(), Times.Once());
        }

        [TestMethod]
        public void Test_SetState_CargoIsCustomsClearedStateId()
        {
            _details.StateId = _cargoIsCustomsClearedStateId;

            _manager.SetState(It.IsAny<long>(), It.IsAny<long>());

            _container.AuthenticationRepository.Verify(x => x.GetById(It.IsAny<long>()), Times.Once());
            _container.ApplicationManager.Verify(x => x.SetState(It.IsAny<long>(), It.IsAny<long>()));
            _container.MailSender.Verify(x => x.Send(It.IsAny<Message[]>()));
            _container.MessageBuilder.Verify(x => x.GetAdminEmails(), Times.Once());
            _container.MessageBuilder.Verify(x => x.GetForwarderEmails(), Times.Once());
        }
    }
}
