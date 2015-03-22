using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;

namespace Alicargo.DataAccess.Repositories.User
{
	public sealed class ClientRepository : IClientRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public ClientRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public long Add(ClientEditData client, long userId, long transitId)
		{
			return _executor.Query<long>("[dbo].[Client_Add]",
				new
				{
					client.BIC,
					client.Bank,
					client.Contacts,
					client.Emails,
					client.INN,
					client.KPP,
					client.KS,
					client.LegalAddress,
					client.LegalEntity,
					client.MailingAddress,
					client.Nic,
					client.OGRN,
					client.Phone,
					client.RS,
					UserId = userId,
					TransitId = transitId,
					client.ContractDate,
					client.ContractNumber
				});
		}

		public ClientData GetByUserId(long userId)
		{
			return _executor.Query<ClientData>("[dbo].[Client_GetByUserId]", new { userId });
		}

		public ClientData Get(long clientId)
		{
			return _executor.Query<ClientData>("[dbo].[Client_Get]", new { clientId });
		}

		public ClientData[] GetAll()
		{
			return _executor.Array<ClientData>("[dbo].[Client_GetAll]");
		}

		public void Update(long clientId, ClientEditData client)
		{
			_executor.Query<long>("[dbo].[Client_Update]",
				new
				{
					clientId,
					client.BIC,
					client.Bank,
					client.Contacts,
					client.Emails,
					client.INN,
					client.KPP,
					client.KS,
					client.LegalAddress,
					client.LegalEntity,
					client.MailingAddress,
					client.Nic,
					client.OGRN,
					client.Phone,
					client.RS,
					client.ContractDate,
					client.ContractNumber
				});
		}
	}
}