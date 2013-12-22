using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Alicargo.Contracts.Helpers
{
	public static class EmailsHelper
	{
		private const string EmailPattern = @".+@.+\..+";

		private const RegexOptions EmailRegexOptions =
			RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant;

		private static readonly TimeSpan MatchTimeout = TimeSpan.FromSeconds(10);
		private static readonly string DefaultEmailSeparator = Environment.NewLine;
		private static readonly string[] Separators = {",", ";", DefaultEmailSeparator};

		public static string[] SplitAndTrimEmails(string emails)
		{
			return emails.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
				.Select(s => s.Trim())
				.ToArray();
		}

		public static string JoinEmails(string[] emails)
		{
			return string.Join(DefaultEmailSeparator, string.Join(DefaultEmailSeparator, emails));
		}

		public static bool Validate(string emails)
		{
			var items = SplitAndTrimEmails(emails);

			foreach (var item in items)
			{
				if (!Regex.IsMatch(item, EmailPattern, EmailRegexOptions, MatchTimeout))
				{
					return false;
				}
			}

			return true;
		}
	}
}