namespace Alicargo.Contracts.Repositories.User
{
	public interface IAdminRepository
	{
		void UpdateAdmin(long entityId, string name, string login, string email);
		void AddAdmin(long userId, string name, string login, string email, string language);
	}
}