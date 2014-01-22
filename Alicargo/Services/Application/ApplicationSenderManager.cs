using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.Utilities;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class ApplicationSenderManager : IApplicationSenderManager
	{
		private readonly IApplicationUpdateRepository _applicationUpdater;
		private readonly IApplicationRepository _applications;
		private readonly ISenderRepository _senders;
		private readonly IStateConfig _stateConfig;
		private readonly ITransitRepository _transits;
		private readonly IUnitOfWork _unitOfWork;

		public ApplicationSenderManager(
			IApplicationRepository applications,
			ISenderRepository senders,
			IApplicationUpdateRepository applicationUpdater,
			IUnitOfWork unitOfWork,
			ITransitRepository transits,
			IStateConfig stateConfig)
		{
			_applications = applications;
			_senders = senders;
			_applicationUpdater = applicationUpdater;
			_unitOfWork = unitOfWork;
			_transits = transits;
			_stateConfig = stateConfig;
		}

		public ApplicationSenderModel Get(long id)
		{
			// todo: 2. check permissions to the application for a sender

			var application = _applications.Get(id);

			var model = GetModel(application);

			return model;
		}

		public void Update(long id, ApplicationSenderModel model)
		{
			// todo: 2. check permissions to the application for a sender

			var applicationData = _applications.Get(id);

			Map(model, applicationData);

			_applicationUpdater.Update(applicationData);

			_unitOfWork.SaveChanges();
		}

		public void Add(ApplicationSenderModel model, long clientId, long creatorSenderId)
		{
			var applicationData = new ApplicationData();

			Map(model, applicationData);

			Add(applicationData, clientId, creatorSenderId);
		}

		private void Add(ApplicationData applicationData, long clientId, long senderId)
		{
			var transitId = CopyTransitDataFromClient(clientId);
			var sender = _senders.Get(senderId);

			applicationData.TransitId = transitId;
			applicationData.StateId = _stateConfig.DefaultStateId;
			applicationData.ClassId = null;
			applicationData.StateChangeTimestamp = DateTimeProvider.Now;
			applicationData.CreationTimestamp = DateTimeProvider.Now;
			applicationData.SenderId = senderId;
			applicationData.ClientId = clientId;
			applicationData.CountryId = sender.CountryId;

			_applicationUpdater.Add(applicationData);

			_unitOfWork.SaveChanges();
		}

		private long CopyTransitDataFromClient(long clientId)
		{
			var transitData = _transits.GetByClient(clientId);

			transitData.Id = 0;

			return _transits.Add(transitData);
		}

		private static void Map(ApplicationSenderModel @from, ApplicationData to)
		{
			to.Count = @from.Count;
			to.FactoryName = @from.FactoryName;
			to.Weight = @from.Weight;
			to.Invoice = @from.Invoice;
			to.MarkName = @from.MarkName;
			to.Value = @from.Currency.Value;
			to.CurrencyId = @from.Currency.CurrencyId;
			to.Volume = @from.Volume;
			to.FactureCost = @from.FactureCost;
			to.PickupCost = from.PickupCost;
		}

		private static ApplicationSenderModel GetModel(ApplicationData application)
		{
			return new ApplicationSenderModel
			{
				Count = application.Count,
				FactoryName = application.FactoryName,
				Weight = application.Weight,
				Invoice = application.Invoice,
				MarkName = application.MarkName,
				Currency = new CurrencyModel
				{
					Value = application.Value,
					CurrencyId = application.CurrencyId
				},
				Volume = application.Volume,
				FactureCost = application.FactureCost,
				PickupCost = application.PickupCost,
			};
		}
	}
}