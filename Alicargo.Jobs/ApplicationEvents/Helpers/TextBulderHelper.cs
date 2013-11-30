using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Alicargo.Jobs.ApplicationEvents.Entities;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	internal static class TextBulderHelper
	{
		static readonly TimeSpan Timeout = TimeSpan.FromSeconds(30);

		public static bool GetMatch(string template, string paremeterName, out string text, out string format)
		{
			text = null;
			format = null;
			if (string.IsNullOrWhiteSpace(template))
			{
				return false;
			}

			try
			{
				var pattern = @"\{" + paremeterName + @"\s*(\[([^\]].+?)\])?\}";
				var match = Regex.Match(template, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline, Timeout);
				if (!match.Success)
				{
					return false;
				}

				text = match.Value;

				if (match.Groups.Count == 3 && match.Groups[2].Success)
				{
					format = match.Groups[2].Value;
				}

				return true;
			}
			catch (RegexMatchTimeoutException exception)
			{
				throw new TextBulderException("Failed get a replacement from the template " + template + " for the parameter " + paremeterName,
					exception);
			}
		}

		public static string GetText(CultureInfo culture, string format, string value)
		{
			if (string.IsNullOrWhiteSpace(format))
			{
				return value;
			}

			if (value == null)
			{
				return string.Empty;
			}

			try
			{
				return string.Format(culture, format, value);
			}
			catch (FormatException)
			{
				return format + value;
			}
		}
	}
}