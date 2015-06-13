using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Alicargo.Core.Email;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.Tests.Services.Email
{
	[TestClass]
	public class MailSenderTests
	{
		private MockContainer _context;
		private string _mailFolder;
		private MailSender _sender;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new MockContainer();
			_context.SenderRepository.Setup(x => x.GetByUserId(TestConstants.TestAdminUserId)).Returns((long?)null);
			var configuration = new MailConfiguration();
			_sender = new MailSender(configuration);
			_mailFolder = configuration.GetConfiguration(TestConstants.TestAdminUserId)
				.SpecifiedPickupDirectory
				.PickupDirectoryLocation;

			if(Directory.Exists(_mailFolder))
			{
				foreach(var file in Directory.EnumerateFiles(_mailFolder))
				{
					File.Delete(file);
				}
			}
		}

		[TestCleanup]
		public void TestCleanup()
		{
			foreach(var file in Directory.EnumerateFiles(_mailFolder))
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
					TestConstants.TestAdminUserId) { Files = files });

			var count = Directory.EnumerateFiles(_mailFolder).Count();
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

			var count = Directory.EnumerateFiles(_mailFolder).Count();
			Assert.AreEqual(tasks.Length, count);
		}
	}
}