using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Services
{
	// todo: test
	public sealed class ClientPresenter : IClientPresenter
	{
		private readonly IClientRepository _clientRepository;
		private readonly IIdentityService _identity;
		private readonly IClientManager _clientManager;

		public ClientPresenter(IClientRepository clientRepository, IIdentityService identity, IClientManager clientManager)
		{
			_clientRepository = clientRepository;
			_identity = identity;
			_clientManager = clientManager;
		}

		public ClientData GetClientData(long? id = null)
		{
			ClientData data;

			if (id.HasValue)
			{
				data = _clientRepository.GetById(id.Value);
			}
			else if (_identity.Id.HasValue)
			{
				data = _clientRepository.GetByUserId(_identity.Id.Value);
			}
			else
			{
				return null;
			}

			if (!_clientManager.HaveAccessToClient(data.UserId))
				throw new AccessForbiddenException();

			return data;
		}

		public ListCollection<ClientData> GetList(int take, int skip)
		{
			var total = _clientRepository.Count();

			var data = _clientRepository.GetRange(skip, take).ToArray();

			return new ListCollection<ClientData> { Data = data, Total = total };
		}
	}
}