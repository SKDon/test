using System.Configuration;
using System.IO;
using System.Net.Mail;
using Alicargo.Services.Abstract;
using Alicargo.Services.Contract;

namespace Alicargo.Services.Email
{
	public sealed class MailSender : IMailSender
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

						if (message.CC != null)
						{
							foreach (var to in message.CC)
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
								var stream = new MemoryStream(file.FileData);
								email.Attachments.Add(new Attachment(stream, file.FileName));
							}
						}

						smtpClient.Send(email);
					}
				}
			}
		}

		// todo: dont like it
		public string DefaultFrom
		{
			get
			{
				return ConfigurationManager.AppSettings["DefaultFrom"];
			}
		}
	}
}