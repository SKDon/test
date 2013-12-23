using System;
using System.Web.Caching;
using System.Web.Mvc;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Services.Abstract;
using Resources;

namespace Alicargo.Controllers.User
{
	public partial class RestorePasswordController : Controller
	{
		private readonly IMailSender _sender;
		private readonly IUserRepository _users;

		public RestorePasswordController(IUserRepository users, IMailSender sender)
		{
			_users = users;
			_sender = sender;
		}

		[HttpGet]
		public virtual ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public virtual ActionResult Index(string email)
		{
			var id = _users.GetUserIdByEmail(email.Trim());

			if (!id.HasValue)
			{
				ModelState.AddModelError("email", Validation.UserNotFound);
				return View();
			}

			var key = Guid.NewGuid().ToString();
			SaveKey(id.Value, key);

			var url = Url.Action(MVC.RestorePassword.NewPassword(id.Value, key));
			_sender.Send(new EmailMessage(Pages.RestorePassword, string.Format(Pages.RestorePasswordText, url),
				EmailsHelper.DefaultFrom, email.Trim())
			{
				CopyTo = null,
				Files = null,
				IsBodyHtml = true
			});

			return RedirectToAction(MVC.RestorePassword.Finish());
		}

		[HttpGet]
		public virtual ActionResult NewPassword(long id, string key)
		{
			var savedKey = GetSavedKey(id);

			if (savedKey != key)
			{
				return RedirectToAction(MVC.RestorePassword.UnknownKey());
			}

			return View();
		}

		[HttpPost]
		public virtual ActionResult NewPassword(long id, string key, string password)
		{
			var savedKey = GetSavedKey(id);

			if (savedKey != key)
			{
				return RedirectToAction(MVC.RestorePassword.UnknownKey());
			}

			HttpContext.Cache.Remove(GetCacheKey(id));

			_users.SetPassword(id, password);

			return RedirectToAction(MVC.RestorePassword.Success());
		}

		[HttpGet]
		public virtual ViewResult Finish()
		{
			return View();
		}

		[HttpGet]
		public virtual ViewResult UnknownKey()
		{
			return View();
		}

		[HttpGet]
		public virtual ViewResult Success()
		{
			return View();
		}

		private void SaveKey(long id, string key)
		{
			HttpContext.Cache.Add(GetCacheKey(id), key, null, DateTime.Now.AddHours(1),
				Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
		}

		private static string GetCacheKey(long id)
		{
			return "restore-password-key-" + id;
		}

		private string GetSavedKey(long id)
		{
			var key = GetCacheKey(id);

			return (string)HttpContext.Cache[key];
		}
	}
}