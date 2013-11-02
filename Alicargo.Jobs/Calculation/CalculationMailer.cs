using Alicargo.Contracts.Contracts;
using Alicargo.Core.Services;

namespace Alicargo.Jobs.Calculation
{
    public sealed class CalculationMailer : ICalculationMailer
    {
        private readonly IMailSender _mailSender;
        private readonly ICalculationMailBuilder _builder;

        public CalculationMailer(IMailSender mailSender, ICalculationMailBuilder builder)
        {
            _mailSender = mailSender;

            _builder = builder;
        }

        public void Send(CalculationData calculation)
        {
            var messages = _builder.Build(calculation);

            _mailSender.Send(messages);
        }
    }
}