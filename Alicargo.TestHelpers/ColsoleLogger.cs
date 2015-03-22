using System;
using Alicargo.Core.Contracts.Common;

namespace Alicargo.TestHelpers
{
	internal sealed class ConsoleLogger : ILog
	{
		public void Error(string message, Exception exception)
		{
			Console.WriteLine(message + exception);
		}

		public void Warning(string message)
		{
			Console.WriteLine(message);
		}

		public void Info(string message)
		{
			Console.WriteLine(message);
		}
	}
}