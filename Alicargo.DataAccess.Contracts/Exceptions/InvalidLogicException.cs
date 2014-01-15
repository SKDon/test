using System;

namespace Alicargo.DataAccess.Contracts.Exceptions
{
	public sealed class InvalidLogicException : Exception
	{
		public InvalidLogicException() { }

		public InvalidLogicException(Exception innerException) : base(innerException.Message, innerException) { }

		public InvalidLogicException(string message) : base(message) { }

		public InvalidLogicException(string message, Exception innerException) : base(message, innerException) { }
	}
}
