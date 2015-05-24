using System;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace Alicargo.DataAccess.Contracts.Helpers
{
	public static class EmailsHelper
	{
		private const string EmailPattern = @".+@.+\..+";

		private const RegexOptions EmailRegexOptions =
			RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant;

		public static readonly string DefaultFrom = ConfigurationManager.AppSettings["DefaultFrom"];
		public static readonly string SupportEmail = ConfigurationManager.AppSettings["SupportEmail"];
		public static readonly string FeedbackEmail = ConfigurationManager.AppSettings["FeedbackEmail"];

		private static readonly TimeSpan MatchTimeout = TimeSpan.FromSeconds(10);
		private static readonly string DefaultEmailSeparator = Environment.NewLine;
		private static readonly string[] Separators = {",", ";", DefaultEmailSeparator};

		public static string[] SplitAndTrimEmails(string emails)
		{
			return emails == null
				? null
				: emails.Split(Separators, StringSplitOptions.RemoveEmptyEntries)
					.Select(s => s.Trim())
					.ToArray();
		}

		public static bool Validate(string emails)
		{
			if(emails == null)
			{
				return false;
			}

			var items = SplitAndTrimEmails(emails);

			return items.All(item => Regex.IsMatch(item, EmailPattern, EmailRegexOptions, MatchTimeout));
		}
	}
}