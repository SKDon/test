using System;

namespace Alicargo.DataAccess.Contracts.Exceptions
{
	public sealed class DublicateLoginException : Exception
	{
		public DublicateLoginException(string message, Exception exception) : base(message, exception) { }
	}
}