using System;

namespace Alicargo.Core.Services.Abstract
{
	public interface ILog
	{
		void Error(string message, Exception exception);
		void Info(string message);
	}
}
