using System;

namespace Alicargo.Core.Exceptions
{
	public sealed class InvalidLogicException : InvalidOperationException
	{
		public InvalidLogicException() { }

		public InvalidLogicException(string message) : base(message) { }

		public InvalidLogicException(string message, Exception innerException) : base(message, innerException) { }
	}
}
