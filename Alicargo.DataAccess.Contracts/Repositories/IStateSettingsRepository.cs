using Alicargo.DataAccess.Contracts.Contracts.State;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface IStateSettingsRepository
	{
		StateRole[] GetStateAvailabilities();
		StateRole[] GetStateVisibilities();
		StateRole[] GetStateEmailRecipients();
		void SetStateAvailabilities(long stateId, RoleType[] roles);
		void SetStateVisibilities(long stateId, RoleType[] roles);
		void SetStateEmailRecipients(long stateId, RoleType[] roles);
	}
}