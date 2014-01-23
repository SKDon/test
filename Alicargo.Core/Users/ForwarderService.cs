using System.Linq;
using Alicargo.Core.Contracts.Users;
using Alicargo.DataAccess.Contracts.Contracts.User;
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

		public ForwarderData GetByCityOrDefault(long cityId)
		{
			var forwarders = _forwarders.GetAll();

			var forwarder = forwarders.FirstOrDefault(x => x.CityId == cityId) ?? forwarders.First();

			return forwarder;
		}
	}
}