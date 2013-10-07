using System.Linq;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Contract;
using Alicargo.Core.Services;

namespace Alicargo.Services.Email
{
	internal sealed class Recipients : IRecipients
	{
		private readonly IUserRepository _users;

		public Recipients(IUserRepository users)
		{
			_users = users;
		}

		public Recipient[] GetAdminEmails()
		{
			return _users.GetByRole(RoleType.Admin)
						 .Select(x => new Recipient
						 {
							 Culture = x.TwoLetterISOLanguageName,
							 Email = x.Email
						 })
						 .ToArray();
		}

		public Recipient[] GetSenderEmails()
		{
			return _users.GetByRole(RoleType.Sender)
						 .Select(x => new Recipient
						 {
							 Culture = x.TwoLetterISOLanguageName,
							 Email = x.Email
						 })
						 .ToArray();
		}

		public Recipient[] GetForwarderEmails()
		{
			return _users.GetByRole(RoleType.Forwarder)
						 .Select(x => new Recipient
						 {
							 Culture = x.TwoLetterISOLanguageName,
							 Email = x.Email
						 })
						 .ToArray();
		}
	}
}