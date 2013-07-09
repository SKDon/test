using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Alicargo.Core.Localization
{
	public static class LocalizeEnumExtension
	{
		public static IEnumerable<TResult> Select<T, TResult>(Func<T, string, TResult> action)
			where T : struct, IComparable, IFormattable, IConvertible 
		{
			var type = typeof(T);
			if (!type.IsEnum)
				throw new NotSupportedException();

			return Enum.GetValues(type)
				.Cast<T>()
				.Select(x => action(x, x.ToLocalString()));
		}

		public static string ToLocalString<T>(this T value)
			where T : struct, IComparable, IFormattable, IConvertible 
		{
			var type = typeof(T);
			if (!type.IsEnum)
				throw new NotSupportedException();

			var name = value.ToString(CultureInfo.InvariantCulture);

			var field = type.GetField(name);
			if (field == null) return name;

			var attribute = field.GetCustomAttribute<DisplayNameLocalizedAttribute>();
			return attribute != null ? attribute.DisplayName : name;
		}
	}
}
