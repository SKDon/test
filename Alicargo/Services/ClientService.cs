using System.Linq;
using Alicargo.Core.Enums;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Models;
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

		public ClientService(IIdentityService identity, IClientRepository clientRepository,
			 ITransitService transitService, IMailSender mailSender, IMessageBuilder messageBuilder,
			IAuthenticationRepository authentications, IUnitOfWork unitOfWork)
		{
			_identity = identity;
			_clientRepository = clientRepository;
			_transitService = transitService;
			_mailSender = mailSender;
			_messageBuilder = messageBuilder;
			_authentications = authentications;
			_unitOfWork = unitOfWork;
		}

		private bool HaveAccessToClient(IClientData client)
		{
			if (client == null)
				throw new EntityNotFoundException();

			if (_identity.IsInRole(RoleType.Admin) || _identity.IsInRole(RoleType.Sender)) return true;

			return client.UserId == _identity.Id;
		}

		public Client GetClient(long? id)
		{
			var client = id.HasValue
				? _clientRepository.GetById(id.Value)
				: _identity.Id.HasValue
					? _clientRepository.GetByUserId(_identity.Id.Value)
					: null;

			if (!HaveAccessToClient(client))
				throw new AccessForbiddenException();

			return client;
		}

		public ListCollection<Client> GetList(int take, int skip)
		{
			var total = _clientRepository.Count();

			var data = _clientRepository.GetRange(skip, take)
				.ToArray();

			return new ListCollection<Client> { Data = data, Total = total };
		}

		public void Update(Client model, CarrierSelectModel carrierSelectModel)
		{
			if (!HaveAccessToClient(model))
				throw new AccessForbiddenException();

			using (var ts = _unitOfWork.StartTransaction())
			{
				
				//_transitService.Update(model.Transit, carrierSelectModel); // todo: fix

				_authentications.Update(model.UserId, model.AuthenticationModel.Login, model.AuthenticationModel.NewPassword);

				_clientRepository.Update(model.Id, model);

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}

		public void Add(Client model, CarrierSelectModel carrierSelectModel)
		{
			if (!HaveAccessToClient(model))
				throw new AccessForbiddenException();

			using (var ts = _unitOfWork.StartTransaction())
			{
				model.TransitId = _transitService.AddTransit(model.Transit, carrierSelectModel);

				var userId = _authentications.Add(model.AuthenticationModel.Login, model.AuthenticationModel.NewPassword, _identity.TwoLetterISOLanguageName);

				_unitOfWork.SaveChanges();

				model.UserId = userId();

				var id = _clientRepository.Add(model);

				_unitOfWork.SaveChanges();

				model.Id = id();

				ts.Complete();
			}

			EmailOnAdd(model);
		}

		private void EmailOnAdd(Client model)
		{
			var body = _messageBuilder.ClientAdd(model);
			var admins = _messageBuilder.GetAdminEmails().Select(x => x.Email).ToArray();
			_mailSender.Send(new Message(_messageBuilder.DefaultSubject, body, model.Email) { CC = admins });
		}
	}
}