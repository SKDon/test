using System;

namespace Alicargo.DataAccess.Contracts.Exceptions
{
	public sealed class EntityNotFoundException : Exception
	{
		public EntityNotFoundException() { }

		public EntityNotFoundException(string message) : base(message) { }

		public EntityNotFoundException(string message, Exception innerException) : base(message, innerException) { }
	}
}
