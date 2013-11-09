using Alicargo.Contracts.Contracts;
using Alicargo.Core.Contract;

namespace Alicargo.Core.Services.Abstract
{
	public interface IMailSender
	{
		void Send(params Message[] messages);
		string DefaultFrom { get; }
	}
}
