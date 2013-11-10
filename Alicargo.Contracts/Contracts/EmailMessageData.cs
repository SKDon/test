using System;
using System.Collections.Generic;

namespace Alicargo.Contracts.Contracts
{
	public sealed class EmailMessageData
	{
		private const string EmailSeparator = ";";

		public static string[] Split(string to)
		{
			return to == null ? null : to.Split(new[] { EmailSeparator }, StringSplitOptions.RemoveEmptyEntries);
		}

		public static string Join(IEnumerable<string> to)
		{
			return to == null ? null : string.Join(EmailSeparator, to);
		}

		public long Id { get; set; }

		public string From { get; set; }
		public string To { get; set; }
		public string CopyTo { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public byte[] Files { get; set; }
		public bool IsBodyHtml { get; set; }
	}
}