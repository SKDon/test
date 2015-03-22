using System;
using Alicargo.Utilities;

namespace Alicargo.TestHelpers
{
	public sealed class DateTimeProviderStub : DateTimeProvider.IDateTimeProvider
	{
		public DateTimeProviderStub(DateTimeOffset now)
		{
			Now = now;
		}

		public DateTimeOffset Now { get; private set; }
	}
}