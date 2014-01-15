using System;

namespace Alicargo.DataAccess.Contracts.Exceptions
{
	public sealed class DeleteConflictedWithConstraintException : Exception
	{
		public DeleteConflictedWithConstraintException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}