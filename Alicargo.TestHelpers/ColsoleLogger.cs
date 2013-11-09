﻿using System;
using Alicargo.Core.Services.Abstract;

namespace Alicargo.TestHelpers
{
	internal sealed class ConsoleLogger : ILog
	{
		public void Error(string message, Exception exception)
		{
			Console.WriteLine(message + exception);
		}

		public void Info(string message)
		{
			Console.WriteLine(message);
		}
	}
}
