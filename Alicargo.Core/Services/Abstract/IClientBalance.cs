using System;

namespace Alicargo.Core.Services.Abstract
{
	public interface IClientBalance
	{
		void Increase(long clientId, decimal money, string comment, DateTimeOffset timestamp);
		void Decrease(long clientId, decimal money, string comment, DateTimeOffset timestamp);
	}
}