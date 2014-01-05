﻿using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Core.Resources;
using Alicargo.Core.Services.Abstract;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs.Calculation
{
	public sealed class CalculationProcessor : IEventProcessor
	{
		private readonly IClientBalance _balance;
		private readonly ISerializer _serializer;

		public CalculationProcessor(IClientBalance balance, ISerializer serializer)
		{
			_balance = balance;
			_serializer = serializer;
		}

		public void ProcessEvent(EventType type, EventData data)
		{
			var eventDataForEntity = _serializer.Deserialize<EventDataForEntity>(data.Data);
			var calculation = _serializer.Deserialize<CalculationData>(eventDataForEntity.Data);

			var money = (decimal)calculation.Weight * calculation.TariffPerKg + calculation.ScotchCost
						+ calculation.InsuranceCost + calculation.FactureCost + calculation.TransitCost + calculation.PickupCost;

			switch (type)
			{
				case EventType.Calculate:
					_balance.Decrease(calculation.ClientId, money,
						GetComment(Contracts.Resources.EventType.CalculationCanceled, calculation), DateTimeOffset.UtcNow);
					break;

				case EventType.CalculationCanceled:
					_balance.Increase(calculation.ClientId, money,
						GetComment(Contracts.Resources.EventType.CalculationCanceled, calculation), DateTimeOffset.UtcNow);
					break;

				default:
					throw new JobException("Unexpected type " + type + " in CalculationProcessor");
			}
		}

		private static string GetComment(string type, CalculationData calculation)
		{
			return type
				   + Environment.NewLine
				   + Entities.AWB + ": " + calculation.AirWaybillDisplay
				   + Environment.NewLine
				   + Entities.Application + ": " + calculation.ApplicationDisplay;
		}
	}
}