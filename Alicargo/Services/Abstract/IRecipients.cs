using Alicargo.Services.Contract;

namespace Alicargo.Services.Abstract
{
	public interface IRecipients
	{
		Recipient[] GetAdminEmails();
		Recipient[] GetSenderEmails();
		Recipient[] GetForwarderEmails();
	}
}