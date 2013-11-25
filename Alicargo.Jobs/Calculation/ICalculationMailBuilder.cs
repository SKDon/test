using Alicargo.Contracts.Contracts;

namespace Alicargo.Jobs.Calculation
{
    public interface ICalculationMailBuilder
    {
        EmailMessage Build(CalculationData calculation);
    }
}