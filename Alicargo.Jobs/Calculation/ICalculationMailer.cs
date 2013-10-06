using Alicargo.Contracts.Contracts;

namespace Alicargo.Jobs.Calculation
{
	internal interface ICalculationMailer
	{
		void Send(CalculationData calculation);
	}
}