using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
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