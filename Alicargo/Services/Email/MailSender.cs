using System.Configuration;
using System.IO;
using System.Net.Mail;
using Alicargo.Core.Contract;
using Alicargo.Core.Services;

namespace Alicargo.Services.Email
{
    internal sealed class MailSender : IMailSender
	{
		public void Send(params Message[] messages)
		{
			using (var smtpClient = new SmtpClient())
			{
				foreach (var message in messages)
				{
					using (var email = new MailMessage())
					{
						email.From = new MailAddress(message.From ?? DefaultFrom);
						foreach (var to in message.To)
						{
							email.To.Add(to);
						}

						if (message.CopyTo != null)
						{
							foreach (var to in message.CopyTo)
							{
								email.CC.Add(to);
							}
						}

						email.Subject = message.Subject;
						email.Body = message.Body;

						email.IsBodyHtml = false;
						if (message.Files != null)
						{
							foreach (var file in message.Files)
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

		// todo: 3. don't like that the property is defined by an app setting
		public string DefaultFrom
		{
			get
			{
				return ConfigurationManager.AppSettings["DefaultFrom"];
			}
		}
	}
}