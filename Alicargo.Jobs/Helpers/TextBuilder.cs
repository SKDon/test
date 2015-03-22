using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.Helpers
{
	internal sealed class TextBuilder : ITextBuilder
	{
		public string GetText(string template, string language, IDictionary<string, string> data)
		{
			var culture = CultureInfo.GetCultureInfo(language);
			var builder = new StringBuilder(template);

			foreach (var property in data)
			{
				var name = property.Key;

				string match;
				string format;
				while (TextBuilderHelper.GetMatch(builder.ToString(), name, out match, out format))
				{
					var text = TextBuilderHelper.GetText(culture, format, property.Value);

					if (string.IsNullOrEmpty(text))
					{
						builder.Replace(match + Environment.NewLine, string.Empty);
					}

					builder.Replace(match, text);
				}
			}

			return builder.ToString();
		}
	}
}