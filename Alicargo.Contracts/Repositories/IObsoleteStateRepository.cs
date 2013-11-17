using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface IObsoleteStateRepository
	{
		long Count();
		ObsoleteStateData[] GetAll();
		ObsoleteStateData Get(long id);

		long[] GetStateAvailability(RoleType role);
		RoleType[] GetAvailableRoles(long stateId);
		long[] GetStateVisibility(RoleType admin);
	}
}