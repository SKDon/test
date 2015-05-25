using System;
using System.Text;
using System.Web.Mvc;
using System.Web.SessionState;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.Exceptions;
using Alicargo.Core.Email;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.ViewModels;

namespace Alicargo.Controllers
{
	[SessionState(SessionStateBehavior.Disabled)]
	public partial class HomeController : Controller
	{
		private readonly IIdentityService _identityService;
		private readonly SilentMailSender _sender;

		public HomeController(IIdentityService identityService, SilentMailSender sender)
		{
			_identityService = identityService;
			_sender = sender;
		}

		public virtual ActionResult Index()
		{
			if(_identityService.IsAuthenticated)
			{
				if(_identityService.IsInRole(RoleType.Broker))
				{
					return RedirectToAction(MVC.AirWaybill.Index());
				}

				if(_identityService.IsInRole(RoleType.Forwarder))
				{
					return RedirectToAction(MVC.Forwarder.Applications.Index());
				}

				return RedirectToAction(MVC.ApplicationList.Index());
			}

			return Redirect("/index.html");
		}

		public virtual RedirectResult Culture(string id, string returnUrl)
		{
			_identityService.SetLanguage(id);

			return Redirect(returnUrl);
		}

		[HttpPost]
		public virtual ActionResult Feedback(FeedbackModel model)
		{
			var text = new StringBuilder()
				.AppendFormat("Новая заявка ({0})", DateTime.UtcNow).AppendLine()
				.AppendLine("КОНТАКТЫ ФАБРИКИ")
				.AppendFormat("Название {0}", model.FactoryName).AppendLine()
				.AppendFormat("Телефон {0}", model.FactoryPhone).AppendLine()
				.AppendFormat("E-mail {0}", model.FactoryEmail).AppendLine()
				.AppendFormat("Контактное лицо {0}", model.FactoryContact).AppendLine()
				.AppendLine()
				.AppendLine("КОНТАКТЫ ОТПРАВИТЕЛЯ")
				.AppendFormat("Имя {0}", model.UserName).AppendLine()
				.AppendFormat("Телефон {0}", model.UserPhone).AppendLine()
				.AppendFormat("E-mail {0}", model.UserEmail).AppendLine()
				.AppendFormat("Удобное время для звонка {0}", model.UserCallTime).AppendLine()
				.AppendLine("Комментарий")
				.Append(model.Comment)
				.ToString();

			if(!ModelState.IsValid)
			{
				throw new InvalidLogicException("МЫ НЕ МОЖЕМ ОБРАБОТАТЬ ЭТУ ЗАЯВКУ"
				                                + Environment.NewLine + "УКАЖИТЕ КОНТАКТНОЕ ИМЯ И ТЕЛЕФОН"
				                                + Environment.NewLine + Environment.NewLine + text
												+ Environment.NewLine);
			}

			_sender.Send(new EmailMessage("Новая завяка на сайте avionrussia.com", text, EmailsHelper.DefaultFrom, EmailsHelper.FeedbackEmail)
			{
				IsBodyHtml = false
			});

			return Redirect("/index.html");
		}
	}
}