using System;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Enums;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.Services.Contract;
using Alicargo.ViewModels;

namespace Alicargo.Services
{
	// todo: test
	public sealed class ClientService : IClientService
	{
		private readonly IIdentityService _identity;
		private readonly IClientRepository _clientRepository;
		private readonly ITransitService _transitService;
		private readonly IMailSender _mailSender;
		private readonly IMessageBuilder _messageBuilder;
		private readonly IAuthenticationRepository _authentications;
		private readonly IUnitOfWork _unitOfWork;

		public ClientService(
			IIdentityService identity,
			IClientRepository clientRepository,
			ITransitService transitService,
			IMailSender mailSender,
			IMessageBuilder messageBuilder,
			IAuthenticationRepository authentications,
			IUnitOfWork unitOfWork)
		{
			_identity = identity;
			_clientRepository = clientRepository;
			_transitService = transitService;
			_mailSender = mailSender;
			_messageBuilder = messageBuilder;
			_authentications = authentications;
			_unitOfWork = unitOfWork;
		}

		private bool HaveAccessToClient(long clientUserId)
		{
			if (_identity.IsInRole(RoleType.Admin) || _identity.IsInRole(RoleType.Sender)) return true;

			return clientUserId == _identity.Id;
		}

		public ClientData GetClientData(long? id = null)
		{
			ClientData data;

			if (id.HasValue)
			{
				data = _clientRepository.GetById(id.Value);
			}
			else if (_identity.Id.HasValue)
			{
				data = _clientRepository.GetByUserId(_identity.Id.Value);
			}
			else
			{
				return null;
			}

			if (!HaveAccessToClient(data.UserId))
				throw new AccessForbiddenException();

			return data;
		}

		public ListCollection<ClientData> GetList(int take, int skip)
		{
			var total = _clientRepository.Count();

			var data = _clientRepository.GetRange(skip, take).ToArray();

			return new ListCollection<ClientData> { Data = data, Total = total };
		}

		public void Update(long clientId, ClientModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel, AuthenticationModel authenticationModel)
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

		public long Add(ClientModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel, AuthenticationModel authenticationModel)
		{
			Func<long> id;
			using (var ts = _unitOfWork.StartTransaction())
			{
				var transitId = _transitService.AddTransit(transitModel, carrierModel);

				var userId = _authentications.Add(authenticationModel.Login, authenticationModel.NewPassword, _identity.TwoLetterISOLanguageName);

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

			EmailOnAdd(model, authenticationModel);

			return id();
		}

		private void EmailOnAdd(ClientModel model, AuthenticationModel authenticationModel)
		{
			var body = _messageBuilder.ClientAdd(model, authenticationModel);
			var admins = _messageBuilder.GetAdminEmails().Select(x => x.Email).ToArray();
			_mailSender.Send(new Message(_messageBuilder.DefaultSubject, body, model.Email) { CC = admins });
		}
	}
}