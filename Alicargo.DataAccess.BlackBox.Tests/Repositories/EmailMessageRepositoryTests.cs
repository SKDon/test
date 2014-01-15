using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class EmailMessageRepositoryTests
	{
		private DbTestContext _context;
		private EmailMessageRepository _messages;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			_messages = new EmailMessageRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_GetNext()
		{
			var partitionId = _fixture.Create<int>();

			var data = Add(partitionId);

			_messages.GetNext(EmailMessageState.New, partitionId + 1).Should().BeNull();

			_messages.GetNext(EmailMessageState.Failed, partitionId).Should().BeNull();

			var next = _messages.GetNext(EmailMessageState.New, partitionId);

			next.ShouldBeEquivalentTo(data, options => options.Excluding(x => x.Id));
		}

		private EmailMessageData Add(int partitionId)
		{
			var data = _fixture.Build<EmailMessageData>()
				.With(x => x.To, EmailMessageData.Join(_fixture.CreateMany<string>()))
				.With(x => x.CopyTo, EmailMessageData.Join(_fixture.CreateMany<string>()))
				.Without(x => x.Id)
				.Create();

			_messages.Add(partitionId, data.From,
				EmailMessageData.Split(data.To),
				EmailMessageData.Split(data.CopyTo),
				data.Subject,
				data.Body, data.IsBodyHtml,
				data.Files);

			return data;
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_SetState()
		{
			var partitionId = _fixture.Create<int>();

			Add(partitionId);

			var data = _messages.GetNext(EmailMessageState.New, partitionId);

			_messages.SetState(data.Id, EmailMessageState.Sent);

			_messages.GetNext(EmailMessageState.New, partitionId).Should().BeNull();

			var next = _messages.GetNext(EmailMessageState.Sent, partitionId);

			next.ShouldBeEquivalentTo(data);
		}
	}
}