using System;
using System.Transactions;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Event;
using Alicargo.Jobs.Balance.Entities;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Calculation.Admin;

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

		public void Add(long clientId, PaymentModel model, DateTimeOffset timestamp)
		{
			using (var scope = new TransactionScope())
			{
				_instance.Add(clientId, model, timestamp);

				if (model.Money != 0)
				{
					AddEvent(clientId, model, timestamp);
				}

				scope.Complete();
			}
		}

		public decimal GetBalance(long clientId)
		{
			return _instance.GetBalance(clientId);
		}

		public ClientBalanceHistoryItem[] GetHistory(long clientId)
		{
			return _instance.GetHistory(clientId);
		}

		private void AddEvent(long clientId, PaymentModel model, DateTimeOffset timestamp)
		{
			var eventType = model.Money > 0 ? EventType.BalanceIncreased : EventType.BalanceDecreased;

			_events.Add(clientId, eventType, EventState.Emailing,
				new PaymentEventData
				{
					AbsMoney = Math.Abs(model.Money),
					Comment = model.Comment,
					Timestamp = timestamp
				});
		}
	}
}