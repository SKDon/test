using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface ICalculationRepository
	{
		void Add(CalculationData data, long applicationId);
		void RemoveByApplication(long applicationId);
		CalculationData GetByApplication(long applicationId);
		decimal GetCalculatedSum();
	}
}