using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Alicargo.Jobs.ApplicationEvents.Entities;

namespace Alicargo.Jobs.Helpers
{
	internal static class TextBulderHelper
	{
		private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(30);

		public static string GetText<T>(string template, string language, T data)
		{
			var properties = typeof(T).GetProperties().Where(x => x.PropertyType == typeof(string)).ToArray();

			var culture = CultureInfo.GetCultureInfo(language);
			var builder = new StringBuilder(template);

			foreach (var property in properties)
			{
				var name = property.Name;

				string match;
				string format;
				while (GetMatch(builder.ToString(), name, out match, out format))
				{
					var value = (string)property.GetValue(data);

					var text = GetText(culture, format, value);

					if (string.IsNullOrEmpty(text))
					{
						builder.Replace(match + Environment.NewLine, string.Empty);
					}

					builder.Replace(match, text);
				}
			}

			return builder.ToString();
		}

		internal static bool GetMatch(string template, string paremeterName, out string text, out string format)
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
				throw new TextBulderException(
					"Failed get a replacement from the template " + template + " for the parameter " + paremeterName,
					exception);
			}
		}

		internal static string GetText(CultureInfo culture, string format, string value)
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