namespace Alicargo.Contracts.Repositories.User
{
	public interface IClientBalanceRepository
	{
		void AddToHistory(long clientId, decimal money, string comment);
		decimal GetBalance(long clientId);
		void SetBalance(long clientId, decimal balance);
	}
}
