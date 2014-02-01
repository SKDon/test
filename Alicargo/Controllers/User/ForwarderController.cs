using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;
using Resources;

namespace Alicargo.Controllers.User
{
	public partial class ForwarderController : Controller
	{
		private readonly ICityRepository _cities;
		private readonly IForwarderRepository _forwarders;
		private readonly IIdentityService _identity;
		private readonly IUserRepository _users;

		public ForwarderController(
			ICityRepository cities,
			IUserRepository users,
			IForwarderRepository forwarders,
			IIdentityService identity)
		{
			_cities = cities;
			_users = users;
			_forwarders = forwarders;
			_identity = identity;
		}

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Create()
		{
			BindBag();

			return View();
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual ActionResult Create(ForwarderModel model)
		{
			if(string.IsNullOrWhiteSpace(model.Authentication.NewPassword))
				ModelState.AddModelError("NewPassword", Validation.EmplyPassword);

			if(!ModelState.IsValid)
			{
				BindBag();

				return View();
			}

			try
			{
				var id = _forwarders.Add(model.Name, model.Authentication.Login, model.Authentication.NewPassword, model.Email,
					_identity.Language);

				_forwarders.SetCities(id, model.Cities);

				return RedirectToAction(MVC.Forwarder.Edit(id));
			}
			catch(DublicateLoginException)
			{
				ModelState.AddModelError("Authentication.Login", Validation.LoginExists);

				BindBag();

				return View();
			}
		}

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Edit(long id)
		{
			BindBag();

			var data = _forwarders.Get(id);

			var model = new ForwarderModel
			{
				Authentication = new AuthenticationModel(data.Login),
				Email = data.Email,
				Cities = _forwarders.GetCities(id),
				Name = data.Name
			};

			return View(model);
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual ActionResult Edit(long id, ForwarderModel model)
		{
			if(!ModelState.IsValid)
			{
				BindBag();

				return View();
			}

			try
			{
				_forwarders.Update(id, model.Name, model.Authentication.Login, model.Email);

				_forwarders.SetCities(id, model.Cities);

				if(!string.IsNullOrWhiteSpace(model.Authentication.NewPassword))
				{
					var data = _forwarders.Get(id);

					_users.SetPassword(data.UserId, model.Authentication.NewPassword);
				}

				return RedirectToAction(MVC.Forwarder.Edit(id));
			}
			catch(DublicateLoginException)
			{
				ModelState.AddModelError("Authentication.Login", Validation.LoginExists);

				BindBag();

				return View();
			}
		}

		private void BindBag()
		{
			ViewBag.Cities = _cities.All(_identity.Language);
		}
	}
}