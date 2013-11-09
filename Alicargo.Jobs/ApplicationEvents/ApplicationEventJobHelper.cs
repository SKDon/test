using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs.ApplicationEvents
{
	internal static class ApplicationEventJobHelper
	{
		public static void Run(
			IApplicationEventRepository events,
			ShardSettings shard,
			Action<ApplicationEventData> action,
			ApplicationEventState nextState)
		{
			var data = events.GetNext(nextState, shard.ZeroBasedIndex, shard.Total);

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
						events.SetState(data.Id, ApplicationEventState.Failed);
					}

					throw;
				}

				data = events.GetNext(nextState, shard.ZeroBasedIndex, shard.Total);
			}
		}
	}
}
