using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Models;

namespace Alicargo.Core.Repositories
{
	public interface ICarrierRepository
	{
		CarrierData[] GetAll();
		Func<long> Add(CarrierData carrier);
		CarrierData Get(string name);
	}
}
