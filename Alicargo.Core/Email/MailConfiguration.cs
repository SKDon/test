using System.Configuration;
using System.Net.Configuration;
using Alicargo.Core.Contracts.Email;
using Alicargo.Core.Contracts.Exceptions;

namespace Alicargo.Core.Email
{
	public sealed class MailConfiguration : IMailConfiguration
	{
		public SmtpSection GetConfiguration(long? userId)
		{
			SmtpSection section = null;

			if(userId.HasValue)
			{
				section = GetSection("mailSettings/user" + userId.Value);
			}

			if(section == null)
			{
				section = GetSection("mailSettings/default");
			}

			if(section == null || section.From == null)
			{
				throw new InvalidLogicException(
					string.Format("Invalid mail settings (User Id: {0})", userId.HasValue ? userId.ToString() : "NULL"));
			}

			return section;
		}

		private static SmtpSection GetSection(string sectionName)
		{
			return (SmtpSection)ConfigurationManager.GetSection(sectionName);
		}
	}
}