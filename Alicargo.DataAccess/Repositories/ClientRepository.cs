using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class ClientRepository : BaseRepository, IClientRepository
	{
		private readonly Expression<Func<Client, ClientData>> _selector;

		public ClientRepository(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			_selector = x => new ClientData
			{
				Id = x.Id,
				UserId = x.UserId,
				BIC = x.BIC,
				Phone = x.Phone,
				Email = x.Email,
				LegalEntity = x.LegalEntity,
				Bank = x.Bank,
				Contacts = x.Contacts,
				INN = x.INN,
				KPP = x.KPP,
				KS = x.KS,
				LegalAddress = x.LegalAddress,
				MailingAddress = x.MailingAddress,
				Nic = x.Nic,
				OGRN = x.OGRN,
				RS = x.RS,
				TransitId = x.TransitId
			};
		}

		public long Count()
		{
			return Context.Clients.LongCount();
		}

		public ClientData[] GetRange(int take, long skip)
		{
			return Context.Clients
						  .OrderBy(x => x.LegalEntity)
						  .Skip((int)skip)
						  .Take(take)
						  .Select(_selector)
						  .ToArray();
		}

		public Func<long> Add(ClientData client)
		{
			var entity = new Client();

			CopyTo(client, entity);

			Context.Clients.InsertOnSubmit(entity);

			return () => entity.Id;
		}

		public ClientData GetByUserId(long userId)
		{
			return Context.Clients.Select(_selector).FirstOrDefault(x => x.UserId == userId);
		}

		public ClientData GetById(long clientId)
		{
			return Context.Clients
						  .Where(x => x.Id == clientId)
						  .Select(_selector)
						  .FirstOrDefault();
		}

		public void Delete(long id)
		{
			var entity = Context.Clients.First(x => x.Id == id);

			Context.Clients.DeleteOnSubmit(entity);
		}

		public ClientData[] GetAll()
		{
			return Context.Clients.Select(_selector).ToArray();
		}

		public void Update(ClientData client)
		{
			var entity = Context.Clients.First(x => x.Id == client.Id);

			CopyTo(client, entity);
		}

		public ClientData[] Get(params long[] clinentIds)
		{
			return Context.Clients
						  .Where(x => clinentIds.Contains(x.Id))
						  .Select(_selector)
						  .ToArray();
		}

		// todo: 2. test
		public IDictionary<long, string> GetNicByApplications(params long[] appIds)
		{
			return Context.Applications
						  .Where(x => appIds.Contains(x.Id))
						  .Select(x => new { x.Id, ClientNic = x.Client.Nic })
						  .ToDictionary(x => x.Id, x => x.ClientNic);
		}

		public void SetCalculationExcel(long clientId, byte[] bytes)
		{
			var client = Context.Clients.First(x => x.Id == clientId);

			client.CalculationFileData = bytes;
		}

		public static void CopyTo(ClientData @from, Client to)
		{
			to.Email = @from.Email;
			to.LegalEntity = @from.LegalEntity;
			to.BIC = @from.BIC;
			to.Nic = @from.Nic;
			to.Contacts = @from.Contacts;
			to.Phone = @from.Phone;
			to.INN = @from.INN;
			to.KPP = @from.KPP;
			to.OGRN = @from.OGRN;
			to.Bank = @from.Bank;
			to.LegalAddress = @from.LegalAddress;
			to.MailingAddress = @from.MailingAddress;
			to.RS = @from.RS;
			to.KS = @from.KS;
			to.TransitId = @from.TransitId;
			to.UserId = @from.UserId;
		}
	}
}