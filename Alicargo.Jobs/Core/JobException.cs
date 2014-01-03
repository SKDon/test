using System;

namespace Alicargo.Jobs.Core
{
	public sealed class JobException : Exception
	{
		public JobException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public JobException(string message)
			: base(message)
		{
		}
	}
}