using Alicargo.Core.Contracts.State;
using Alicargo.Core.Contracts.Users;
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
		private readonly IApplicationRepository _applications;
		private readonly IForwarderService _forwarders;
		private readonly ISenderRepository _senders;
		private readonly IStateConfig _stateConfig;
		private readonly ITransitRepository _transits;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IApplicationUpdateRepository _updater;

		public ApplicationSenderManager(
			IApplicationRepository applications,
			ISenderRepository senders,
			IApplicationUpdateRepository updater,
			IForwarderService forwarders,
			IUnitOfWork unitOfWork,
			ITransitRepository transits,
			IStateConfig stateConfig)
		{
			_applications = applications;
			_senders = senders;
			_updater = updater;
			_forwarders = forwarders;
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

			_updater.Update(applicationData);

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
			CopyTransitDataFromClient(clientId, applicationData);
			applicationData.StateId = _stateConfig.DefaultStateId;
			applicationData.Class = null;
			applicationData.StateChangeTimestamp = DateTimeProvider.Now;
			applicationData.CreationTimestamp = DateTimeProvider.Now;
			applicationData.SenderId = senderId;
			applicationData.ClientId = clientId;
			applicationData.CountryId = _senders.Get(senderId).CountryId;

			_updater.Add(applicationData);

			_unitOfWork.SaveChanges();
		}

		private void CopyTransitDataFromClient(long clientId, ApplicationData applicationData)
		{
			var transit = _transits.GetByClient(clientId);

			transit.Id = 0;

			var forwarder = _forwarders.GetByCityOrDefault(transit.CityId);

			var transitId = _transits.Add(transit);

			applicationData.TransitId = transitId;

			applicationData.ForwarderId = forwarder.Id;
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
	}
}