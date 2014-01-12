using Alicargo.Contracts.Contracts;

namespace Alicargo.Core.Contracts
{
	public interface IMailSender
	{
		void Send(params EmailMessage[] messages);
	}
}
