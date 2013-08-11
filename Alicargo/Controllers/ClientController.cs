using System.Web.Mvc;
using Alicargo.Core.Enums;
using Alicargo.Core.Exceptions;
using Alicargo.Helpers;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Resources;

namespace Alicargo.Controllers
{
	// todo: refactor contracts
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
		[ValidateAntiForgeryToken]
		[Access(RoleType.Admin, RoleType.Client)]
		public virtual ActionResult Create(ClientModel model, TransitEditModel transitModel, CarrierSelectModel carrierModel, AuthenticationModel authenticationModel)
		{
			if (string.IsNullOrWhiteSpace(authenticationModel.NewPassword))
				ModelState.AddModelError("NewPassword", Validation.EmplyPassword);

			if (!ModelState.IsValid) return View();

			try
			{
				_clientService.Add(model, carrierModel, transitModel, authenticationModel);
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
			var data = _clientService.GetClientData(id);

			var model = ClientModel.GetModel(data);

			return View(model);
		}

		// todo: test
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Access(RoleType.Admin, RoleType.Client)]
		public virtual ActionResult Edit(long? id, ClientModel model, TransitEditModel transitModel, CarrierSelectModel carrierModel, AuthenticationModel authenticationModel)
		{
			var data = _clientService.GetClientData(id);

			if (!ModelState.IsValid) return View(model);

			_clientService.Update(data.Id, model, carrierModel, transitModel, authenticationModel);

			return RedirectToAction(MVC.Client.Index());
		}

		#endregion
	}
}