using System;
using System.Globalization;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class CalculationRepository : ICalculationRepository
	{
		private readonly AlicargoDataContext _context;

		public CalculationRepository(IUnitOfWork unitOfWork)
		{
			_context = (AlicargoDataContext)unitOfWork.Context;
		}

		public void Add(CalculationData data, long applicationId)
		{
			_context.Calculations.InsertOnSubmit(new Calculation
			{
				AirWaybillDisplay = data.AirWaybillDisplay,
				ApplicationDisplay = data.ApplicationDisplay,
				ClientId = data.ClientId,
				FactureCost = data.FactureCost,
				Id = 0,
				InsuranceCost = data.InsuranceCost,
				MarkName = data.MarkName,
				RowVersion = null,
				ScotchCost = data.ScotchCost,
				StateId = (int)CalculationState.New,
				StateIdTimestamp = DateTimeOffset.UtcNow,
				TariffPerKg = data.TariffPerKg,
				Weight = data.Weight,
				ApplicationHistoryId = applicationId
			});
		}

		public VersionedData<CalculationState, CalculationData>[] Get(CalculationState state)
		{
			return _context.Calculations.Where(x => x.StateId == (int)state)
						  .OrderBy(x => x.StateIdTimestamp)
						  .Select(x => new VersionedData<CalculationState, CalculationData>
						  {
							  Version = new VersionData<CalculationState>
							  {
								  State = (CalculationState)x.StateId,
								  StateTimestamp = x.StateIdTimestamp,
								  Id = x.Id,
								  RowVersion = x.RowVersion.ToArray(),
							  },
							  Data = new CalculationData
							  {
								  AirWaybillDisplay = x.AirWaybillDisplay,
								  ApplicationDisplay = x.ApplicationDisplay,
								  ClientId = x.ClientId,
								  FactureCost = x.FactureCost,
								  InsuranceCost = x.InsuranceCost,
								  MarkName = x.MarkName,
								  ScotchCost = x.ScotchCost,
								  TariffPerKg = x.TariffPerKg,
								  Weight = x.Weight
							  }
						  })
						  .ToArray();
		}

		public VersionData<CalculationState> SetState(long id, byte[] rowVersion, CalculationState state)
		{
			var result = _context.Calculation_SetState(id, rowVersion, (int)state).FirstOrDefault();

			if (result == null)
			{
				throw new EntityUpdateConflict("The calculation " + id.ToString(CultureInfo.InvariantCulture)
											   + " is updated already.");
			}

			return new VersionData<CalculationState>
			{
				Id = id,
				RowVersion = result.RowVersion,
				State = state,
				StateTimestamp = result.StateTimestamp
			};
		}
	}
}