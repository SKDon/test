using System;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Contracts;
using Alicargo.Core.Enums;
using Alicargo.Core.Repositories;
using Alicargo.Helpers;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Controllers
{
	[Access(RoleType.Sender)]
	public partial class SenderController : Controller
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IClientRepository _clientRepository;
		private readonly ITransitRepository _transitRepository;
		private readonly IStateConfig _stateConfig;
		private readonly IUnitOfWork _unitOfWork;

		public SenderController(
			IApplicationRepository applicationRepository, 
			IClientRepository clientRepository,
			ITransitRepository transitRepository,
			IStateConfig stateConfig,
			IUnitOfWork unitOfWork)
		{
			_applicationRepository = applicationRepository;
			_clientRepository = clientRepository;
			_transitRepository = transitRepository;
			_stateConfig = stateConfig;
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public virtual ViewResult ApplicationCreate(long id)
		{
			var clientData = _clientRepository.Get(id).First();

			ViewBag.Nic = clientData.Nic;

			return View(new ApplicationSenderEdit { CurrencyId = (int)CurrencyType.Euro });
		}

		[HttpGet]
		public virtual ViewResult ApplicationEdit(long id)
		{
			var application = _applicationRepository.Get(id);
			var clientData = _clientRepository.Get(application.ClientId).First();

			ViewBag.Nic = clientData.Nic;
			ViewBag.ApplicationNumber = ApplicationModel.GetDisplayNumber(application.Id, application.Count);

			return View(new ApplicationSenderEdit
			{
				Count = application.Count,
				PackingFile = null,
				PackingFileName = application.PackingFileName,
				FactoryName = application.FactoryName,
				Gross = application.Gross,
				Invoice = application.Invoice,
				InvoiceFile = null,
				InvoiceFileName = application.InvoiceFileName,
				MarkName = application.MarkName,
				SwiftFile = null,
				SwiftFileName = application.SwiftFileName,
				Value = application.Value,
				CurrencyId = application.CurrencyId,
				Volume = application.Volume
			});
		}

		[HttpPost]
		public virtual ActionResult ApplicationEdit(long id, ApplicationSenderEdit model)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.ApplicationNumber = ApplicationModel.GetDisplayNumber(id, model.Count);
				return View(model);
			}

			var applicationData = _applicationRepository.Get(id);

			Set(model, applicationData);

			_applicationRepository.Update(applicationData, model.SwiftFile, model.InvoiceFile, null, null, null, model.PackingFile);
			_unitOfWork.SaveChanges();

			return RedirectToAction(MVC.Application.Index());
		}

		[HttpPost]
		public virtual ActionResult ApplicationCreate(long id, ApplicationSenderEdit model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var applicationData = new ApplicationData { ClientId = id };
			Set(model, applicationData);
			var clientData = _clientRepository.Get(id).First();
			var transitData = _transitRepository.Get(clientData.TransitId).First();

			using (var ts = _unitOfWork.GetTransactionScope())
			{
				var tid = _transitRepository.Add(transitData);
				_unitOfWork.SaveChanges();
				applicationData.TransitId = tid();
				applicationData.TermsOfDelivery = "";
				applicationData.FactoryPhone = "";
				applicationData.FactoryEmail = "";
				applicationData.StateId = _stateConfig.DefaultStateId;
				applicationData.StateChangeTimestamp = DateTimeOffset.UtcNow;
				applicationData.CreationTimestamp = DateTimeOffset.UtcNow;

				_applicationRepository.Add(applicationData, model.SwiftFile, model.InvoiceFile, null, null, null, model.PackingFile);
				_unitOfWork.SaveChanges();
				
				ts.Complete();
			}

			return RedirectToAction(MVC.Application.Index());
		}

		private static void Set(ApplicationSenderEdit model, ApplicationData applicationData)
		{
			applicationData.Count = model.Count;
			applicationData.PackingFileName = model.PackingFileName;
			applicationData.FactoryName = model.FactoryName;
			applicationData.Gross = model.Gross;
			applicationData.Invoice = model.Invoice;
			applicationData.InvoiceFileName = model.InvoiceFileName;
			applicationData.MarkName = model.MarkName;
			applicationData.SwiftFileName = model.SwiftFileName;
			applicationData.Value = model.Value;
			applicationData.CurrencyId = model.CurrencyId;
			applicationData.Volume = model.Volume;
		}
	}
}
