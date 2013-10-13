using System;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;

namespace Alicargo.Jobs.Calculation
{
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