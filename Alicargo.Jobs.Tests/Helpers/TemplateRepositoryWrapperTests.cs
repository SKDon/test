using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Jobs.Helpers;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.Jobs.Tests.Helpers
{
	[TestClass]
	public class TemplateRepositoryWrapperTests
	{
		private MockContainer _container;
		private TemplateRepositoryWrapper _templateRepositoryWrapper;

		[TestInitialize]
		public void TestInitialize()
		{
			_container = new MockContainer();

			_templateRepositoryWrapper = _container.Create<TemplateRepositoryWrapper>();
		}

		[TestMethod]
		public void Test_GetByEventType_EnableEmailSend_False()
		{
			var eventTemplate = _container.Create<EventTemplateData>();
			eventTemplate.EnableEmailSend = false;
			var eventType = _container.Create<EventType>();

			_container.TemplateRepository.Setup(x => x.GetByEventType(eventType)).Returns(eventTemplate);

			var template = _templateRepositoryWrapper.GetTemplateId(eventType);

			template.Should().NotHaveValue();
		}

		[TestMethod]
		public void Test_GetByEventType_EnableEmailSend_True()
		{
			var eventTemplate = _container.Create<EventTemplateData>();
			eventTemplate.EnableEmailSend = true;
			var eventType = _container.Create<EventType>();

			_container.TemplateRepository.Setup(x => x.GetByEventType(eventType)).Returns(eventTemplate);

			var template = _templateRepositoryWrapper.GetTemplateId(eventType);

			template.ShouldBeEquivalentTo(eventTemplate.EmailTemplateId);
		}
	}
}