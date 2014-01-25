using Alicargo.Core.Contracts.State;
using Alicargo.Core.Contracts.Users;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
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
		private readonly ISenderService _senders;
		private readonly IStateConfig _stateConfig;
		private readonly ITransitRepository _transits;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IApplicationEditor _updater;

		public ApplicationSenderManager(
			IApplicationRepository applications,
			ISenderService senders,
			IApplicationEditor updater,
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
				CountryId = application.CountryId
			};
		}

		public void Update(long id, ApplicationSenderModel model)
		{
			var applicationData = _applications.Get(id);

			_senders.CheckCountry(applicationData.SenderId, model.CountryId);

			Map(model, applicationData);

			_updater.Update(applicationData);

			_unitOfWork.SaveChanges();
		}

		public void Add(ApplicationSenderModel model, long clientId, long creatorSenderId)
		{
			_senders.CheckCountry(creatorSenderId, model.CountryId);

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

			_updater.Add(applicationData);
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
			to.CountryId = from.CountryId;
		}
	}
}