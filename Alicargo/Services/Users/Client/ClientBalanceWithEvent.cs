using System;
using System.Transactions;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Event;
using Alicargo.Core.Services.Abstract;
using Alicargo.Jobs.Balance.Entities;

namespace Alicargo.Services.Users.Client
{
	internal sealed class ClientBalanceWithEvent : IClientBalance
	{
		private readonly IEventFacade _events;
		private readonly IClientBalance _instance;

		public ClientBalanceWithEvent(IClientBalance instance, IEventFacade events)
		{
			_instance = instance;
			_events = events;
		}

		public void Add(long clientId, decimal money, string comment, DateTimeOffset timestamp)
		{
			using (var scope = new TransactionScope())
			{
				_instance.Add(clientId, money, comment, timestamp);

				if (money != 0)
				{
					AddEvent(clientId, money, comment, timestamp);
				}

				scope.Complete();
			}
		}

		private void AddEvent(long clientId, decimal money, string comment, DateTimeOffset timestamp)
		{
			var eventType = money > 0 ? EventType.BalanceIncreased : EventType.BalanceDecreased;

			_events.Add(clientId, eventType, EventState.Emailing,
				new PaymentEventData
				{
					AbsMoney = Math.Abs(money),
					Comment = comment,
					Timestamp = timestamp
				});
		}
	}
}