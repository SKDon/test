using System;
using Alicargo.Contracts.Contracts;

namespace Alicargo.Services.Contract
{
	public sealed class Message
	{
		public Message(string subject, string body, params string[] to)
		{
			if (to == null || to.Length == 0)
				throw new ArgumentNullException("to");

			Body = body;
			Subject = subject;
			To = to;
		}

		public string From { get; set; }
		public string[] To { get; set; }
		public string[] CC { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public FileHolder[] Files { get; set; }
	}
}