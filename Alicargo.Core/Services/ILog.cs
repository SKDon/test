using System;

namespace Alicargo.Core.Services
{
	public interface ILog
	{
		void Error(string message, Exception exception);
	}
}
