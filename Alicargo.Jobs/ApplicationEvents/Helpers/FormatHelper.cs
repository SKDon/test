using System;
using System.Text.RegularExpressions;
using Alicargo.Core.Services.Abstract;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	internal static class FormatHelper
	{
		public static bool GetFormat(string template, string name, ILog log, out string text, out string format)
		{
			//var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));

			//var task = Task<String>.Factory.StartNew(() =>
			//{
			try
			{
				var match = Regex.Match(template, @"\{(" + name + @")( ?([#\w.,\s:]*))\}");
				text = match.Value;

				if (match.Groups.Count > 2)
				{
					format = match.Groups[2].Value;
				}
			}
			catch (Exception exception)
			{
				log.Error("Failed get a format from the template " + template + " for the parameter " + name, exception);
			}

			text = null;
			format = null;
			//}, tokenSource.Token);

			//return task.Result;
		}
	}
}
