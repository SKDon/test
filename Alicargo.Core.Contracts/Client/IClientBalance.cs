using System;

namespace Alicargo.Core.Contracts.Client
{
	public interface IClientBalance
	{
		void Increase(long clientId, decimal money, string comment, DateTimeOffset timestamp, bool isCalculation);
		void Decrease(long clientId, decimal money, string comment, DateTimeOffset timestamp, bool isCalculation);
	}
}