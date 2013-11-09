using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface IEmailMessageRepository
	{
		void Add(Message message);
	}
}
