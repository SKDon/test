using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class CalculationRepository : ICalculationRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public CalculationRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public void Add(CalculationData data, long applicationId)
		{
			_executor.Execute("[dbo].[Calculation_Add]", new
			{
				data.ClientId,
				ApplicationHistoryId = applicationId,
				data.AirWaybillDisplay,
				data.ApplicationDisplay,
				data.MarkName,
				data.Weight,
				data.TariffPerKg,
				data.ScotchCost,
				data.InsuranceCost,
				data.FactureCost,
				data.TransitCost,
				data.PickupCost,
				data.FactoryName
			});
		}

		public CalculationData GetByApplication(long applicationId)
		{
			return _executor.Query<CalculationData>("[dbo].[Calculation_GetByApplication]", new { applicationId });
		}

		public decimal GetCalculatedSum()
		{
			return _executor.Query<decimal>("[dbo].[Calculation_GetCalculatedSum]");
		}

		public void RemoveByApplication(long applicationId)
		{
			_executor.Execute("[dbo].[Calculation_RemoveByApplication]", new { applicationId });
		}
	}
}