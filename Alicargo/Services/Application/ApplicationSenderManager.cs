using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class ApplicationSenderManager : IApplicationSenderManager
	{
		private readonly IApplicationUpdateRepository _applicationUpdater;
		private readonly IApplicationRepository _applications;
		private readonly IStateConfig _stateConfig;
		private readonly ITransitRepository _transits;
		private readonly IUnitOfWork _unitOfWork;

		public ApplicationSenderManager(
			IApplicationRepository applications,
			IApplicationUpdateRepository applicationUpdater,
			IUnitOfWork unitOfWork,
			ITransitRepository transits,
			IStateConfig stateConfig)
		{
			_applications = applications;
			_applicationUpdater = applicationUpdater;
			_unitOfWork = unitOfWork;
			_transits = transits;
			_stateConfig = stateConfig;
		}

		public ApplicationSenderModel Get(long id)
		{
			var application = _applications.Get(id);

			var model = GetModel(application);

			return model;
		}

		public void Update(long id, ApplicationSenderModel model)
		{
			var applicationData = _applications.Get(id);

			Map(model, applicationData);

			_applicationUpdater.Update(applicationData, model.SwiftFile, model.InvoiceFile, null, null, null, model.PackingFile);

			_unitOfWork.SaveChanges();
		}

		public void Add(ApplicationSenderModel model, long clientId, long senderId)
		{
			var applicationData = new ApplicationData { ClientId = clientId };

			Map(model, applicationData);

			Add(applicationData, clientId, senderId, model.SwiftFile, model.InvoiceFile, model.PackingFile);
		}

		private void Add(ApplicationData applicationData, long clientId, long? senderId, byte[] swiftFile, byte[] invoiceFile,
						 byte[] packingFile)
		{
			var transitId = CopyTransitDataFromClient(clientId);

			applicationData.TransitId = transitId;
			applicationData.StateId = _stateConfig.DefaultStateId;
			applicationData.ClassId = null;
			applicationData.StateChangeTimestamp = DateTimeOffset.UtcNow;
			applicationData.CreationTimestamp = DateTimeOffset.UtcNow;
			applicationData.SenderId = senderId;

			_applicationUpdater.Add(applicationData, swiftFile, invoiceFile, null, null, null, packingFile);
			
			_unitOfWork.SaveChanges();
		}

		private long CopyTransitDataFromClient(long clientId)
		{
			var transitData = _transits.GetByClient(clientId);
			var tid = _transits.Add(transitData);
			_unitOfWork.SaveChanges();
			var transitId = tid();
			return transitId;
		}

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
			to.CountryId = from.CountryId;
		}

		private static ApplicationSenderModel GetModel(ApplicationData application)
		{
			return new ApplicationSenderModel
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
				WithdrawCost = application.WithdrawCost,
				CountryId = application.CountryId
			};
		}
	}
}