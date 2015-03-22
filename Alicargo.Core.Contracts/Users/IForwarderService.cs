namespace Alicargo.Core.Contracts.Users
{
	public interface IForwarderService
	{
		long GetByCityOrAny(long cityId, long? oldForwarderId);
	}
}