using System;

namespace Alicargo.Core.Exceptions
{
	public sealed class UnexpectedStateException : Exception
	{
		private readonly long _stateId;

		public UnexpectedStateException(long stateId)
		{
			_stateId = stateId;
		}

		public UnexpectedStateException(long stateId, string message)
			: base(message)
		{
			_stateId = stateId;
		}

		public UnexpectedStateException(long stateId, string message, Exception innerException)
			: base(message, innerException)
		{
			_stateId = stateId;
		}

		public long StateId
		{
			get { return _stateId; }
		}
	}
}