using System;

namespace Alicargo.Core.Contracts
{
	public interface ILog
	{
		void Error(string message, Exception exception);
		void Info(string message);
	}
}
