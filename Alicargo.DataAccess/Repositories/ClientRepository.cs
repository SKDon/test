using System;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Core.Contracts;
using Alicargo.Core.Helpers;
using Alicargo.Core.Models;
using Alicargo.Core.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	// todo: use contracts for the repository
	internal sealed class ClientRepository : BaseRepository, IClientRepository
	{
		private readonly Expression<Func<DbContext.Client, Client>> _selector;

		public ClientRepository(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			// todo: fix selector to use contract
			_selector = x => new Client
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
				TransitId = x.TransitId,

				Transit = new Transit
				{
					Id = x.TransitId,
					Address = x.Transit.Address,
					City = x.Transit.City,
					Phone = x.Transit.Phone,
					RecipientName = x.Transit.RecipientName,
					CarrierId = x.Transit.CarrierId,
					DeliveryTypeId = x.Transit.DeliveryTypeId,
					MethodOfTransitId = x.Transit.MethodOfTransitId,
					WarehouseWorkingTime = x.Transit.WarehouseWorkingTime,
					CarrierName = x.Transit.Carrier.Name
				},

				AuthenticationModel = new AuthenticationModel
				{
					Login = x.User.Login
				}
			};
		}

		public long Count()
		{
			return Context.Clients.LongCount();
		}

		public Client[] GetRange(long skip, int take)
		{
			return Context.Clients
				.OrderBy(x => x.LegalEntity)
				.Skip((int)skip)
				.Take(take)
				.Select(_selector)
				.ToArray();
		}

		public Func<long> Add(IClientData client)
		{
			var entity = new DbContext.Client();

			client.CopyTo(entity);

			Context.Clients.InsertOnSubmit(entity);

			return () => entity.Id;
		}

		public Client GetByUserId(long userId)
		{
			return Context.Clients.Select(_selector).FirstOrDefault(x => x.UserId == userId);
		}

		public Client GetById(long clientId)
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

		public Client[] GetAll()
		{
			return Context.Clients.Select(_selector).ToArray();
		}

		public void Update(long id, IClientData client)
		{
			var entity = Context.Clients.First(x => x.Id == id);

			client.CopyTo(entity);
		}

		public ClientData[] Get(params long[] clinentIds)
		{			
			return Context.Clients
				.Where(x => clinentIds.Contains(x.Id))
				.Select(_selector)
				.Cast<ClientData>()
				.ToArray();
		}
	}
}