using System.Configuration;
using System.Net.Configuration;
using Alicargo.Core.Contracts.Email;
using Alicargo.Core.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories.User;

namespace Alicargo.Core.Email
{
	public sealed class MailConfiguration : IMailConfiguration
	{
		private readonly ISenderRepository _senders;

		public MailConfiguration(ISenderRepository senders)
		{
			_senders = senders;
		}

		public SmtpSection GetConfiguration(long? userId)
		{
			SmtpSection section = null;

			if(userId.HasValue)
			{
				var senderId = _senders.GetByUserId(userId.Value);

				switch(senderId)
				{
					case 1: // alicargo
						section = GetSection("mailSettings/alicargo");
						break;

					case 3: // avion
						section = GetSection("mailSettings/avion");
						break;
				}
			}

			if(section == null)
			{
				section = GetSection("mailSettings/default");

				if(section.From == null)
				{
					throw new InvalidLogicException("Did not set From in email config " + section.SectionInformation.SectionName);
				}
			}

			return section ;
		}

		private static SmtpSection GetSection(string sectionName)
		{
			return (SmtpSection)ConfigurationManager.GetSection(sectionName);
		}
	}
}