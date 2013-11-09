using Alicargo.Contracts.Contracts;
using Alicargo.Core.Models;

namespace Alicargo.Jobs.Calculation
{
    public interface ICalculationMailBuilder
    {
        EmailMessage Build(CalculationData calculation);
    }
}