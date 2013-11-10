using Alicargo.Contracts.Contracts;

namespace Alicargo.Core.Services.Abstract
{
	public interface IMailSender
	{
		void Send(params EmailMessage[] messages);
	}
}
