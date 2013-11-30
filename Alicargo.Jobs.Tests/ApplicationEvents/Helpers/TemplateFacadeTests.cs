using System.Linq;
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
	public class TemplateFacadeTests
	{
		private MockContainer _container;
		private TemplatesFacade _facade;

		[TestInitialize]
		public void TestInitialize()
		{
			_container = new MockContainer();

			_facade = _container.Create<TemplatesFacade>();
		}

		[TestMethod]
		public void Test_GetTemplateId_EnableEmailSend_False()
		{
			var eventTemplate = _container.Create<ApplicationEventTemplateData>();
			eventTemplate.EnableEmailSend = false;
			var eventType = _container.Create<ApplicationEventType>();

			_container.EmailTemplateRepository.Setup(x => x.GetByEventType(eventType)).Returns(eventTemplate);

			var templateId = _facade.GetTemplateId(eventType, _container.CreateMany<byte>().ToArray());

			templateId.Should().NotHaveValue();
		}

		[TestMethod]
		public void Test_GetTemplateId_UseApplicationEventTemplate_True()
		{
			var bytes = _container.CreateMany<byte>().ToArray();
			var stateTemplate = new StateEmailTemplateData
			{
				UseApplicationEventTemplate = true,
				EnableEmailSend = true,
				EmailTemplateId = _container.Create<long>()
			};
			var stateEventData = _container.Create<ApplicationSetStateEventData>();
			var eventTemplate = _container.Create<ApplicationEventTemplateData>();
			eventTemplate.EnableEmailSend = true;

			_container.Serializer.Setup(x => x.Deserialize<ApplicationSetStateEventData>(bytes)).Returns(stateEventData);
			_container.EmailTemplateRepository.Setup(x => x.GetByStateId(stateEventData.StateId)).Returns(stateTemplate);
			_container.EmailTemplateRepository.Setup(x => x.GetByEventType(ApplicationEventType.SetState)).Returns(eventTemplate);

			var templateId = _facade.GetTemplateId(ApplicationEventType.SetState, bytes);

			templateId.ShouldBeEquivalentTo(eventTemplate.EmailTemplateId);
		}

		[TestMethod]
		public void Test_GetTemplateId_UseApplicationEventTemplate_False()
		{
			var bytes = _container.CreateMany<byte>().ToArray();
			var stateEventData = _container.Create<ApplicationSetStateEventData>();
			var eventTemplate = _container.Create<ApplicationEventTemplateData>();
			eventTemplate.EnableEmailSend = true;
			var stateTemplate = new StateEmailTemplateData
			{
				UseApplicationEventTemplate = false,
				EnableEmailSend = true,
				EmailTemplateId = _container.Create<long>()
			};

			_container.Serializer.Setup(x => x.Deserialize<ApplicationSetStateEventData>(bytes)).Returns(stateEventData);
			_container.EmailTemplateRepository.Setup(x => x.GetByStateId(stateEventData.StateId)).Returns(stateTemplate);
			_container.EmailTemplateRepository.Setup(x => x.GetByEventType(ApplicationEventType.SetState)).Returns(eventTemplate);

			var templateId = _facade.GetTemplateId(ApplicationEventType.SetState, bytes);

			templateId.ShouldBeEquivalentTo(stateTemplate.EmailTemplateId);
		}
	}
}
