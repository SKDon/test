using Alicargo.Contracts.Contracts;
using Alicargo.Core.Contract;

namespace Alicargo.Jobs.Calculation
{
    public interface ICalculationMailBuilder
    {
        Message Build(CalculationData calculation);
    }
}