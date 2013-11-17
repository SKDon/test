using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface IStateRepository
	{
		long Count();
		StateData[] GetAll();
		StateData Get(long id);

		long[] GetStateAvailability(RoleType role);
		RoleType[] GetAvailableRoles(long stateId);
		long[] GetStateVisibility(RoleType admin);
	}
}