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
	public partial class CarrierController : Controller
	{
		private readonly ICityRepository _cities;
		private readonly ICarrierRepository _carriers;
		private readonly IIdentityService _identity;
		private readonly IUserRepository _users;

		public CarrierController(ICityRepository cities, ICarrierRepository carriers, IIdentityService identity, IUserRepository users)
		{
			_cities = cities;
			_carriers = carriers;
			_identity = identity;
			_users = users;
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
		public virtual ActionResult Create(CarrierModel model)
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
				var id = _carriers.Add(model.Name, model.Email, model.Phone, model.Contact,
					model.Authentication.Login, model.Authentication.NewPassword, _identity.Language);

				_carriers.SetCities(id, model.Cities);

				return RedirectToAction(MVC.Carrier.Edit(id));
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

			var data = _carriers.Get(id);

			var model = new CarrierModel
			{
				Authentication = new AuthenticationModel(data.Login),
				Email = data.Email,
				Cities = _carriers.GetCities(id),
				Name = data.Name,
				Contact = data.Contact,
				Phone = data.Phone
			};

			return View(model);
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual ActionResult Edit(long id, CarrierModel model)
		{
			if(!ModelState.IsValid)
			{
				BindBag();

				return View();
			}

			try
			{
				_carriers.Update(id, model.Name, model.Email, model.Phone, model.Contact, model.Authentication.Login);

				_carriers.SetCities(id, model.Cities);

				if(!string.IsNullOrWhiteSpace(model.Authentication.NewPassword))
				{
					var data = _carriers.Get(id);

					_users.SetPassword(data.UserId, model.Authentication.NewPassword);
				}

				return RedirectToAction(MVC.Carrier.Edit(id));
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
