using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;

namespace Alicargo.Jobs.Calculation
{
	internal static class CalculationJobHepler
	{
		public static bool SetState(this ICalculationRepository calculations,
			VersionedData<CalculationState, CalculationData> item, CalculationState state)
		{
			try
			{
				var data = calculations.SetState(item.Version.Id, item.Version.RowVersion, state);

				item.Version.RowVersion = data.RowVersion;
				item.Version.State = data.State;
				item.Version.StateTimestamp = data.StateTimestamp;
			}
			catch (EntityUpdateConflict)
			{
				return false;
			}

			return true;
		}

		public static void Process(this ICalculationRepository calculations, CalculationState stateToProcess,
			CalculationState processingState, Action<VersionedData<CalculationState, CalculationData>> run)
		{
			var data = calculations.Get(stateToProcess);

			foreach (var item in data)
			{
				if (calculations.SetState(item, processingState))
				{
					run(item);
				}
			}
		}
	}
}