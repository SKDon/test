using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface IStateSettingsRepository
	{
		StateRole[] GetStateAvailabilities();
		StateRole[] GetStateVisibilities();
		StateRole[] GetStateEmailRecipients();
	}
}