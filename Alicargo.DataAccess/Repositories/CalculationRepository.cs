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
	internal sealed class CalculationRepository : BaseRepository, ICalculationRepository
	{
		public CalculationRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

		public void Add(CalculationData data)
		{
			Context.Calculations.InsertOnSubmit(new Calculation
			{
				AirWaybillDisplay = data.AirWaybillDisplay,
				ApplicationDisplay = data.ApplicationDisplay,
				Client = null,
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
				Weight = data.Weight
			});
		}

		public VersionedData<CalculationState, CalculationData>[] Get(CalculationState state)
		{
			return Context.Calculations.Where(x => x.StateId == (int)state)
						  .OrderBy(x => x.StateIdTimestamp)
						  .Select(x => new VersionedData<CalculationState, CalculationData>
						  {
							  State = (CalculationState)x.StateId,
							  StateTimestamp = x.StateIdTimestamp,
							  Id = x.Id,
							  RowVersion = x.RowVersion.ToArray(),
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

		// todo: 1. test
		public void SetState(long id, byte[] rowVersion, CalculationState state)
		{
			var calculation = Context.Calculations.First(x => x.Id == id);

			if (calculation.RowVersion != rowVersion)
			{
				throw new EntityUpdateConflict("The calculation " + id.ToString(CultureInfo.InvariantCulture)
											   + " is updated already.");
			}

			calculation.StateId = (int)state;
			calculation.StateIdTimestamp = DateTimeOffset.UtcNow;
		}
	}
}