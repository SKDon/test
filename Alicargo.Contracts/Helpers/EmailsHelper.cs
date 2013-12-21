using System;
using System.Linq;

namespace Alicargo.Contracts.Helpers
{
	public static class EmailsHelper
	{
		private static readonly string DefaultEmailSeparator = Environment.NewLine;
		private static readonly string[] Separators = {",", ";", DefaultEmailSeparator};

		public static string[] SplitEmails(string emails)
		{
			return emails.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
				.Select(s => s.Trim())
				.ToArray();
		}

		public static string JoinEmails(string[] emails)
		{
			return string.Join(DefaultEmailSeparator, string.Join(DefaultEmailSeparator, emails));
		}
	}
}