using System;
using System.Web.Mvc;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers.Extensions;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Resources;

namespace Alicargo.Controllers
{
	// todo: 2. bb tests
	public partial class ClientController : Controller
	{
		private readonly IClientManager _clientManager;
		private readonly IClientPresenter _clientPresenter;
		private readonly IClientFileRepository _files;

		public ClientController(
			IClientManager clientManager,
			IClientFileRepository files,
			IClientPresenter clientPresenter)
		{
			_clientManager = clientManager;
			_files = files;
			_clientPresenter = clientPresenter;
		}

		#region List

		[Access(RoleType.Admin, RoleType.Forwarder, RoleType.Sender)]
		public virtual ViewResult Index()
		{
			return View();
		}

		[HttpPost, Access(RoleType.Admin, RoleType.Forwarder, RoleType.Sender),
		 OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
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
		public virtual ActionResult Create(ClientModel model,
			[Bind(Prefix = "Transit")] TransitEditModel transitModel,
			CarrierSelectModel carrierModel,
			[Bind(Prefix = "Authentication")] AuthenticationModel authenticationModel)
		{
			long clientId = 0;
			var passwordDefined = !string.IsNullOrWhiteSpace(authenticationModel.NewPassword);
			if (passwordDefined)
			{
				try
				{
					clientId = _clientManager.Add(model, carrierModel, transitModel, authenticationModel);

					if (model.ContractFile != null)
					{
						MergeContract(model, clientId);
					}
				}
				catch (DublicateLoginException)
				{
					ModelState.AddModelError("Login", Validation.LoginExists);
				}
			}
			else
			{
				ModelState.AddModelError("NewPassword", Validation.EmplyPassword);
			}

			if (!ModelState.IsValid) return View();

			return RedirectToAction(MVC.Client.Edit(clientId));
		}

		#endregion

		private void MergeContract(ClientModel model, long clientId)
		{
			var oldFileName = _files.GetClientContractFileName(clientId);
			if (oldFileName != model.ContractFileName)
			{
				_files.SetClientContract(clientId, model.ContractFileName, model.ContractFile);
			}
		}

		[HttpGet, Access(RoleType.Admin, RoleType.Client)]
		public virtual FileResult Contract(long? id)
		{
			var data = _clientPresenter.GetCurrentClientData(id);

			var document = _files.GetClientContract(data.Id);

			return document.GetFileResult();
		}

		#region Edit

		[HttpGet, Access(RoleType.Admin, RoleType.Client)]
		public virtual ActionResult Edit(long? id)
		{
			var data = _clientPresenter.GetCurrentClientData(id);

			var contractFileName = _files.GetClientContractFileName(data.Id);

			var model = GetModel(data, contractFileName);

			ViewBag.ClientId = data.Id;

			return View(model);
		}

		private static ClientModel GetModel(ClientData client, string contractFileName)
		{
			return new ClientModel
			{
				BIC = client.BIC,
				Phone = client.Phone,
				Email = client.Email,
				LegalEntity = client.LegalEntity,
				Bank = client.Bank,
				Contacts = client.Contacts,
				INN = client.INN,
				KPP = client.KPP,
				KS = client.KS,
				LegalAddress = client.LegalAddress,
				MailingAddress = client.MailingAddress,
				Nic = client.Nic,
				OGRN = client.OGRN,
				RS = client.RS,
				ContractFile = null,
				ContractFileName = contractFileName
			};
		}

		[ValidateAntiForgeryToken, HttpPost, Access(RoleType.Admin, RoleType.Client)]
		public virtual ActionResult Edit(long? id, ClientModel model,
			[Bind(Prefix = "Transit")] TransitEditModel transitModel,
			CarrierSelectModel carrierModel,
			[Bind(Prefix = "Authentication")] AuthenticationModel authenticationModel)
		{
			var client = _clientPresenter.GetCurrentClientData(id);

			try
			{
				_clientManager.Update(client.Id, model, carrierModel, transitModel, authenticationModel);

				MergeContract(model, client.Id);
			}
			catch (DublicateLoginException)
			{
				ModelState.AddModelError("Login", Validation.LoginExists);
			}

			if (!ModelState.IsValid) return View();

			return RedirectToAction(MVC.Client.Edit(client.Id));
		}

		#endregion
	}
}