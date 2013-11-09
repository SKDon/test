using Alicargo.Contracts.Contracts;
using Alicargo.Core.Models;

namespace Alicargo.Core.Services.Abstract
{
	public interface IMailSender
	{
		void Send(params EmailMessage[] messages);
		string DefaultFrom { get; }
	}
}
