using System;
using Alicargo.DataAccess.Contracts.Helpers;

namespace Alicargo.DataAccess.Contracts.Contracts
{
	public sealed class EmailMessage
	{
		public EmailMessage(string subject, string body, string from, string to)
			: this(subject, body, from, EmailsHelper.SplitAndTrimEmails(to))
		{
		}

		public EmailMessage(string subject, string body, string from, string[] to)
		{
			if (to == null || to.Length == 0)
				throw new ArgumentNullException("to");

			Body = body;
			Subject = subject;
			To = to;
			From = from;
		}

		public string From { get; private set; }
		public string Subject { get; private set; }
		public string Body { get; private set; }
		public string[] To { get; private set; }

		public FileHolder[] Files { get; set; }
		public string[] CopyTo { get; set; }
		public bool IsBodyHtml { get; set; }
	}
}