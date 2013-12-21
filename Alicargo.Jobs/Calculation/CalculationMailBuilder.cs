﻿using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Services.Abstract;

namespace Alicargo.Jobs.Calculation
{
	public sealed class CalculationMailBuilder : ICalculationMailBuilder
	{
		private readonly IClientRepository _clients;
		private readonly IRecipients _recipients;
		private readonly string _from;

		public CalculationMailBuilder(IClientRepository clients, IRecipients recipients, string from)
		{
			_clients = clients;
			_recipients = recipients;
			_from = @from;
		}

		public EmailMessage Build(CalculationData calculation)
		{
			var client = _clients.Get(calculation.ClientId);
			var cost = calculation.TariffPerKg * (decimal)calculation.Weight;
			var total = cost + calculation.ScotchCost + calculation.FactureCost
						+ calculation.InsuranceCost + calculation.TransitCost + calculation.PickupCost;

			var text = string.Format(Resources.Calculation_Body, // format
				calculation.AirWaybillDisplay, // 0
				calculation.Weight.ToString("N2"), // 1
				calculation.TariffPerKg.ToString("N2"), // 2
				cost.ToString("N2"), // 3
				calculation.ScotchCost.ToString("N2"), // 4
				calculation.InsuranceCost.ToString("N2"), // 5
				calculation.FactureCost.ToString("N2"), // 6
				calculation.TransitCost.ToString("N2"), // 7
				calculation.PickupCost.ToString("N2"), // 8
				total.ToString("N2")); // 9

			var subject = string.Format(Resources.Calculation_Subject,
				calculation.ApplicationDisplay,
				calculation.FactoryName,
				calculation.MarkName);

			return new EmailMessage(subject, text, _from, client.Emails)
			{
				CopyTo = _recipients.GetAdminEmails().Select(x => x.Email).ToArray()
			};
		}
	}
}