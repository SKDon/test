using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Alicargo.Core.Email;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.TestHelpers;
using Alicargo.Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.Tests.Services.Email
{
	[TestClass]
	public class MailSenderTests
	{
		private MockContainer _context;
		private MailSender _sender;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new MockContainer();
			_context.SenderRepository.Setup(x => x.GetByUserId(TestConstants.TestAdminUserId)).Returns((long?)null);
			_sender = new MailSender(new MailConfiguration(_context.SenderRepository.Object));

			if(!Directory.Exists(Settings.Default.MailsFolder))
			{
				Directory.CreateDirectory(Settings.Default.MailsFolder);
			}

			foreach(var file in Directory.EnumerateFiles(Settings.Default.MailsFolder))
			{
				File.Delete(file);
			}
		}

		[TestCleanup]
		public void TestCleanup()
		{
			foreach(var file in Directory.EnumerateFiles(Settings.Default.MailsFolder))
			{
				File.Delete(file);
			}
		}

		[TestMethod]
		public void Test_Send()
		{
			var files = _context.CreateMany<FileHolder>().ToArray();
			_sender.Send(
				new EmailMessage(
					"subject",
					"body",
					"from@mail.com",
					"to@gmail.com",
					TestConstants.TestAdminUserId) {Files = files});

			var count = Directory.EnumerateFiles(Settings.Default.MailsFolder).Count();
			Assert.AreEqual(1, count);
		}

		[TestMethod]
		public void Test_Send_Parallel()
		{
			var tasks = new Task[10];
			var files = _context.CreateMany<FileHolder>().ToArray();

			for(var index = 0; index < tasks.Length; index++)
			{
				tasks[index] = Task.Factory.StartNew(
					() => _sender.Send(
						new EmailMessage(
							"subject",
							"body",
							"from@mail.com",
							"to@gmail.com",
							TestConstants.TestAdminUserId)
						{
							Files = files
						}));
			}

			Task.WaitAll(tasks);

			var count = Directory.EnumerateFiles(Settings.Default.MailsFolder).Count();
			Assert.AreEqual(tasks.Length, count);
		}
	}
}