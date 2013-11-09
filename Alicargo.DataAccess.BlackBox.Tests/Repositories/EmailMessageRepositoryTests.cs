using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.DataAccess.BlackBox.Tests.Helpers;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
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

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext();

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
			var partitionId = _context.Fixture.Create<int>();

			var data = Add(partitionId);

			_messages.GetNext(EmailMessageState.New, partitionId + 1).Should().BeNull();

			_messages.GetNext(EmailMessageState.Failed, partitionId).Should().BeNull();

			var next = _messages.GetNext(EmailMessageState.New, partitionId);

			next.ShouldBeEquivalentTo(data, options => options.Excluding(x => x.Id));
		}

		private EmailMessageData Add(int partitionId)
		{
			var data = _context.Fixture.Build<EmailMessageData>()
				.With(x => x.To, string.Join(EmailMessageData.EmailSeparator, _context.Fixture.CreateMany<string>()))
				.With(x => x.CopyTo, string.Join(EmailMessageData.EmailSeparator, _context.Fixture.CreateMany<string>()))
				.Without(x => x.Id)
				.Create();

			_messages.Add(partitionId, data.From,
				data.To.Split(new[] { EmailMessageData.EmailSeparator }, StringSplitOptions.RemoveEmptyEntries),
				data.CopyTo.Split(new[] { EmailMessageData.EmailSeparator }, StringSplitOptions.RemoveEmptyEntries), data.Subject,
				data.Body, data.IsBodyHtml,
				data.Files);

			return data;
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_SetState()
		{
			var partitionId = _context.Fixture.Create<int>();

			Add(partitionId);

			var data = _messages.GetNext(EmailMessageState.New, partitionId);

			_messages.SetState(data.Id, EmailMessageState.Sent);

			_messages.GetNext(EmailMessageState.New, partitionId).Should().BeNull();

			var next = _messages.GetNext(EmailMessageState.Sent, partitionId);

			next.ShouldBeEquivalentTo(data);
		}
	}
}