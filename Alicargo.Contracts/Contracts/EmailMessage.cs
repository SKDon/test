using System;

namespace Alicargo.Contracts.Contracts
{
    public sealed class EmailMessage
	{
		public EmailMessage(string subject, string body, params string[] to)
		{
			if (to == null || to.Length == 0)
				throw new ArgumentNullException("to");

			Body = body;
			Subject = subject;
			To = to;
		}

		public string From { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public string[] To { get; set; }
		public FileHolder[] Files { get; set; }
		public string[] CopyTo { get; set; }
	    public bool IsBodyHtml { get; set; }
	}
}