using System;

namespace Alicargo.Core.Contracts.Common
{
	public interface ILog
	{
		void Error(string message, Exception exception);
		void Info(string message);
	}
}
