using System;

namespace Alicargo.Contracts.Exceptions
{
	public sealed class DeleteConflictedWithConstraintException : Exception
	{
		public DeleteConflictedWithConstraintException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}