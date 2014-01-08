using System;
using System.Reflection;

namespace Alicargo.Core.Localization
{
	public static class LocalizeEnumExtension
	{
		public static string ToLocalString<T>(this T value)
			where T : struct, IComparable, IFormattable, IConvertible
		{
			var type = typeof(T);

			return ToLocalString(value, type);
		}

		private static string ToLocalString(this ValueType value, Type type)
		{
			if (!type.IsEnum)
				throw new NotSupportedException();

			var name = value.ToString();

			var field = type.GetField(name);
			if (field == null) return name;

			var attribute = field.GetCustomAttribute<DisplayNameLocalizedAttribute>();
			return attribute != null
				? attribute.DisplayName
				: name;
		}
	}
}