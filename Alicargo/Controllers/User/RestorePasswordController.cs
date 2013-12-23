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

			var url = Url.Action(MVC.RestorePassword.NewPassword(id.Value, key), "http");
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
			var savedKey = GetSavedKey(GetCacheKey(id));

			if (savedKey != key)
			{
				return RedirectToAction(MVC.RestorePassword.UnknownKey());
			}

			return View();
		}

		[HttpPost]
		public virtual ActionResult NewPassword(long id, string key, string password)
		{
			var cacheKey = GetCacheKey(id);
			var savedKey = GetSavedKey(cacheKey);

			if (savedKey != key)
			{
				return RedirectToAction(MVC.RestorePassword.UnknownKey());
			}

			RemoveKey(cacheKey);

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

		private void RemoveKey(string cacheKey)
		{
			HttpContext.Cache.Remove(cacheKey);
		}

		private void SaveKey(long id, string key)
		{
			var cacheKey = GetCacheKey(id);

			RemoveKey(cacheKey);

			HttpContext.Cache.Add(cacheKey, key, null, DateTime.Now.AddHours(1),
				Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
		}

		private string GetSavedKey(string cacheKey)
		{
			return (string)HttpContext.Cache[cacheKey];
		}

		private static string GetCacheKey(long id)
		{
			return "restore-password-key-" + id;
		}
	}
}