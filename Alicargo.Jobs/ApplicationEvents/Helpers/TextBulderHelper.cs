using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Alicargo.Core.Services.Abstract;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	internal static class TextBulderHelper
	{
		public static bool GetMatch(string template, string name, ILog log, out string text, out string format)
		{
			//var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));

			//var task = Task<String>.Factory.StartNew(() =>
			//{
			text = null;
			format = null;
			try
			{
				//\{(name)( *\[([:print:]+)\])\}
				//\[([\w\s\{\}])+\]
				var match = Regex.Match(template, @"\{(" + name + @")(\s*\[(\p{C}+)\])");
				text = match.Value;

				if (match.Groups.Count > 2)
				{
					format = match.Groups[2].Value;
				}

				return true;
			}
			catch (Exception exception)
			{
				log.Error("Failed get a format from the template " + template + " for the parameter " + name, exception);
			}

			return false;
			//}, tokenSource.Token);

			//return task.Result;
		}

		public static string GetText(CultureInfo culture, string format, string value)
		{
			if (format != null)
			{
				if (string.IsNullOrEmpty(value))
				{
					return string.Empty;
				}

				return string.Format(culture, format, value);
			}

			return string.Format(culture, "{0}", value);
		}
	}
}