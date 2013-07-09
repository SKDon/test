using Alicargo.Core.Contracts;
using Alicargo.Core.Enums;

namespace Alicargo.Core.Repositories
{
	public interface IStateRepository
	{
		long Count();
		StateData[] GetAll();
		StateData Get(long id);

		long[] GetAvailableStates(RoleType role);
		RoleType[] GetAvailableRoles(long stateId);
		long[] GetVisibleStates(RoleType admin);
	}
}