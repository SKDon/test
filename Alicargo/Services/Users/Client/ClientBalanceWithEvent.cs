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

		public void Increase(long clientId, decimal money, string comment, DateTimeOffset timestamp)
		{
			using(var scope = new TransactionScope())
			{
				_instance.Increase(clientId, money, comment, timestamp);

				AddEvent(clientId, money, comment, timestamp, EventType.BalanceIncreased);

				scope.Complete();
			}
		}

		public void Decrease(long clientId, decimal money, string comment, DateTimeOffset timestamp)
		{
			using(var scope = new TransactionScope())
			{
				_instance.Decrease(clientId, money, comment, timestamp);

				AddEvent(clientId, money, comment, timestamp, EventType.BalanceDecreased);

				scope.Complete();
			}
		}

		private void AddEvent(long clientId, decimal money, string comment, DateTimeOffset timestamp, EventType eventType)
		{
			_events.Add(clientId, eventType, EventState.Emailing,
				new PaymentEventData
				{
					Money = money,
					Comment = comment,
					Timestamp = timestamp
				});
		}
	}
}