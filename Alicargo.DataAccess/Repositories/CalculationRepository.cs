using System.Linq;
using Alicargo.Contracts.Contracts;
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
				FactoryName = data.FactoryName,
				ScotchCost = data.ScotchCost,
				TariffPerKg = data.TariffPerKg,
				Weight = data.Weight,
				ApplicationHistoryId = applicationId,
				TransitCost = data.TransitCost,
				PickupCost = data.PickupCost
			});
		}

		public CalculationData[] GetByClientId(long clientId)
		{
			return _context.Calculations.Where(x => x.ClientId == clientId)
				.OrderBy(x => x.ApplicationDisplay)
				.Select(x =>
					new CalculationData
					{
						AirWaybillDisplay = x.AirWaybillDisplay,
						ApplicationDisplay = x.ApplicationDisplay,
						ClientId = x.ClientId,
						FactureCost = x.FactureCost,
						TransitCost = x.TransitCost,
						InsuranceCost = x.InsuranceCost,
						MarkName = x.MarkName,
						FactoryName = x.FactoryName,
						ScotchCost = x.ScotchCost,
						TariffPerKg = x.TariffPerKg,
						Weight = x.Weight,
						PickupCost = x.PickupCost
					}).ToArray();
		}

		public void RemoveByApplication(long applicationId)
		{
			var calculation = _context.Calculations.First(x => x.ApplicationHistoryId == applicationId);

			_context.Calculations.DeleteOnSubmit(calculation);
		}
	}
}