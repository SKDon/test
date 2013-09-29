using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.Services.Contract;
using Alicargo.ViewModels.Helpers;
using Resources;

namespace Alicargo.Services.Calculation
{
	internal sealed class CalculationServiceSender : ICalculationService
	{
		private readonly IApplicationRepository _applications;
		private readonly ICalculationClientFile _clientFile;
		private readonly IClientRepository _clients;
		private readonly IMailSender _mailSender;
		private readonly IRecipients _recipients;

		public CalculationServiceSender(
			ICalculationClientFile clientFile,
			IRecipients recipients,
			IMailSender mailSender,
			IClientRepository clients,
			IApplicationRepository applications)
		{
			_clientFile = clientFile;
			_recipients = recipients;
			_mailSender = mailSender;
			_clients = clients;
			_applications = applications;
		}

		public void Calculate(long applicationId)
		{
			var application = _applications.Get(applicationId);

			var client = _clients.GetById(application.ClientId);

			SendEmail(application, client);
		}

		private void SendEmail(ApplicationData application, ClientData client)
		{
			var text = _clientFile.Build(application);

			var messages = new Message(
				string.Format(Mail.Calculation_Subject, ApplicationModelHelper.GetDisplayNumber(application.Id, application.Count)),
				text, client.Email)
			{
				CopyTo = _recipients.GetAdminEmails().Select(x => x.Email).ToArray()
			};

			_mailSender.Send(messages);
		}
	}
}