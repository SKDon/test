using Alicargo.Core.Contracts.Event;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Users.Client
{
	internal sealed class ClientManagerWithEvent : IClientManager
	{
		private readonly IEventFacade _events;
		private readonly IClientManager _manager;

		public ClientManagerWithEvent(IClientManager manager, IEventFacade events)
		{
			_manager = manager;
			_events = events;
		}

		public void Update(long clientId, ClientModel model, TransitEditModel transit)
		{
			_manager.Update(clientId, model, transit);
		}

		public long Add(ClientModel client, TransitEditModel transit)
		{
			var id = _manager.Add(client, transit);

			_events.Add(id, EventType.ClientAdded, EventState.Emailing, client.Authentication.NewPassword);

			return id;
		}
	}
}