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

		public void Update(long clientId, ClientModel model, TransitEditModel transit, AuthenticationModel authentication)
		{
			_manager.Update(clientId, model, transit, authentication);
		}

		public long Add(ClientModel client, TransitEditModel transit, AuthenticationModel authentication)
		{
			var id = _manager.Add(client, transit, authentication);

			_events.Add(id, EventType.ClientAdd, EventState.Emailing, authentication.NewPassword);

			return id;
		}
	}
}