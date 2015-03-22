using System;

namespace Alicargo.Utilities
{
	public static class DateTimeProvider
	{
		private static IDateTimeProvider _current;

		static DateTimeProvider()
		{
			SetDefault();
		}

		public static DateTimeOffset Now
		{
			get { return _current.Now; }
		}

		public static void SetDefault()
		{
			SetProvider(new DefaultDateTimeProvider());
		}

		public static void SetProvider(IDateTimeProvider value)
		{
			_current = value;
		}

		private sealed class DefaultDateTimeProvider : IDateTimeProvider
		{
			DateTimeOffset IDateTimeProvider.Now
			{
				get { return DateTimeOffset.Now; }
			}
		}

		public interface IDateTimeProvider
		{
			DateTimeOffset Now { get; }
		}
	}
}