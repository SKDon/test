using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
			var section = _configuration.GetConfiguration(userId);

			using(var smtpClient = new SmtpClient(section.Network.Host, section.Network.Port)
			{
				Credentials = new NetworkCredential(
					section.Network.UserName, section.Network.Password, section.Network.ClientDomain),
				EnableSsl = section.Network.EnableSsl,
				UseDefaultCredentials = section.Network.DefaultCredentials,
				DeliveryFormat = section.DeliveryFormat,
				DeliveryMethod = section.DeliveryMethod,
			})
			{
				if(section.Network.TargetName != null)
					smtpClient.TargetName = section.Network.TargetName;

				if(section.SpecifiedPickupDirectory != null && section.SpecifiedPickupDirectory.PickupDirectoryLocation != null)
					smtpClient.PickupDirectoryLocation = section.SpecifiedPickupDirectory.PickupDirectoryLocation;

				foreach(var message in messages)
				{
					using(var email = new MailMessage())
					{
						email.From = new MailAddress(section.From ?? message.From, message.From);
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
}