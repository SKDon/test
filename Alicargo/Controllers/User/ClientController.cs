using System.Web.Mvc;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Extensions;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;
using Resources;

namespace Alicargo.Controllers.User
{
	public partial class ClientController : Controller
	{
		private readonly IClientPresenter _clients;
		private readonly IClientFileRepository _files;
		private readonly IClientManager _manager;

		public ClientController(
			IClientManager manager,
			IClientFileRepository files,
			IClientPresenter clients)
		{
			_manager = manager;
			_files = files;
			_clients = clients;
		}

		#region List

		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ViewResult Index()
		{
			return View();
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Manager)]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, int skip)
		{
			var collection = _clients.GetList(take, skip);

			return Json(collection);
		}

		#endregion

		#region Create

		[HttpGet]
		[Access(RoleType.Admin, RoleType.Manager, RoleType.Client)]
		public virtual ViewResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Access(RoleType.Admin, RoleType.Manager, RoleType.Client)]
		public virtual ActionResult Create(ClientModel model, [Bind(Prefix = "Transit")] TransitEditModel transitModel)
		{
			if(!EmailsHelper.Validate(model.Emails))
			{
				ModelState.AddModelError("Emails", @"Emails format is invalid");
			}

			if(!ModelState.IsValid)
			{
				return View();
			}

			var authenticationModel = model.Authentication;
			long clientId = 0;
			var passwordDefined = !string.IsNullOrWhiteSpace(authenticationModel.NewPassword);
			if(passwordDefined)
			{
				try
				{
					clientId = _manager.Add(model, transitModel, authenticationModel);

					if(model.ContractFile != null)
					{
						MergeContract(model, clientId);
					}
				}
				catch(DublicateLoginException)
				{
					ModelState.AddModelError("Login", Validation.LoginExists);
				}
			}
			else
			{
				ModelState.AddModelError("NewPassword", Validation.EmplyPassword);
			}

			if(!ModelState.IsValid) return View();

			return RedirectToAction(MVC.Client.Edit(clientId));
		}

		#endregion

		[HttpGet]
		[Access(RoleType.Admin, RoleType.Manager, RoleType.Client)]
		public virtual FileResult Contract(long? id)
		{
			var data = _clients.GetCurrentClientData(id);

			var document = _files.GetClientContract(data.ClientId);

			return document.GetFileResult();
		}

		#region Edit

		[HttpGet]
		[Access(RoleType.Admin, RoleType.Manager, RoleType.Client)]
		public virtual ActionResult Edit(long? id)
		{
			var data = _clients.GetCurrentClientData(id);

			var contractFileName = _files.GetClientContractFileName(data.ClientId);

			var model = GetModel(data, contractFileName);

			ViewBag.ClientId = data.ClientId;

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Access(RoleType.Admin, RoleType.Manager, RoleType.Client)]
		public virtual ActionResult Edit(long? id, ClientModel model,
			[Bind(Prefix = "Transit")] TransitEditModel transitModel)
		{
			if(!EmailsHelper.Validate(model.Emails))
			{
				ModelState.AddModelError("Emails", @"Emails format is invalid");
			}

			if(!ModelState.IsValid)
			{
				return View();
			}

			var client = _clients.GetCurrentClientData(id);

			try
			{
				var authenticationModel = model.Authentication;

				_manager.Update(client.ClientId, model, transitModel, authenticationModel);

				MergeContract(model, client.ClientId);
			}
			catch(DublicateLoginException)
			{
				ModelState.AddModelError("Login", Validation.LoginExists);
			}

			if(!ModelState.IsValid) return View();

			return RedirectToAction(MVC.Client.Edit(client.ClientId));
		}

		private static ClientModel GetModel(ClientData client, string contractFileName)
		{
			return new ClientModel
			{
				BIC = client.BIC,
				Phone = client.Phone,
				Emails = EmailsHelper.JoinEmails(client.Emails),
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
				ContractFileName = contractFileName,
				Authentication = new AuthenticationModel(client.Login)
			};
		}

		#endregion

		private void MergeContract(ClientModel model, long clientId)
		{
			var oldFileName = _files.GetClientContractFileName(clientId);
			if(oldFileName != model.ContractFileName)
			{
				_files.SetClientContract(clientId, model.ContractFileName, model.ContractFile);
			}
		}
	}
}