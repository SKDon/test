using System;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Services
{
	// todo: test
	public sealed class ClientManager : IClientManager
	{
		private readonly IAuthenticationRepository _authentications;
		private readonly IClientRepository _clientRepository;
		private readonly IIdentityService _identity;
		private readonly ITransitService _transitService;
		private readonly IUnitOfWork _unitOfWork;

		public ClientManager(
			IIdentityService identity,
			IClientRepository clientRepository,
			ITransitService transitService,
			IAuthenticationRepository authentications,
			IUnitOfWork unitOfWork)
		{
			_identity = identity;
			_clientRepository = clientRepository;
			_transitService = transitService;
			_authentications = authentications;
			_unitOfWork = unitOfWork;
		}

		public void Update(long clientId, ClientModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel,
						   AuthenticationModel authenticationModel)
		{
			var data = _clientRepository.Get(clientId).First();

			if (!HaveAccessToClient(data.UserId))
				throw new AccessForbiddenException();

			using (var ts = _unitOfWork.StartTransaction())
			{
				_transitService.Update(data.TransitId, transitModel, carrierModel);

				_authentications.Update(data.UserId, authenticationModel.Login, authenticationModel.NewPassword);

				data.BIC = model.BIC;
				data.Phone = model.Phone;
				data.Email = model.Email;
				data.LegalEntity = model.LegalEntity;
				data.Bank = model.Bank;
				data.Contacts = model.Contacts;
				data.INN = model.INN;
				data.KPP = model.KPP;
				data.KS = model.KS;
				data.LegalAddress = model.LegalAddress;
				data.MailingAddress = model.MailingAddress;
				data.Nic = model.Nic;
				data.OGRN = model.OGRN;
				data.RS = model.RS;

				_clientRepository.Update(data);

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}

		public long Add(ClientModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel,
						AuthenticationModel authenticationModel)
		{
			Func<long> id;
			using (var ts = _unitOfWork.StartTransaction())
			{
				var transitId = _transitService.AddTransit(transitModel, carrierModel);

				var userId = _authentications.Add(authenticationModel.Login, authenticationModel.NewPassword,
												  _identity.TwoLetterISOLanguageName);

				_unitOfWork.SaveChanges();

				var data = new ClientData
				{
					UserId = userId(),
					BIC = model.BIC,
					Phone = model.Phone,
					Email = model.Email,
					LegalEntity = model.LegalEntity,
					Bank = model.Bank,
					Contacts = model.Contacts,
					INN = model.INN,
					KPP = model.KPP,
					KS = model.KS,
					LegalAddress = model.LegalAddress,
					MailingAddress = model.MailingAddress,
					Nic = model.Nic,
					OGRN = model.OGRN,
					RS = model.RS,
					TransitId = transitId
				};

				id = _clientRepository.Add(data);

				_unitOfWork.SaveChanges();

				ts.Complete();
			}

			return id();
		}

		public bool HaveAccessToClient(long clientUserId)
		{
			if (_identity.IsInRole(RoleType.Admin) || _identity.IsInRole(RoleType.Sender)) return true;

			return clientUserId == _identity.Id;
		}
	}
}