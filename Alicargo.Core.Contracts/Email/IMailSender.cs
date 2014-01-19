using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.Core.Contracts.Email
{
	public interface IMailSender
	{
		void Send(params EmailMessage[] messages);
	}
}
