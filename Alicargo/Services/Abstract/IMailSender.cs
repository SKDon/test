using Alicargo.Services.Contract;

namespace Alicargo.Services.Abstract
{
	public interface IMailSender
	{
		void Send(params Message[] messages);
		string DefaultFrom { get; }
	}
}
