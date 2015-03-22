using System;

namespace Alicargo.DataAccess.Contracts.Exceptions
{
	public sealed class DublicateException : Exception
	{
		public DublicateException(string message, Exception exception) : base(message, exception) { }
	}
}