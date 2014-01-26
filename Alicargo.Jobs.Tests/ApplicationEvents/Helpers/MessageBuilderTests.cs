using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Jobs.ApplicationEvents.Helpers;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Jobs.Tests.ApplicationEvents.Helpers
{
	[TestClass]
	public class MessageBuilderTests
	{
		private MockContainer _container;
		private MessageBuilder _builder;

		[TestInitialize]
		public void TestInitialize()
		{
			_container = new MockContainer();
			_builder = _container.Create<MessageBuilder>();
		}

		[TestMethod]
		public void Test_SetStateForNotClient()
		{
			const EventType eventType = EventType.ApplicationSetState;
			var applicationDetailsData = _container.Create<ApplicationExtendedData>();
			var eventDataForEntity = _container.Create<EventDataForEntity>();
			var eventData = _container.Create<EventData>();
			var recipientData = new RecipientData(_container.Create<string>(), _container.Create<string>(), RoleType.Broker);
			var localization = _container.Create<EmailTemplateLocalizationData>();
			var templateId = _container.Create<long>();

			// ReSharper disable ImplicitlyCapturedClosure
			_container.Serializer.Setup(x => x.Deserialize<EventDataForEntity>(eventData.Data)).Returns(eventDataForEntity);
			_container.ApplicationRepository.Setup(x => x.GetExtendedData(eventDataForEntity.EntityId)).Returns(applicationDetailsData);
			_container.ApplicationEventTemplates.Setup(x => x.GetTemplateId(eventType, eventDataForEntity.Data)).Returns(templateId);
			_container.ApplicationEventTemplates.Setup(x => x.GetLocalization(templateId, recipientData.Culture))
				.Returns(localization);
			_container.FilesFasade.Setup(x => x.GetFiles(eventDataForEntity.EntityId, It.IsAny<long?>(), eventType, eventDataForEntity.Data))
				.Returns(new[] { _container.Create<FileHolder>() });
			_container.RecipientsFacade.Setup(x => x.GetRecipients(applicationDetailsData, eventType, eventDataForEntity.Data))
				.Returns(new[] { recipientData });
			_container.TextBulder.Setup(
				x => x.GetText(localization.Subject, recipientData.Culture, eventType, applicationDetailsData, eventDataForEntity.Data))
				.Returns(localization.Subject);
			_container.TextBulder.Setup(
				x => x.GetText(localization.Body, recipientData.Culture, eventType, applicationDetailsData, eventDataForEntity.Data))
				.Returns(localization.Body);

			var messages = _builder.Get(eventType, eventData);

			messages[0].Files.Should().BeNull();
			messages[0].IsBodyHtml.ShouldBeEquivalentTo(localization.IsBodyHtml);
			messages[0].Body.ShouldBeEquivalentTo(localization.Body);
			messages[0].Subject.ShouldBeEquivalentTo(localization.Subject);
			messages[0].To[0].ShouldBeEquivalentTo(recipientData.Email);

			_container.ApplicationRepository.Verify(x => x.GetExtendedData(eventDataForEntity.EntityId));
			_container.ApplicationEventTemplates.Verify(x => x.GetLocalization(templateId, recipientData.Culture));
			_container.ApplicationEventTemplates.Verify(x => x.GetTemplateId(eventType, eventDataForEntity.Data));
			_container.FilesFasade.Verify(x => x.GetFiles(eventDataForEntity.EntityId, It.IsAny<long?>(), eventType, eventDataForEntity.Data));
			_container.RecipientsFacade.Verify(x => x.GetRecipients(applicationDetailsData, eventType, eventDataForEntity.Data));
			_container.TextBulder.Verify(
				x => x.GetText(localization.Subject, recipientData.Culture, eventType, applicationDetailsData, eventDataForEntity.Data));
			_container.TextBulder.Verify(
				x => x.GetText(localization.Body, recipientData.Culture, eventType, applicationDetailsData, eventDataForEntity.Data));
			// ReSharper restore ImplicitlyCapturedClosure
		}
	}
}