using System;

namespace Alicargo.DataAccess.Contracts.Exceptions
{
	public sealed class UpdateConflictException : Exception
	{
		public UpdateConflictException(string message) : base(message) { }
		public UpdateConflictException(string message, Exception innerException) : base(message, innerException) { }
	}
}
