using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Models;
using Alicargo.Services.Email;
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
			_sender = new MailSender();

			if (!Directory.Exists(Settings.Default.MailsFolder))
			{
				Directory.CreateDirectory(Settings.Default.MailsFolder);
			}

			foreach (var file in Directory.EnumerateFiles(Settings.Default.MailsFolder))
			{
				File.Delete(file);
			}
		}

		[TestCleanup]
		public void TestCleanup()
		{
			foreach (var file in Directory.EnumerateFiles(Settings.Default.MailsFolder))
			{
				File.Delete(file);
			}
		}

		[TestMethod]
		public void Test_Send()
		{
			var files = _context.CreateMany<FileHolder>().ToArray();
			_sender.Send(new EmailMessage("subject", "body", "to@gmail.com") { Files = files, From = "from@mail.com" });

			var count = Directory.EnumerateFiles(Settings.Default.MailsFolder).Count();
			Assert.AreEqual(1, count);
		}

		[TestMethod]
		public void Test_Send_Parallel()
		{
			var tasks = new Task[10];
			var files = _context.CreateMany<FileHolder>().ToArray();

			for (var index = 0; index < tasks.Length; index++)
			{
				tasks[index] = Task.Factory.StartNew(
					() => _sender.Send(new EmailMessage("subject", "body", "to@gmail.com") { Files = files, From = "from@mail.com" }));
			}

			Task.WaitAll(tasks);

			var count = Directory.EnumerateFiles(Settings.Default.MailsFolder).Count();
			Assert.AreEqual(tasks.Length, count);
		}
	}
}
