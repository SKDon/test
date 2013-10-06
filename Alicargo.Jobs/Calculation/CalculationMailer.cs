using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Contract;
using Alicargo.Core.Services;

namespace Alicargo.Jobs.Calculation
{
	internal sealed class CalculationMailer : IJob<VersionedData<CalculationState, CalculationData>>
	{
		private readonly IClientRepository _clients;
		private readonly IMailSender _mailSender;
		private readonly IRecipients _recipients;

		public CalculationMailer(IMailSender mailSender, IRecipients recipients, IClientRepository clients)
		{
			_mailSender = mailSender;
			_recipients = recipients;
			_clients = clients;
		}

		public void Run(VersionedData<CalculationState, CalculationData> data)
		{
			var calculation = data.Data;
			var client = _clients.Get(calculation.ClientId).First();
			var cost = calculation.TariffPerKg * (decimal)calculation.Weight;
			var total = cost + calculation.ScotchCost + calculation.FactureCost + calculation.InsuranceCost;

			var text = string.Format(Resources.Calculation_Body,
									 calculation.AirWaybillDisplay,
									 calculation.ApplicationDisplay,
									 calculation.MarkName,
									 calculation.Weight.ToString("N2"),
									 calculation.TariffPerKg.ToString("N2"),
									 cost.ToString("N2"),
									 calculation.ScotchCost.ToString("N2"),
									 calculation.InsuranceCost.ToString("N2"),
									 calculation.FactureCost.ToString("N2"),
									 total.ToString("N2"));

			var messages = new Message(string.Format(Resources.Calculation_Subject, calculation.ApplicationDisplay),
									   text, client.Email)
			{
				CopyTo = _recipients.GetAdminEmails().Select(x => x.Email).ToArray()
			};

			_mailSender.Send(messages);
		}
	}
}