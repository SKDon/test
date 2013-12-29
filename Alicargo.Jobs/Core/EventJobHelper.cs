using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services;

namespace Alicargo.Jobs.Core
{
	internal static class EventJobHelper
	{
		public static void Run(
			IEventRepository events,
			ShardSettings shard,
			Action<EventData> action,
			EventState nextState)
		{
			var data = events.GetNext(nextState, shard.ZeroBasedIndex, shard.Count);

			while (data != null)
			{
				try
				{
					action(data);
				}
				catch (Exception e)
				{
					if (!e.IsCritical())
					{
						events.SetState(data.Id, EventState.Failed);
					}

					throw;
				}

				data = events.GetNext(nextState, shard.ZeroBasedIndex, shard.Count);
			}
		}
	}
}
