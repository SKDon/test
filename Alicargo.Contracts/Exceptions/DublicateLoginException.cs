using System;

namespace Alicargo.Contracts.Exceptions
{
	public sealed class DublicateLoginException : Exception
	{
		public DublicateLoginException(string message, Exception exception) : base(message, exception) { }
	}
}