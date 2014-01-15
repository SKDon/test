using System;

namespace Alicargo.DataAccess.Contracts.Exceptions
{
	public sealed class EntityUpdateConflict : Exception
	{
		public EntityUpdateConflict(string message) : base(message) { }
		public EntityUpdateConflict(string message, Exception innerException) : base(message, innerException) { }
	}
}
