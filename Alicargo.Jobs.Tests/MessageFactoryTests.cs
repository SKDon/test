﻿using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.Application;
using Alicargo.Contracts.Enums;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Jobs.Events.Helpers;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Jobs.Tests
{
	// ReSharper disable ImplicitlyCapturedClosure
	[TestClass]
	public class MessageFactoryTests
	{
		private MockContainer _container;
		private IMessageFactory _factory;

		[TestInitialize]
		public void TestInitialize()
		{
			_container = new MockContainer();
			_factory = _container.Create<MessageFactory>();
		}

		[TestMethod]
		public void Test_SetStateForNoClient()
		{
			var applicationId = _container.Create<long>();
			const EventType eventType = EventType.ApplicationSetState;
			var bytes = _container.Create<byte[]>();
			var applicationDetailsData = _container.Create<ApplicationDetailsData>();
			var recipientData = new RecipientData(_container.Create<string>(), _container.Create<string>(), RoleType.Broker);
			var localization = _container.Create<EmailTemplateLocalizationData>();
			var templateId = _container.Create<long>();

			_container.ApplicationRepository.Setup(x => x.GetDetails(applicationId)).Returns(applicationDetailsData);
			_container.TemplatesFacade.Setup(x => x.GetTemplateId(eventType, bytes)).Returns(templateId);
			_container.TemplatesFacade.Setup(x => x.GetLocalization(templateId, recipientData.Culture))
				.Returns(localization);
			_container.FilesFasade.Setup(x => x.GetFiles(applicationId, It.IsAny<long?>(), eventType, bytes))
				.Returns(new[] {_container.Create<FileHolder>()});
			_container.RecipientsFacade.Setup(x => x.GetRecipients(applicationDetailsData, eventType, bytes))
				.Returns(new[] {recipientData});
			_container.TextBulder.Setup(
				x => x.GetText(localization.Subject, recipientData.Culture, eventType, applicationDetailsData, bytes))
				.Returns(localization.Subject);
			_container.TextBulder.Setup(
				x => x.GetText(localization.Body, recipientData.Culture, eventType, applicationDetailsData, bytes))
				.Returns(localization.Body);

			var messages = _factory.Get(applicationId, eventType, bytes);

			messages[0].Files.Should().BeNull();
			messages[0].IsBodyHtml.ShouldBeEquivalentTo(localization.IsBodyHtml);
			messages[0].Body.ShouldBeEquivalentTo(localization.Body);
			messages[0].Subject.ShouldBeEquivalentTo(localization.Subject);
			messages[0].To[0].ShouldBeEquivalentTo(recipientData.Email);

			_container.ApplicationRepository.Verify(x => x.GetDetails(applicationId));
			_container.TemplatesFacade.Verify(x => x.GetTemplateId(eventType, bytes));
			_container.TemplatesFacade.Verify(x => x.GetLocalization(templateId, recipientData.Culture));
			_container.FilesFasade.Verify(x => x.GetFiles(applicationId, It.IsAny<long?>(), eventType, bytes));
			_container.RecipientsFacade.Verify(x => x.GetRecipients(applicationDetailsData, eventType, bytes));
			_container.TextBulder.Verify(
				x => x.GetText(localization.Subject, recipientData.Culture, eventType, applicationDetailsData, bytes));
			_container.TextBulder.Verify(
				x => x.GetText(localization.Body, recipientData.Culture, eventType, applicationDetailsData, bytes));
		}
	}

	// ReSharper restore ImplicitlyCapturedClosure
}