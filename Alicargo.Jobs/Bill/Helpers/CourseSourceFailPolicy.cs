using System;
using Alicargo.Core.Contracts.Email;
using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.Jobs.Bill.Helpers
{
	public sealed class CourseSourceFailPolicy : ICourseSource
	{
		private readonly ICourseSource _courseSource;
		private readonly string _fromEmail;
		private readonly IMailSender _sender;
		private readonly string _supportEmail;

		public CourseSourceFailPolicy(ICourseSource courseSource, IMailSender sender, string fromEmail, string supportEmail)
		{
			_supportEmail = supportEmail;
			_fromEmail = fromEmail;
			_courseSource = courseSource;
			_sender = sender;
		}

		public decimal GetEuroToRuble(string url)
		{
			try
			{
				return _courseSource.GetEuroToRuble(url);
			}
			catch(Exception e)
			{
				if(!string.IsNullOrWhiteSpace(_supportEmail))
				{
					var body = "Не удалось обновить курс евро из " + url + Environment.NewLine + e;
					var message = new EmailMessage("Alicargo. Ошибка обновления курса", body, _fromEmail, _supportEmail);

					_sender.Send(message);
				}

				throw;
			}
		}
	}
}