using Alicargo.Contracts.Contracts;

namespace Alicargo.Core.Services.Abstract
{
	public interface IRecipients
	{
		RecipientData[] GetAdminEmails();
		RecipientData[] GetSenderEmails();
		RecipientData[] GetForwarderEmails();
	}
}