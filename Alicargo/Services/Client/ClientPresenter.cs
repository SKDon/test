using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Services.Client
{
    internal sealed class ClientPresenter : IClientPresenter
	{
		private readonly IClientRepository _clientRepository;
		private readonly IIdentityService _identity;
        private readonly IClientPermissions _clientPermissions;

        public ClientPresenter(
            IClientRepository clientRepository,
            IIdentityService identity, 
            IClientPermissions clientPermissions)
		{
			_clientRepository = clientRepository;
			_identity = identity;
		    _clientPermissions = clientPermissions;
		}

		public ClientData GetCurrentClientData(long? clientId = null)
		{
			ClientData data;

			if (clientId.HasValue)
			{
				data = _clientRepository.GetById(clientId.Value);
			}
			else if (_identity.Id.HasValue)
			{
				data = _clientRepository.GetByUserId(_identity.Id.Value);
			}
			else
			{
				return null;
			}

            if (!_clientPermissions.HaveAccessToClient(data))
				throw new AccessForbiddenException();

			return data;
		}

		public ListCollection<ClientData> GetList(int take, int skip)
		{
			var total = _clientRepository.Count();

			var data = _clientRepository.GetRange(take, skip).ToArray();

			return new ListCollection<ClientData> { Data = data, Total = total };
		}

	    public FileHolder GetCalculationFile()
	    {
		    throw new System.NotImplementedException();
	    }
	}
}