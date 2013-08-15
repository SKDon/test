using System;

namespace Alicargo.Contracts.Exceptions
{
	public sealed class AccessForbiddenException : Exception
	{
		public AccessForbiddenException() { }

		public AccessForbiddenException(string message) : base(message) { }

		public AccessForbiddenException(string message, Exception innerException) : base(message, innerException) { }
	}
}
