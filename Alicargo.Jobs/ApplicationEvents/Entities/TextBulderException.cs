using System;

namespace Alicargo.Jobs.ApplicationEvents.Entities
{
	internal sealed class TextBulderException : Exception
	{
		public TextBulderException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
