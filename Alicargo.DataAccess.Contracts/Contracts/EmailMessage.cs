using System;
using Alicargo.DataAccess.Contracts.Helpers;

namespace Alicargo.DataAccess.Contracts.Contracts
{
	public sealed class EmailMessage
	{
		public EmailMessage(string subject, string body, string from, string to, long? emailSenderUserId)
			: this(subject, body, from, EmailsHelper.SplitAndTrimEmails(to), emailSenderUserId)
		{
		}

		public EmailMessage(string subject, string body, string from, string[] to, long? emailSenderUserId)
		{			
			if(to == null || to.Length == 0)
				throw new ArgumentNullException("to");

			Body = body;
			Subject = subject;
			To = to;
			From = from;
			EmailSenderUserId = emailSenderUserId;
		}

		public long? EmailSenderUserId { get; private set; }
		public string From { get; private set; }
		public string Subject { get; private set; }
		public string Body { get; private set; }
		public string[] To { get; private set; }

		public FileHolder[] Files { get; set; }
		public string[] CopyTo { get; set; }
		public bool IsBodyHtml { get; set; }
	}
}