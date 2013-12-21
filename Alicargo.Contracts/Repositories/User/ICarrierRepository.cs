using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.User;

namespace Alicargo.Contracts.Repositories
{
	public interface ICarrierRepository
	{
		CarrierData[] GetAll();
		Func<long> Add(CarrierData carrier);
		CarrierData Get(string name);
	}
}
