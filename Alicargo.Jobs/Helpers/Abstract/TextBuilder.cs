using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Alicargo.Jobs.Helpers.Abstract
{
	internal sealed class TextBuilder<T> : ITextBuilder<T>
	{
		// ReSharper disable StaticFieldInGenericType
		private static readonly PropertyInfo[] Properties = typeof(T).GetProperties().Where(x => x.PropertyType == typeof(string)).ToArray();
		// ReSharper restore StaticFieldInGenericType

		public string GetText(string template, string language, T data)
		{
			var culture = CultureInfo.GetCultureInfo(language);
			var builder = new StringBuilder(template);

			foreach (var property in Properties)
			{
				var name = property.Name;

				string match;
				string format;
				while (TextBuilderHelper.GetMatch(builder.ToString(), name, out match, out format))
				{
					var value = (string)property.GetValue(data);

					var text = TextBuilderHelper.GetText(culture, format, value);

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