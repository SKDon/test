using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;

namespace Alicargo.DataAccess.Repositories.Application
{
	internal sealed class BillRepository : IBillRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public BillRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public void AddOrReplace(long applicationId, BillData data)
		{
			_executor.Execute("[dbo].[Bill_AddOrReplace]",
				new
				{
					applicationId,
					data.Accountant,
					data.Bank,
					data.BIC,
					data.Client,
					data.CorrespondentAccount,
					data.Count,
					data.CurrentAccount,
					data.Goods,
					data.Head,
					data.HeaderText,
					data.Payee,
					data.Price,
					data.Shipper,
					data.TaxRegistrationReasonCode,
					data.TIN,
					data.VAT,
					data.EuroToRuble,
					data.Number,
					data.SaveDate,
					data.SendDate
				});
		}

		public BillData Get(long applicationId)
		{
			return _executor.Query<BillData>("[dbo].[Bill_GetByApplicationId]", new { applicationId });
		}
	}
}