using System;
using Alicargo.Core.Contracts.Email;
using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.Jobs.Bill.Helpers
{
	public sealed class CourseSourceFailPolicy : ICourseSource
	{
		private readonly ICourseSource _courseSource;
		private readonly IMailSender _sender;

		public CourseSourceFailPolicy(ICourseSource courseSource, IMailSender sender)
		{
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
				//_sender.Send(new EmailMessage("Alicargo. Ошибка", "Не удалось обновить курс евро по ",));

				throw;
			}
		}
	}
}
