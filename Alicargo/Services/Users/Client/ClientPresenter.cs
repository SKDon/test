using System.Linq;
using Alicargo.Core.Contracts.Client;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Users.Client
{
	internal sealed class ClientPresenter : IClientPresenter
	{
		private readonly IClientBalanceRepository _balances;
		private readonly IClientRepository _clients;
		private readonly IIdentityService _identity;
		private readonly IClientPermissions _permissions;

		public ClientPresenter(
			IClientRepository clients,
			IIdentityService identity,
			IClientBalanceRepository balances,
			IClientPermissions permissions)
		{
			_clients = clients;
			_identity = identity;
			_balances = balances;
			_permissions = permissions;
		}

		public ClientData GetCurrentClientData(long? clientId = null)
		{
			ClientData data;

			if(clientId.HasValue)
			{
				data = _clients.Get(clientId.Value);
			}
			else if(_identity.Id.HasValue)
			{
				data = _clients.GetByUserId(_identity.Id.Value);
			}
			else
			{
				return null;
			}

			if(!_permissions.HaveAccessToClient(data))
				throw new AccessForbiddenException();

			return data;
		}

		public ListCollection<ClientListItem> GetList(int take, int skip)
		{
			var all = _clients.GetAll();

			var data = all.Skip(skip).Take(take)
				.Select(x => new ClientListItem
				{
					Nic = x.Nic,
					LegalEntity = x.LegalEntity,
					Balance = _balances.GetBalance(x.ClientId),
					ClientId = x.ClientId
				})
				.ToArray();

			return new ListCollection<ClientListItem>
			{
				Data = data,
				Total = all.Length
			};
		}
	}
}