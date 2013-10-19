using System.Linq;
using System.Text;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Services.Client
{
	internal sealed class ClientPresenter : IClientPresenter
	{
		private readonly IClientRepository _clients;
		private readonly IIdentityService _identity;
		private readonly IClientFileRepository _files;
		private readonly IClientPermissions _permissions;

		public ClientPresenter(
			IClientRepository clients,
			IIdentityService identity,
			IClientFileRepository files,
			IClientPermissions permissions)
		{
			_clients = clients;
			_identity = identity;
			_files = files;
			_permissions = permissions;
		}

		public ClientData GetCurrentClientData(long? clientId = null)
		{
			ClientData data;

			if (clientId.HasValue)
			{
				data = _clients.GetById(clientId.Value);
			}
			else if (_identity.Id.HasValue)
			{
				data = _clients.GetByUserId(_identity.Id.Value);
			}
			else
			{
				return null;
			}

			if (!_permissions.HaveAccessToClient(data))
				throw new AccessForbiddenException();

			return data;
		}

		public ListCollection<ClientData> GetList(int take, int skip)
		{
			var total = _clients.Count();

			var data = _clients.GetRange(take, skip).ToArray();

			return new ListCollection<ClientData> { Data = data, Total = total };
		}

		public FileHolder GetCalculationFile(long clientId)
		{
			var data = GetCurrentClientData(clientId);

			var file = _files.GetCalculationFile(data.Id) ?? new FileHolder
			{
				Name = "NoCalculation.txt",
				Data = Encoding.UTF8.GetBytes("На данный момент расчетов нет")
			};

			return file;
		}
	}
}