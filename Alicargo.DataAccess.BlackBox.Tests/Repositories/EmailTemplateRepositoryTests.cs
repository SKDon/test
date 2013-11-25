﻿using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.DataAccess.BlackBox.Tests.Helpers;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class EmailTemplateRepositoryTests
	{
		private DbTestContext _context;
		private Fixture _fixture;
		private StateRepository _states;
		private EmailTemplateRepository _templates;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext();
			_fixture = new Fixture();

			var executor = new SqlProcedureExecutor(_context.Connection.ConnectionString);
			_templates = new EmailTemplateRepository(executor);
			_states = new StateRepository(executor);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Set_New()
		{
			var id = AddTestState();

			var localizationData = _fixture.Create<EmailTemplateLocalizationData>();

			_templates.SetForState(id, TwoLetterISOLanguageName.English, false, true, localizationData);

			var data = _templates.GetByStateId(id);

			data.UseApplicationEventTemplate.Should().BeTrue();

			data.EnableEmailSend.Should().BeFalse();

			var localization = _templates.GetLocalization(data.EmailTemplateId, TwoLetterISOLanguageName.English);

			localization.ShouldBeEquivalentTo(localizationData);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Set_SetForApplicationEvent()
		{
			var localizationData = _fixture.Create<EmailTemplateLocalizationData>();

			const ApplicationEventType eventType = ApplicationEventType.Created;

			var recipients = _fixture.CreateMany<RoleType>().ToArray();

			_templates.SetForApplicationEvent(eventType, TwoLetterISOLanguageName.English, false, recipients, localizationData);

			var data = _templates.GetBeEventType(eventType);

			data.EnableEmailSend.Should().BeFalse();

			var localization = _templates.GetLocalization(data.EmailTemplateId, TwoLetterISOLanguageName.English);

			localization.ShouldBeEquivalentTo(localizationData);

			_templates.GetRecipients(eventType).ShouldAllBeEquivalentTo(recipients);
		}

		private long AddTestState()
		{
			return _states.Add(TwoLetterISOLanguageName.English, _fixture.Create<StateData>());
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Set_WithSameLanguage()
		{
			var id = AddTestState();

			var localizationData = _fixture.Create<EmailTemplateLocalizationData>();

			_templates.SetForState(id, TwoLetterISOLanguageName.English, false, true, localizationData);

			var newLocalizationData = _fixture.Create<EmailTemplateLocalizationData>();

			_templates.SetForState(id, TwoLetterISOLanguageName.English, true, false, newLocalizationData);

			var data = _templates.GetByStateId(id);

			data.EnableEmailSend.Should().BeTrue();

			data.UseApplicationEventTemplate.Should().BeFalse();

			var localization = _templates.GetLocalization(data.EmailTemplateId, TwoLetterISOLanguageName.English);

			localization.ShouldBeEquivalentTo(newLocalizationData);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Set_WithOtherLanguage()
		{
			var id = AddTestState();

			var localizationData = _fixture.Create<EmailTemplateLocalizationData>();

			_templates.SetForState(id, TwoLetterISOLanguageName.English, false, true, localizationData);

			var newLocalizationData = _fixture.Create<EmailTemplateLocalizationData>();

			_templates.SetForState(id, TwoLetterISOLanguageName.Italian, true, false, newLocalizationData);

			var data = _templates.GetByStateId(id);

			data.EnableEmailSend.Should().BeTrue();

			data.UseApplicationEventTemplate.Should().BeFalse();

			_templates.GetLocalization(data.EmailTemplateId, TwoLetterISOLanguageName.English).ShouldBeEquivalentTo(localizationData);

			_templates.GetLocalization(data.EmailTemplateId, TwoLetterISOLanguageName.Italian).ShouldBeEquivalentTo(newLocalizationData);
		}
	}
}