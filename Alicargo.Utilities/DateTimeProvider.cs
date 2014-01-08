using System;

namespace Alicargo.Core.Helpers
{
	public static class DateTimeProvider
	{
		private static IDateTimeProvider _current;

		static DateTimeProvider()
		{
			SetProvider(new DefaultDateTimeProvider());
		}

		public static DateTimeOffset Now
		{
			get { return _current.Now; }
		}

		public static void SetProvider(IDateTimeProvider value)
		{
			_current = value;
		}

		private class DefaultDateTimeProvider : IDateTimeProvider
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