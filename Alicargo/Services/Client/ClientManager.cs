using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Client
{
    internal sealed class ClientManager : IClientManager
    {
        private readonly IAuthenticationRepository _authentications;
        private readonly IClientRepository _clientRepository;
        private readonly IClientPermissions _clientPermissions;
        private readonly IIdentityService _identity;
        private readonly ITransitService _transitService;
        private readonly IUnitOfWork _unitOfWork;

        public ClientManager(
            IIdentityService identity,
            IClientRepository clientRepository,
            IClientPermissions clientPermissions,
            ITransitService transitService,
            IAuthenticationRepository authentications,
            IUnitOfWork unitOfWork)
        {
            _identity = identity;
            _clientRepository = clientRepository;
            _clientPermissions = clientPermissions;
            _transitService = transitService;
            _authentications = authentications;
            _unitOfWork = unitOfWork;
        }

        public void Update(long clientId, ClientModel model, CarrierSelectModel carrierModel,
                           TransitEditModel transitModel,
                           AuthenticationModel authenticationModel)
        {
            var data = _clientRepository.Get(clientId).First();

            if (!_clientPermissions.HaveAccessToClient(data))
                throw new AccessForbiddenException();

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
        }

        public long Add(ClientModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel,
                        AuthenticationModel authenticationModel)
        {
            var transitId = _transitService.AddTransit(transitModel, carrierModel);

            var userId = _authentications.Add(authenticationModel.Login, authenticationModel.NewPassword,
                                              _identity.TwoLetterISOLanguageName);

            _unitOfWork.SaveChanges();
            
            var data = new ClientData
                {
					Id = 0,
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

            var id = _clientRepository.Add(data);

            _unitOfWork.SaveChanges();

            return id();
        }		
    }
}