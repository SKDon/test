using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Jobs.Helpers;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.Jobs.Tests.Helpers
{
	[TestClass]
	public class TemplateRepositoryHelperTests
	{
		private MockContainer _container;
		private TemplateRepositoryHelper _helper;

		[TestInitialize]
		public void TestInitialize()
		{
			_container = new MockContainer();
			_helper = _container.Create<TemplateRepositoryHelper>();
		}

		[TestMethod]
		public void Test_GetByEventType_EnableEmailSend_False()
		{
			var templateData = _container.Create<EventTemplateData>();
			templateData.EnableEmailSend = false;

			_container.TemplateRepository.Setup(x => x.GetByEventType(EventType.ApplicationSetState)).Returns(templateData);

			var templateId = _helper.GetTemplateId(EventType.ApplicationSetState);

			templateId.Should().NotHaveValue();
		}

		[TestMethod]
		public void Test_GetByEventType_EnableEmailSend_True()
		{
			var templateData = _container.Create<EventTemplateData>();
			templateData.EnableEmailSend = true;

			_container.TemplateRepository.Setup(x => x.GetByEventType(EventType.ApplicationSetState)).Returns(templateData);

			var templateId = _helper.GetTemplateId(EventType.ApplicationSetState);

			templateId.ShouldBeEquivalentTo(templateData.EmailTemplateId);
		}
	}
}