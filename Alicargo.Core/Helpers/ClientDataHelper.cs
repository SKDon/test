using Alicargo.Core.Contracts;
using Alicargo.Core.Models;

namespace Alicargo.Core.Helpers
{
	// todo: test
	public static class ClientDataHelper
	{
		public static void CopyTo(this IClientData @from, IClientData to)
		{
			if (to.Id == 0)
				to.Id = @from.Id;

			to.Email = @from.Email;
			to.LegalEntity = @from.LegalEntity;
			to.BIC = @from.BIC;
			to.Nic = @from.Nic;
			to.Contacts = @from.Contacts;
			to.Phone = @from.Phone;
			to.INN = @from.INN;
			to.KPP = @from.KPP;
			to.OGRN = @from.OGRN;
			to.Bank = @from.Bank;
			to.LegalAddress = @from.LegalAddress;
			to.MailingAddress = @from.MailingAddress;
			to.RS = @from.RS;
			to.KS = @from.KS;
			to.TransitId = @from.TransitId;
			to.UserId = @from.UserId;
		}
	}
}