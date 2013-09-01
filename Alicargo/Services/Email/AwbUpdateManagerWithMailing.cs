using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.Services.Contract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.Email
{
    internal sealed class AwbUpdateManagerWithMailing : IAwbUpdateManager
    {
        private readonly IAwbPresenter _awbPresenter;
        private readonly IAwbRepository _awbRepository;
        private readonly IMailSender _mailSender;
        private readonly IMessageBuilder _messageBuilder;
        private readonly IAwbUpdateManager _updateManager;

        public AwbUpdateManagerWithMailing(
            IAwbUpdateManager updateManager,
            IAwbPresenter awbPresenter,
            IAwbRepository awbRepository,
            IMailSender mailSender,
            IMessageBuilder messageBuilder)
        {
            _updateManager = updateManager;
            _awbPresenter = awbPresenter;
            _awbRepository = awbRepository;
            _mailSender = mailSender;
            _messageBuilder = messageBuilder;
        }

        public void Update(long id, AirWaybillEditModel model)
        {
            var old = _awbPresenter.GetData(id);

            _updateManager.Update(id, model);

            SendOnFileAdd(id, old);
        }

        public void Update(long id, BrockerAWBModel model)
        {
            var old = _awbPresenter.GetData(id);

            _updateManager.Update(id, model);

            SendOnFileAdd(id, old);
        }

        private void SendOnFileAdd(long id, AirWaybillData oldData)
        {
            var model = _awbPresenter.GetData(id);

            var subject = _messageBuilder.DefaultSubject;
            var brocker = _awbPresenter.GetBrocker(model.BrockerId);

            if (oldData.InvoiceFileName == null && model.InvoiceFileName != null)
            {
                var body = _messageBuilder.AwbInvoiceFileAdded(model);
                var to = _messageBuilder.GetSenderEmails()
                                        .Concat(_messageBuilder.GetAdminEmails())
                                        .Select(x => x.Email)
                                        .ToArray();
                var file = _awbRepository.GetInvoiceFile(model.Id);
                _mailSender.Send(new Message(subject, body, to) {Files = new[] {file}});
            }

            if (oldData.AWBFileName == null && model.AWBFileName != null)
            {
                var body = _messageBuilder.AwbAWBFileAdded(model);
                var to = _messageBuilder.GetSenderEmails()
                                        .Concat(_messageBuilder.GetAdminEmails())
                                        .Select(x => x.Email)
                                        .Concat(new[] {brocker.Email})
                                        .ToArray();
                var file = _awbRepository.GetAWBFile(model.Id);

                _mailSender.Send(new Message(subject, body, to) {Files = new[] {file}});
            }

            if (oldData.PackingFileName == null && model.PackingFileName != null)
            {
                var body = _messageBuilder.AwbPackingFileAdded(model);
                var to = new[] {brocker.Email}.Concat(_messageBuilder.GetAdminEmails().Select(x => x.Email)).ToArray();
                var file = _awbRepository.GetPackingFile(model.Id);

                _mailSender.Send(new Message(subject, body, to) {Files = new[] {file}});
            }

            if (oldData.GTDFileName == null && model.GTDFileName != null)
            {
                var body = _messageBuilder.AwbGTDFileAdded(model);
                var file = _awbRepository.GetGTDFile(model.Id);
                foreach (var client in _awbRepository.GetClientEmails(model.Id))
                {
                    _mailSender.Send(new Message(subject, body, client) {Files = new[] {file}});
                }
                _mailSender.Send(new Message(subject, body,
                                             _messageBuilder.GetAdminEmails().Select(x => x.Email).ToArray())
                    {
                        Files = new[] {file}
                    });
            }

            if (oldData.GTDAdditionalFileName == null && model.GTDAdditionalFileName != null)
            {
                var body = _messageBuilder.AwbGTDAdditionalFileAdded(model);
                var file = _awbRepository.GTDAdditionalFile(model.Id);
                foreach (var client in _awbRepository.GetClientEmails(model.Id))
                {
                    _mailSender.Send(new Message(subject, body, client) {Files = new[] {file}});
                }
                _mailSender.Send(new Message(subject, body,
                                             _messageBuilder.GetAdminEmails().Select(x => x.Email).ToArray())
                    {
                        Files = new[] {file}
                    });
            }
        }
    }
}