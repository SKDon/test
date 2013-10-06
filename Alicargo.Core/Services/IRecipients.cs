using Alicargo.Core.Contract;

namespace Alicargo.Core.Services
{
	public interface IRecipients
	{
		Recipient[] GetAdminEmails();
		Recipient[] GetSenderEmails();
		Recipient[] GetForwarderEmails();
	}
}