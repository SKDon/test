using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers.Extensions;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using System.Net;
using Alicargo.ViewModels.Application;

namespace Alicargo.Controllers
{
	public partial class ApplicationController : Controller
	{
		private readonly IApplicationManager _applicationManager;
		private readonly IApplicationPresenter _applicationPresenter;
		private readonly IClientPresenter _clientPresenter;
		private readonly IApplicationRepository _applicationRepository;
		private readonly IUserRepository _users;

		public ApplicationController(
			IApplicationManager applicationManager,
			IApplicationPresenter applicationPresenter,
			IClientPresenter clientPresenter,
			IApplicationRepository applicationRepository, IUserRepository users)
		{
			_applicationManager = applicationManager;
			_applicationPresenter = applicationPresenter;
			_clientPresenter = clientPresenter;
			_applicationRepository = applicationRepository;
			_users = users;
		}

		#region Details

		[ChildActionOnly]
		public virtual PartialViewResult Details(long id)
		{
			var application = _applicationPresenter.GetDetails(id);

			return PartialView(application);
		}

		public virtual FileResult InvoiceFile(long id)
		{
			var file = _applicationRepository.GetInvoiceFile(id);

			return file.FileData.GetFileResult(file.FileName);
		}

		public virtual FileResult DeliveryBillFile(long id)
		{
			var file = _applicationRepository.GetDeliveryBillFile(id);

			return file.FileData.GetFileResult(file.FileName);
		}

		public virtual FileResult CPFile(long id)
		{
			var file = _applicationRepository.GetCPFile(id);

			return file.FileData.GetFileResult(file.FileName);
		}

		public virtual FileResult SwiftFile(long id)
		{
			var file = _applicationRepository.GetSwiftFile(id);

			return file.FileData.GetFileResult(file.FileName);
		}

		public virtual FileResult Torg12File(long id)
		{
			var file = _applicationRepository.GetTorg12File(id);

			return file.FileData.GetFileResult(file.FileName);
		}

		public virtual FileResult PackingFile(long id)
		{
			var file = _applicationRepository.GetPackingFile(id);

			return file.FileData.GetFileResult(file.FileName);
		}

		#endregion

		private void BindBag(long? clientId, long? applicationId)
		{
			var client = _clientPresenter.GetCurrentClientData(clientId);

			ViewBag.ClientNic = client.Nic;

			ViewBag.ClientId = client.Id;

			ViewBag.ApplicationId = applicationId;

			ViewBag.Countries = _applicationPresenter.GetLocalizedCountries();

			ViewBag.Senders = _users.GetByRole(RoleType.Sender).OrderBy(x => x.Name).ToDictionary(x => x.EntityId, x => x.Name);
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual HttpStatusCodeResult Delete(long id)
		{
			_applicationManager.Delete(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		#region Edit

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Edit(long id)
		{
			var application = _applicationManager.Get(id);

			var clientId = _applicationRepository.GetClientId(id);

			BindBag(clientId, id);

			return View(application);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Access(RoleType.Admin)]
		public virtual ActionResult Edit(long id, ApplicationAdminModel model, CarrierSelectModel carrierModel,
			[Bind(Prefix = "Transit")] TransitEditModel transitModel)
		{
			if (!ModelState.IsValid)
			{
				var clientId = _applicationRepository.GetClientId(id);
				BindBag(clientId, id);
				return View(model);
			}

			_applicationManager.Update(id, model, carrierModel, transitModel);

			return RedirectToAction(MVC.ApplicationList.Index());
		}

		#endregion

		#region Create

		[Access(RoleType.Admin, RoleType.Client)]
		public virtual ViewResult Create(long? clientId)
		{
			BindBag(clientId, null);

			return View(new ApplicationAdminModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Access(RoleType.Admin, RoleType.Client)]
		public virtual ActionResult Create(long? clientId, ApplicationAdminModel model, CarrierSelectModel carrierModel,
			[Bind(Prefix = "Transit")] TransitEditModel transitModel)
		{
			var client = _clientPresenter.GetCurrentClientData(clientId);

			if (!ModelState.IsValid)
			{
				BindBag(client.Id, null);

				return View(model);
			}

			try
			{
				_applicationManager.Add(model, carrierModel, transitModel, client.Id);
			}
			catch (DublicateException ex)
			{
				ModelState.AddModelError("DublicateException", ex.ToString());

				return View(model);
			}

			return RedirectToAction(MVC.ApplicationList.Index());
		}

		#endregion
	}
}