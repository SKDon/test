using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.MvcHelpers;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Antlr.Runtime.Misc;
using Resources;

namespace Alicargo.Controllers
{
    // todo: 2. bb tests
	public partial class ClientController : Controller
	{
		private readonly IClientManager _clientManager;
		private readonly IClientPresenter _clientPresenter;

		public ClientController(IClientManager clientManager, IClientPresenter clientPresenter)
		{
			_clientManager = clientManager;
			_clientPresenter = clientPresenter;
		}

		#region List

		[Access(RoleType.Admin, RoleType.Forwarder, RoleType.Sender)]
		public virtual ViewResult Index()
		{
			return View();
		}

		[HttpPost, Access(RoleType.Admin, RoleType.Forwarder, RoleType.Sender)]
		public virtual JsonResult List(int take, int skip, int page, int pageSize)
		{
			var collection = _clientPresenter.GetList(take, skip);

			return Json(collection);
		}

		#endregion

		#region Create

		[HttpGet, Access(RoleType.Admin, RoleType.Client)]
		public virtual ViewResult Create()
		{
			return View();
		}

		[ValidateAntiForgeryToken, HttpPost, Access(RoleType.Admin, RoleType.Client)]
		public virtual ActionResult Create(ClientModel model, [Bind(Prefix = "Transit")] TransitEditModel transitModel,
										   CarrierSelectModel carrierModel, [Bind(Prefix = "Authentication")] AuthenticationModel authenticationModel)
		{
			if (string.IsNullOrWhiteSpace(authenticationModel.NewPassword))
				ModelState.AddModelError("NewPassword", Validation.EmplyPassword);

			if (!ModelState.IsValid) return View();

			long id = 0;
			if (!HandleDublicateLogin(() => id = _clientManager.Add(model, carrierModel, transitModel, authenticationModel)))
			{
				return View(model);
			}

			return RedirectToAction(MVC.Client.Edit(id));
		}

		#endregion

		private bool HandleDublicateLogin(Action action)
		{
			try
			{
				action();
			}
			catch (DublicateException ex)
			{
				if (ex.ToString().Contains("IX_User_Login"))
				{
					ModelState.AddModelError("Login", Validation.LoginExists);
				}
				else
				{
					throw;
				}

				return false;
			}

			return true;
		}

		#region Edit

		[HttpGet, Access(RoleType.Admin, RoleType.Client)]
		public virtual ActionResult Edit(long? id)
		{
			var data = _clientPresenter.GetCurrentClientData(id);

			var model = ClientModel.GetModel(data);

			ViewBag.ClientId = data.Id;

			return View(model);
		}

		[ValidateAntiForgeryToken, HttpPost, Access(RoleType.Admin, RoleType.Client)]
		public virtual ActionResult Edit(long? id, ClientModel model, [Bind(Prefix = "Transit")] TransitEditModel transitModel,
										 CarrierSelectModel carrierModel, [Bind(Prefix = "Authentication")] AuthenticationModel authenticationModel)
		{
			var data = _clientPresenter.GetCurrentClientData(id);

			if (!ModelState.IsValid) return View(model);

			if (!HandleDublicateLogin(() => _clientManager.Update(data.Id, model, carrierModel, transitModel, authenticationModel)))
			{
				return View(model);
			}

			return RedirectToAction(MVC.Client.Edit(data.Id));
		}

		#endregion
	}
}