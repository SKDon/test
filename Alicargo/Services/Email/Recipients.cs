using System.Linq;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Contract;
using Alicargo.Core.Services;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.Email
{
	internal sealed class Recipients : IRecipients
	{
		private readonly IUserRepository _userRepository;

		public Recipients(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public Recipient[] GetAdminEmails()
		{
			return _userRepository.GetByRole(RoleType.Admin)
								  .Select(x => new Recipient
								  {
									  Culture = x.TwoLetterISOLanguageName,
									  Email = x.Email
								  })
								  .ToArray();
		}

		public Recipient[] GetSenderEmails()
		{
			return _userRepository.GetByRole(RoleType.Sender)
								  .Select(x => new Recipient
								  {
									  Culture = x.TwoLetterISOLanguageName,
									  Email = x.Email
								  })
								  .ToArray();
		}

		public Recipient[] GetForwarderEmails()
		{
			return _userRepository.GetByRole(RoleType.Forwarder)
								  .Select(x => new Recipient
								  {
									  Culture = x.TwoLetterISOLanguageName,
									  Email = x.Email
								  })
								  .ToArray();
		}
	}
}