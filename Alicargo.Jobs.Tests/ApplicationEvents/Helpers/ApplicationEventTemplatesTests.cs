using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Jobs.ApplicationEvents.Entities;
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
			var eventTemplate = _container.Create<EventTemplateData>();
			eventTemplate.EnableEmailSend = true;
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
			_container.EmailTemplateRepository.Setup(x => x.GetByStateId(stateEventData.StateId)).Returns(stateTemplate);
			_container.EmailTemplateRepository.Setup(x => x.GetByEventType(EventType.ApplicationSetState)).Returns(eventTemplate);

			var templateId = _facade.GetTemplateId(EventType.ApplicationSetState, applicationEventData);

			templateId.ShouldBeEquivalentTo(stateTemplate.EmailTemplateId);
			_container.Serializer.Verify(x => x.Deserialize<ApplicationSetStateEventData>(applicationEventData));
			_container.EmailTemplateRepository.Verify(x => x.GetByStateId(stateEventData.StateId));
			_container.EmailTemplateRepository.Verify(x => x.GetByEventType(EventType.ApplicationSetState));
		}
	}
}