using Alicargo.Core.Contract;

namespace Alicargo.Core.Services
{
	public interface IMailSender
	{
		void Send(params Message[] messages);
		string DefaultFrom { get; }
	}
}
