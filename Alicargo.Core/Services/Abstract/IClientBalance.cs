using System;

namespace Alicargo.Core.Services.Abstract
{
	public interface IClientBalance
	{
		void Add(long clientId, decimal money, string comment, DateTimeOffset timestamp);
	}
}