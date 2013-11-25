using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services.Abstract;

namespace Alicargo.Services.Email
{
	internal sealed class Recipients : IRecipients
	{
		private readonly IUserRepository _users;

		public Recipients(IUserRepository users)
		{
			_users = users;
		}

		public RecipientData[] GetAdminEmails()
		{
			return _users.GetByRole(RoleType.Admin)
						 .Select(x => new RecipientData
						 {
							 Culture = x.TwoLetterISOLanguageName,
							 Email = x.Email
						 })
						 .ToArray();
		}

		public RecipientData[] GetSenderEmails()
		{
			return _users.GetByRole(RoleType.Sender)
						 .Select(x => new RecipientData
						 {
							 Culture = x.TwoLetterISOLanguageName,
							 Email = x.Email
						 })
						 .ToArray();
		}

		public RecipientData[] GetForwarderEmails()
		{
			return _users.GetByRole(RoleType.Forwarder)
						 .Select(x => new RecipientData
						 {
							 Culture = x.TwoLetterISOLanguageName,
							 Email = x.Email
						 })
						 .ToArray();
		}
	}
}