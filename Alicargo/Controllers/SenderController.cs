using System;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Controllers
{
	[Access(RoleType.Sender)]
	public partial class SenderController : Controller
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IApplicationUpdateRepository _applicationUpdater;
		private readonly IClientRepository _clientRepository;
		private readonly ITransitRepository _transitRepository;
		private readonly IStateConfig _stateConfig;
		private readonly IUnitOfWork _unitOfWork;		

		public SenderController(
			IApplicationRepository applicationRepository,
			IClientRepository clientRepository,
			ITransitRepository transitRepository,
			IStateConfig stateConfig,
			IUnitOfWork unitOfWork,
			IApplicationUpdateRepository applicationUpdater)
		{
			_applicationRepository = applicationRepository;
			_clientRepository = clientRepository;
			_transitRepository = transitRepository;
			_stateConfig = stateConfig;
			_unitOfWork = unitOfWork;
			_applicationUpdater = applicationUpdater;
		}

		[HttpGet]
		public virtual ViewResult ApplicationCreate(long id)
		{
			var clientData = _clientRepository.Get(id).First();

			ViewBag.Nic = clientData.Nic;

			return View(new ApplicationSenderModel());
		}

		[HttpGet]
		public virtual ViewResult ApplicationEdit(long id)
		{
			var application = _applicationRepository.Get(id);
			var clientData = _clientRepository.Get(application.ClientId).First();

			ViewBag.Nic = clientData.Nic;
			ViewBag.ApplicationNumber = ApplicationModelHelper.GetDisplayNumber(application.Id, application.Count);

			return View(new ApplicationSenderModel
			{
				Count = application.Count,
				PackingFile = null,
				PackingFileName = application.PackingFileName,
				FactoryName = application.FactoryName,
				Weigth = application.Weigth,
				Invoice = application.Invoice,
				InvoiceFile = null,
				InvoiceFileName = application.InvoiceFileName,
				MarkName = application.MarkName,
				SwiftFile = null,
				SwiftFileName = application.SwiftFileName,
				Currency = new CurrencyModel
				{
					Value = application.Value,
					CurrencyId = application.CurrencyId
				},
				Volume = application.Volume,
				FactureCost = application.FactureCost,
				ScotchCost = application.ScotchCost,
				WithdrawCost = application.WithdrawCost
			});
		}

		[HttpPost]
		public virtual ActionResult ApplicationEdit(long id, ApplicationSenderModel model)
		{
			if (!ModelState.IsValid)
			{
				var clientId = _applicationRepository.GetClientId(id);
				var clientData = _clientRepository.Get(clientId).First();
				ViewBag.Nic = clientData.Nic;
				ViewBag.ApplicationNumber = ApplicationModelHelper.GetDisplayNumber(id, model.Count);

				return View(model);
			}

			var applicationData = _applicationRepository.Get(id);

			Map(model, applicationData);

			_applicationUpdater.Update(applicationData, model.SwiftFile, model.InvoiceFile, null, null, null, model.PackingFile);
			_unitOfWork.SaveChanges();

			return RedirectToAction(MVC.ApplicationList.Index());
		}

		[HttpPost]
		public virtual ActionResult ApplicationCreate(long id, ApplicationSenderModel model)
		{
			var clientData = _clientRepository.Get(id).First();

			if (!ModelState.IsValid)
			{
				ViewBag.Nic = clientData.Nic;

				return View(model);
			}

			var applicationData = new ApplicationData { ClientId = id };

			Map(model, applicationData);

			var transitData = _transitRepository.Get(clientData.TransitId).First();

			using (var ts = _unitOfWork.StartTransaction())
			{
				var tid = _transitRepository.Add(transitData);
				_unitOfWork.SaveChanges();

				applicationData.TransitId = tid();
				applicationData.StateId = _stateConfig.DefaultStateId;
				applicationData.StateChangeTimestamp = DateTimeOffset.UtcNow;
				applicationData.CreationTimestamp = DateTimeOffset.UtcNow;

				_applicationUpdater.Add(applicationData, model.SwiftFile, model.InvoiceFile, null, null, null, model.PackingFile);
				_unitOfWork.SaveChanges();

				ts.Complete();
			}

			return RedirectToAction(MVC.ApplicationList.Index());
		}

		//[ChildActionOnly]
		//public SelectModel<long> Select(long? applicationId)
		//{
		//	var model = new SelectModel<long>
		//	{
		//		List = _users.GetByRole(RoleType.Sender).OrderBy(x => x.Name).ToDictionary(x => x.Id, x => x.Name)
		//	};

		//	if (applicationId.HasValue)
		//	{
		//		var application = _applicationRepository.Get(applicationId.Value);
		//		if (application.SenderId.HasValue)
		//		{
		//			model.Selected = application.SenderId.Value;
		//		}
		//	}

		//	return model;
		//}

		private static void Map(ApplicationSenderModel @from, ApplicationData to)
		{
			to.Count = @from.Count;
			to.PackingFileName = @from.PackingFileName;
			to.FactoryName = @from.FactoryName;
			to.Weigth = @from.Weigth;
			to.Invoice = @from.Invoice;
			to.InvoiceFileName = @from.InvoiceFileName;
			to.MarkName = @from.MarkName;
			to.SwiftFileName = @from.SwiftFileName;
			to.Value = @from.Currency.Value;
			to.CurrencyId = @from.Currency.CurrencyId;
			to.Volume = @from.Volume;
			to.FactureCost = @from.FactureCost;
			to.WithdrawCost = from.WithdrawCost;
			to.ScotchCost = from.ScotchCost;
		}
	}
}
