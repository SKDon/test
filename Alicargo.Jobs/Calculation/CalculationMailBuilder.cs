using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Contract;
using Alicargo.Core.Services;

namespace Alicargo.Jobs.Calculation
{
    internal sealed class CalculationMailBuilder : ICalculationMailBuilder
    {
        private readonly IClientRepository _clients;
        private readonly IRecipients _recipients;

        public CalculationMailBuilder(IClientRepository clients, IRecipients recipients)
        {
            _clients = clients;
            _recipients = recipients;
        }

        public Message Build(CalculationData calculation)
        {
            var client = _clients.Get(calculation.ClientId).First();
            var cost = calculation.TariffPerKg * (decimal)calculation.Weight;
            var total = cost + calculation.ScotchCost + calculation.FactureCost + calculation.InsuranceCost +
                            calculation.TransitCost + calculation.PickupCost;

            var text = string.Format(Resources.Calculation_Body, // format
                calculation.AirWaybillDisplay, // 0
                calculation.ApplicationDisplay, // 1
                calculation.MarkName, // 2
                calculation.Weight.ToString("N2"), // 3
                calculation.TariffPerKg.ToString("N2"), // 4
                cost.ToString("N2"), // 5
                calculation.ScotchCost.ToString("N2"), // 6
                calculation.InsuranceCost.ToString("N2"), // 7
                calculation.FactureCost.ToString("N2"), // 8
                calculation.TransitCost.ToString("N2"), // 9
                calculation.PickupCost.ToString("N2"), // 10
                total.ToString("N2")); // 11

            return new Message(string.Format(Resources.Calculation_Subject, calculation.ApplicationDisplay),
                text, client.Email)
            {
                CopyTo = _recipients.GetAdminEmails().Select(x => x.Email).ToArray()
            };
        }
    }
}