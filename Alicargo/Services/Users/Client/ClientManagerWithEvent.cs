using Alicargo.Contracts.Enums;
using Alicargo.Core.Event;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Calculation.Admin;
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

		public void Update(long clientId, ClientModel model, CarrierSelectModel carrier, TransitEditModel transit,
			AuthenticationModel authentication)
		{
			_manager.Update(clientId, model, carrier, transit, authentication);
		}

		public long Add(ClientModel model, CarrierSelectModel carrier, TransitEditModel transit,
			AuthenticationModel authentication)
		{
			var id = _manager.Add(model, carrier, transit, authentication);

			return id;
		}

		public void AddToBalance(long clientId, PaymentModel model)
		{
			_manager.AddToBalance(clientId, model);

			if (model.Money == 0)
			{
				return;
			}

			var eventType = model.Money > 0 ? EventType.BalanceIncreased : EventType.BalanceDecreased;

			_events.Add(clientId, eventType, EventState.Emailing);
		}
	}
}