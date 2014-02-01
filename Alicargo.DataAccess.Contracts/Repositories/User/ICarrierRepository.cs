using System;
using Alicargo.DataAccess.Contracts.Contracts.User;

namespace Alicargo.DataAccess.Contracts.Repositories.User
{
	public interface ICarrierRepository
	{
		CarrierData[] GetAll();
		Func<long> Add(CarrierData carrier);
		CarrierData Get(string name);
		long? GetByUserId(long userId);
	}
}
