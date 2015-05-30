using System;
using Alicargo.Core.BlackBox.Tests.Properties;
using Alicargo.Core.Email;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Alicargo.Core.BlackBox.Tests.Email
{
	[TestClass]
	public class MailSenderTests
	{
		private CompositionHelper _context;
		private MailSender _sender;

		[Ignore]
		[TestMethod]
		public void Send_Test()
		{
			_sender.Send(
				new EmailMessage(
					"test subject" + DateTime.UtcNow,
					"test body " + Guid.NewGuid(),
					"test@fr.om",
					"6alyamov@gmail.com",
					null));
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new CompositionHelper(Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString);
			_sender = _context.Kernel.Get<MailSender>();
		}
	}
}