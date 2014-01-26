using System.Linq;
using Alicargo.Core.Contracts.Users;
using Alicargo.DataAccess.Contracts.Repositories.User;

namespace Alicargo.Core.Users
{
	public sealed class ForwarderService : IForwarderService
	{
		private readonly IForwarderRepository _forwarders;

		public ForwarderService(IForwarderRepository forwarders)
		{
			_forwarders = forwarders;
		}

		public long GetByCityOrAny(long cityId, long? oldForwarderId)
		{
			var list = _forwarders.GetByCity(cityId);

			if(list.Length == 0)
			{
				return _forwarders.GetAll().Select(x => x.Id).First();
			}

			if(oldForwarderId.HasValue && list.Contains(oldForwarderId.Value))
			{
				return oldForwarderId.Value;
			}

			return list.First();
		}
	}
}