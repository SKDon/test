using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Models;
using Alicargo.Jobs.ApplicationEvents;
using Alicargo.Jobs.Entities;
using Alicargo.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Jobs.Tests
{
	[TestClass]
	public class MessageFactoryTests
	{
		private long _cargoAtCustomsStateId;
		private long _cargoIsCustomsClearedStateId;
		private long _cargoReceivedStateId;
		private MockContainer _container;
		private ApplicationDetailsData _details;
		private MessageFactory _factory;

		[TestInitialize]
		public void TestInitialize()
		{
			_container = new MockContainer();
			_cargoReceivedStateId = _container.Create<long>();
			_cargoAtCustomsStateId = _container.Create<long>();
			_cargoIsCustomsClearedStateId = _container.Create<long>();
			_container.StateConfig.SetupGet(x => x.CargoReceivedStateId).Returns(_cargoReceivedStateId);
			_container.StateConfig.SetupGet(x => x.CargoAtCustomsStateId).Returns(_cargoAtCustomsStateId);
			_container.StateConfig.SetupGet(x => x.CargoIsCustomsClearedStateId).Returns(_cargoIsCustomsClearedStateId);
			_details = _container.Create<ApplicationDetailsData>();
			_container.ApplicationRepository.Setup(x => x.GetDetails(It.IsAny<long>())).Returns(_details);

			_factory = _container.Create<MessageFactory>();
		}

		[TestMethod]
		public void Test_SetState_CargoReceivedStateId()
		{
			_container.Recipients.Setup(x => x.GetAdminEmails()).Returns(_container.CreateMany<Recipient>().ToArray());
			_container.Recipients.Setup(x => x.GetForwarderEmails()).Returns(_container.CreateMany<Recipient>().ToArray());
			Setup(_cargoReceivedStateId);

			_factory.Get(It.IsAny<long>(), ApplicationEventType.SetState, It.IsAny<byte[]>());

			_container.Recipients.Verify(x => x.GetAdminEmails(), Times.Once());
			_container.Recipients.Verify(x => x.GetForwarderEmails(), Times.Once());
		}

		private void SetupGetFiles()
		{
			_container.ApplicationFileRepository.Setup(x => x.GetInvoiceFile(It.IsAny<long>())).Returns(It.IsAny<FileHolder>());
			_container.ApplicationFileRepository.Setup(x => x.GetDeliveryBillFile(It.IsAny<long>()))
				.Returns(It.IsAny<FileHolder>());
			_container.ApplicationFileRepository.Setup(x => x.GetCPFile(It.IsAny<long>())).Returns(It.IsAny<FileHolder>());
			_container.ApplicationFileRepository.Setup(x => x.GetPackingFile(It.IsAny<long>())).Returns(It.IsAny<FileHolder>());
			_container.ApplicationFileRepository.Setup(x => x.GetSwiftFile(It.IsAny<long>())).Returns(It.IsAny<FileHolder>());
			_container.ApplicationFileRepository.Setup(x => x.GetTorg12File(It.IsAny<long>())).Returns(It.IsAny<FileHolder>());
			_container.AirWaybillRepository.Setup(x => x.GTDAdditionalFile(It.IsAny<long>())).Returns(It.IsAny<FileHolder>());
			_container.AirWaybillRepository.Setup(x => x.GetGTDFile(It.IsAny<long>())).Returns(It.IsAny<FileHolder>());
		}

		[TestMethod]
		public void Test_SetState_NotCargoReceivedStateId()
		{
			_container.Recipients.Setup(x => x.GetAdminEmails()).Returns(_container.CreateMany<Recipient>().ToArray());
			_container.Recipients.Setup(x => x.GetForwarderEmails()).Returns(_container.CreateMany<Recipient>().ToArray());
			Setup(_cargoReceivedStateId + 1);

			_factory.Get(It.IsAny<long>(), ApplicationEventType.SetState, It.IsAny<byte[]>());

			_container.ClientRepository.Verify(x => x.GetLanguage(_details.ClientId), Times.Once());
			_container.Recipients.Verify(x => x.GetAdminEmails(), Times.Never());
			_container.Recipients.Verify(x => x.GetForwarderEmails(), Times.Never());
		}

		[TestMethod]
		public void Test_SetState_CargoAtCustomsStateId()
		{
			_container.Recipients.Setup(x => x.GetAdminEmails()).Returns(_container.CreateMany<Recipient>().ToArray());
			_container.Recipients.Setup(x => x.GetForwarderEmails()).Returns(_container.CreateMany<Recipient>().ToArray());
			Setup(_cargoAtCustomsStateId);

			_factory.Get(It.IsAny<long>(), ApplicationEventType.SetState, It.IsAny<byte[]>());

			_container.ClientRepository.Verify(x => x.GetLanguage(_details.ClientId), Times.Once());
			_container.Recipients.Verify(x => x.GetAdminEmails(), Times.Once());
			_container.Recipients.Verify(x => x.GetForwarderEmails(), Times.Once());
		}

		[TestMethod]
		public void Test_SetState_CargoIsCustomsClearedStateId()
		{
			_container.Recipients.Setup(x => x.GetAdminEmails()).Returns(_container.CreateMany<Recipient>().ToArray());
			_container.Recipients.Setup(x => x.GetForwarderEmails()).Returns(_container.CreateMany<Recipient>().ToArray());
			Setup(_cargoIsCustomsClearedStateId);

			_factory.Get(It.IsAny<long>(), ApplicationEventType.SetState, It.IsAny<byte[]>());

			_container.ClientRepository.Verify(x => x.GetLanguage(_details.ClientId), Times.Once());
			_container.Recipients.Verify(x => x.GetAdminEmails(), Times.Once());
			_container.Recipients.Verify(x => x.GetForwarderEmails(), Times.Once());
		}

		private void Setup(long stateId)
		{
			_container.Serializer.Setup(x => x.Deserialize<ApplicationSetStateEventData>(It.IsAny<byte[]>()))
				.Returns(new ApplicationSetStateEventData
				{
					Timestamp = DateTimeOffset.UtcNow,
					StateId = stateId
				});
			_container.ClientRepository.Setup(x => x.GetLanguage(_details.ClientId)).Returns(TwoLetterISOLanguageName.English);
			var stateData = new StateData(new Dictionary<string, string>
			{
				{TwoLetterISOLanguageName.English, "English"},
				{TwoLetterISOLanguageName.Italian, "Italian"},
				{TwoLetterISOLanguageName.Russian, "Russian"},
			}) { Name = "English" };

			_container.StateRepository.Setup(x => x.Get(stateId)).Returns(new Dictionary<long, StateData>
			{
				{
					stateId, stateData
				}
			});
			SetupGetFiles();
		}
	}
}