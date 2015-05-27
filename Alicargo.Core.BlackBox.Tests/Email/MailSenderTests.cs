using System;
using Alicargo.Core.Email;
using Alicargo.DataAccess.Contracts.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.Core.BlackBox.Tests.Email
{
	[TestClass]
	public class MailSenderTests
	{
		private MailSender _sender;

		[Ignore] // local only
		[TestMethod]
		public void Send_Test()
		{
			_sender.Send(new EmailMessage(
				"test subject" + DateTime.UtcNow,
				"test body " + Guid.NewGuid(),
				"test@fr.om",
				"6alyamov@gmail.com"));
		}

		[TestInitialize]		
		public void TestInitialize()
		{
			_sender = new MailSender();
		}
	}
}