using System;
using System.Web.Mvc;
using Alicargo.Core.Enums;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Models;
using Alicargo.Helpers;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Resources;

namespace Alicargo.Controllers
{
	// todo: create a model binder for the ClientModel
	public partial class ClientController : Controller
	{
		private readonly IClientService _clientService;

		public ClientController(IClientService clientService)
		{
			_clientService = clientService;
		}

		#region List

		[Access(RoleType.Admin, RoleType.Forwarder, RoleType.Sender)]
		public virtual ViewResult Index()
		{
			return View();
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Forwarder, RoleType.Sender)]
		public virtual JsonResult List(int take, int skip, int page, int pageSize)
		{
			var collection = _clientService.GetList(take, skip);

			return Json(collection);
		}

		#endregion

		#region Create

		[HttpGet]
		[Access(RoleType.Admin, RoleType.Client)]
		public virtual ViewResult Create()
		{
			return View();
		}

		[HttpPost]
		//[ValidateAntiForgeryToken]
		[Access(RoleType.Admin, RoleType.Client)]
		public virtual ActionResult Create(Client model, CarrierSelectModel carrierSelectModel)
		{
			if (string.IsNullOrWhiteSpace(model.AuthenticationModel.NewPassword))
				ModelState.AddModelError("NewPassword", Validation.EmplyPassword);

			if (!ModelState.IsValid) return View();

			try
			{
				_clientService.Add(model, carrierSelectModel);
			}
			catch (DublicateException ex)
			{
				// todo: refactor
				if (ex.ToString().Contains("IX_User_Login"))
				{
					ModelState.AddModelError("AuthenticationModel.Login", Validation.LoginExists);
				}
				else
				{
					ModelState.AddModelError("", ex.ToString());
				}

				return View(model);
			}

			return RedirectToAction(MVC.Client.Index());
		}

		#endregion

		#region Edit

		[HttpGet]
		[Access(RoleType.Admin, RoleType.Client)]
		public virtual ActionResult Edit(long? id)
		{
			var client = _clientService.GetClient(id);

			return View(client);
		}

		// todo: test
		[HttpPost]
		//[ValidateAntiForgeryToken]
		[Access(RoleType.Admin, RoleType.Client)]
		public virtual ActionResult Edit(Client model, CarrierSelectModel carrierSelectModel)
		{
			if (!ModelState.IsValid) return View(model);

			_clientService.Update(model, carrierSelectModel);

			return RedirectToAction(MVC.Client.Edit(model.Id));
		}

		#endregion
	}
}