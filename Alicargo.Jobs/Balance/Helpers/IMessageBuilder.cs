using Alicargo.Contracts.Contracts;

namespace Alicargo.Jobs.Balance.Helpers
{
	internal interface IMessageBuilder
	{
		EmailMessage[] Get();
	}
}
