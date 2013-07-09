using System;
using Alicargo.Core.Models;

namespace Alicargo.Core.Repositories
{
	public interface ICarrierRepository
	{
		Carrier[] GetAll();
		Func<long> Add(Carrier carrier);
		Carrier Get(string name);
	}
}
