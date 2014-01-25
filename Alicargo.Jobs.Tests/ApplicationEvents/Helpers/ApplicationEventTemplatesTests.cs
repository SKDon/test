using Alicargo.Core.Contracts.Event;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Jobs.ApplicationEvents.Helpers;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.Jobs.Tests.ApplicationEvents.Helpers
{
	[TestClass]
	public class ApplicationEventTemplatesTests
	{
		private MockContainer _container;
		private ApplicationEventTemplates _facade;

		[TestInitialize]
		public void TestInitialize()
		{
			_container = new MockContainer();
			_facade = _container.Create<ApplicationEventTemplates>();
		}

		[TestMethod]
		public void Test_GetTemplateId_UseEventTemplate_False()
		{
			var emailTemplateId = _container.Create<long>();
			var stateTemplate = new StateEmailTemplateData
			{
				UseEventTemplate = false,
				EnableEmailSend = true,
				EmailTemplateId = _container.Create<long>()
			};
			var applicationEventData = _container.Create<byte[]>();
			var stateEventData = _container.Create<ApplicationSetStateEventData>();

			_container.Serializer.Setup(x => x.Deserialize<ApplicationSetStateEventData>(applicationEventData))
				.Returns(stateEventData);
			_container.TemplateRepository.Setup(x => x.GetByStateId(stateEventData.StateId)).Returns(stateTemplate);
			_container.TemplateRepositoryWrapper.Setup(x => x.GetTemplateId(EventType.ApplicationSetState)).Returns(emailTemplateId);

			var templateId = _facade.GetTemplateId(EventType.ApplicationSetState, applicationEventData);

			templateId.ShouldBeEquivalentTo(stateTemplate.EmailTemplateId);
			templateId.Should().NotBe(emailTemplateId);
			_container.Serializer.Verify(x => x.Deserialize<ApplicationSetStateEventData>(applicationEventData));
			_container.TemplateRepository.Verify(x => x.GetByStateId(stateEventData.StateId));
			_container.TemplateRepositoryWrapper.Verify(x => x.GetTemplateId(EventType.ApplicationSetState));
		}
	}
}