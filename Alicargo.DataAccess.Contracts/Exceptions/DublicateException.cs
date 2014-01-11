using System;

namespace Alicargo.Contracts.Exceptions
{
	public sealed class DublicateException : Exception
	{
		public DublicateException(string message, Exception exception) : base(message, exception) { }
	}
}