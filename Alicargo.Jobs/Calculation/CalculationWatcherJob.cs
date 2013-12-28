using System;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.Core;

namespace Alicargo.Jobs.Calculation
{
	// todo: remove this class and use partitions in the Calculation table to eliminate racing between jobs
	public sealed class CalculationWatcherJob : IJob
	{
		private readonly TimeSpan _deadTimeout;
		private readonly ICalculationRepository _calculations;

		public CalculationWatcherJob(TimeSpan deadTimeout, ICalculationRepository calculations)
		{
			_deadTimeout = deadTimeout;
			_calculations = calculations;
		}

		public void Run()
		{
			var data = _calculations.Get(CalculationState.Emailing);

			foreach (var item in data)
			{
				if (item.Version.StateTimestamp.Add(_deadTimeout) < DateTime.UtcNow)
				{
					_calculations.SetState(item, CalculationState.New);
				}
			}
		}
	}
}