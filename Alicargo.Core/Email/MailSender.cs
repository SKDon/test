using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using Alicargo.Core.Contracts.Email;
using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.Core.Email
{
	public sealed class MailSender : IMailSender
	{
		private readonly IMailConfiguration _configuration;

		public MailSender(IMailConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void Send(params EmailMessage[] messages)
		{
			foreach(var group in messages.GroupBy(x => x.EmailSenderUserId))
			{
				Send(group, group.Key);
			}
		}

		private void Send(IEnumerable<EmailMessage> messages, long? userId)
		{
			using(var smtpClient = new SmtpClient())
			{
				var section = Configure(smtpClient, userId);

				Send(smtpClient, section.From, messages);
			}
		}

		private SmtpSection Configure(SmtpClient smtpClient, long? userId)
		{
			var section = _configuration.GetConfiguration(userId);

			smtpClient.DeliveryMethod = section.DeliveryMethod;

			smtpClient.DeliveryFormat = section.DeliveryFormat;

			var specifiedPickupDirectory = section.SpecifiedPickupDirectory;

			var useFolder = specifiedPickupDirectory != null
			                && !string.IsNullOrWhiteSpace(specifiedPickupDirectory.PickupDirectoryLocation);

			if(useFolder)
			{
				if(!Directory.Exists(specifiedPickupDirectory.PickupDirectoryLocation))
				{
					Directory.CreateDirectory(specifiedPickupDirectory.PickupDirectoryLocation);
				}

				smtpClient.PickupDirectoryLocation = specifiedPickupDirectory.PickupDirectoryLocation;
			}
			else
			{
				smtpClient.Host = section.Network.Host;

				smtpClient.Port = section.Network.Port;

				smtpClient.UseDefaultCredentials = section.Network.DefaultCredentials;

				smtpClient.Credentials = new NetworkCredential(section.Network.UserName, section.Network.Password);

				smtpClient.EnableSsl = section.Network.EnableSsl;

				if(section.Network.TargetName != null)
				{
					smtpClient.TargetName = section.Network.TargetName;
				}
			}

			return section;
		}

		private static void Send(SmtpClient smtpClient, string addressFrom, IEnumerable<EmailMessage> messages)
		{
			foreach(var message in messages)
			{
				using(var email = new MailMessage())
				{
					email.From = new MailAddress(addressFrom);
					foreach(var to in message.To)
					{
						email.To.Add(to);
					}

					if(message.CopyTo != null)
					{
						foreach(var to in message.CopyTo)
						{
							email.CC.Add(to);
						}
					}

					email.Subject = message.Subject;
					email.Body = message.Body;

					email.IsBodyHtml = message.IsBodyHtml;
					if(message.Files != null)
					{
						foreach(var file in message.Files)
						{
							var stream = new MemoryStream(file.Data);
							email.Attachments.Add(new Attachment(stream, file.Name));
						}
					}

					smtpClient.Send(email);
				}
			}
		}
	}
}