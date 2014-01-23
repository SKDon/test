using Alicargo.DataAccess.Contracts.Contracts.User;

namespace Alicargo.Core.Contracts.Users
{
	public interface IForwarderService
	{
		ForwarderData GetByCityOrDefault(long cityId);
	}
}