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

			_templates.Set(id, TwoLetterISOLanguageName.English, localizationData, false);

			var stateEmailTemplateData = _templates.GetByStateId(id, TwoLetterISOLanguageName.English);

			stateEmailTemplateData.EnableEmailSend.Should().BeFalse();

			stateEmailTemplateData.Localization.ShouldBeEquivalentTo(localizationData);
		}

		private long AddTestState()
		{
			return _states.Add(TwoLetterISOLanguageName.English, _fixture.Create<StateData>());
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Set_Update()
		{
			var id = AddTestState();

			var localizationData = _fixture.Create<EmailTemplateLocalizationData>();

			_templates.Set(id, TwoLetterISOLanguageName.English, localizationData, false);

			var newLocalizationData = _fixture.Create<EmailTemplateLocalizationData>();

			_templates.Set(id, TwoLetterISOLanguageName.English, newLocalizationData, true);

			var stateEmailTemplateData = _templates.GetByStateId(id, TwoLetterISOLanguageName.English);

			stateEmailTemplateData.EnableEmailSend.Should().BeTrue();

			var actual = stateEmailTemplateData.Localization;

			actual.ShouldBeEquivalentTo(newLocalizationData);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Set_Merge()
		{
			var id = AddTestState();

			var localizationData = _fixture.Create<EmailTemplateLocalizationData>();

			_templates.Set(id, TwoLetterISOLanguageName.English, localizationData, false);

			var newLocalizationData = _fixture.Create<EmailTemplateLocalizationData>();

			_templates.Set(id, TwoLetterISOLanguageName.Italian, newLocalizationData, true);

			var stateEmailTemplateData = _templates.GetByStateId(id, TwoLetterISOLanguageName.English);

			stateEmailTemplateData.EnableEmailSend.Should().BeTrue();

			stateEmailTemplateData.Localization.ShouldBeEquivalentTo(localizationData);

			_templates.GetByStateId(id, TwoLetterISOLanguageName.Italian).Localization.ShouldBeEquivalentTo(newLocalizationData);
		}
	}
}