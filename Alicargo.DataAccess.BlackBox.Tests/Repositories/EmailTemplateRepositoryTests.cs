using System.Linq;
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
			localizationData.TwoLetterISOLanguageName = TwoLetterISOLanguageName.English;

			_templates.Set(id, localizationData, false);

			var stateEmailTemplateData = _templates.GetByStateId(id);

			stateEmailTemplateData.EnableEmailSend.Should().BeFalse();

			stateEmailTemplateData.Localizations.Should().HaveCount(3);

			stateEmailTemplateData.Localizations.Single(x => x.TwoLetterISOLanguageName == TwoLetterISOLanguageName.English)
				.ShouldBeEquivalentTo(localizationData);
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
			localizationData.TwoLetterISOLanguageName = TwoLetterISOLanguageName.English;

			_templates.Set(id, localizationData, false);

			var newLocalizationData = _fixture.Create<EmailTemplateLocalizationData>();
			newLocalizationData.TwoLetterISOLanguageName = localizationData.TwoLetterISOLanguageName;

			_templates.Set(id, newLocalizationData, true);

			var stateEmailTemplateData = _templates.GetByStateId(id);

			stateEmailTemplateData.EnableEmailSend.Should().BeTrue();

			var actual = stateEmailTemplateData.Localizations.Single(x => x.TwoLetterISOLanguageName == TwoLetterISOLanguageName.English);

			actual.ShouldBeEquivalentTo(newLocalizationData);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Set_Merge()
		{
			var id = AddTestState();

			var localizationData = _fixture.Create<EmailTemplateLocalizationData>();
			localizationData.TwoLetterISOLanguageName = TwoLetterISOLanguageName.English;

			_templates.Set(id, localizationData, false);

			var newLocalizationData = _fixture.Create<EmailTemplateLocalizationData>();
			newLocalizationData.TwoLetterISOLanguageName = TwoLetterISOLanguageName.Italian;

			_templates.Set(id, newLocalizationData, true);

			var stateEmailTemplateData = _templates.GetByStateId(id);

			stateEmailTemplateData.EnableEmailSend.Should().BeTrue();

			stateEmailTemplateData.Localizations.Single(x => x.TwoLetterISOLanguageName == TwoLetterISOLanguageName.English)
				.ShouldBeEquivalentTo(localizationData);

			stateEmailTemplateData.Localizations.Single(x => x.TwoLetterISOLanguageName == TwoLetterISOLanguageName.Italian)
				.ShouldBeEquivalentTo(newLocalizationData);
		}
	}
}