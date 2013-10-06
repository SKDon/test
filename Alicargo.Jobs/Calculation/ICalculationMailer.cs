using Alicargo.Contracts.Contracts;

namespace Alicargo.Jobs.Calculation
{
	public interface ICalculationMailer
	{
		void Send(CalculationData calculation);
	}
}