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
	internal sealed class SenderApplicationManager : ISenderApplicationManager
	{
		private readonly IApplicationRepository _applications;
		private readonly IForwarderService _forwarders;
		private readonly ISenderService _senders;
		private readonly IStateConfig _stateConfig;
		private readonly ITransitRepository _transits;
		private readonly IApplicationEditor _updater;

		public SenderApplicationManager(
			IApplicationRepository applications,
			ISenderService senders,
			IApplicationEditor updater,
			IForwarderService forwarders,
			ITransitRepository transits,
			IStateConfig stateConfig)
		{
			_applications = applications;
			_senders = senders;
			_updater = updater;
			_forwarders = forwarders;
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
		}

		public void Add(ApplicationSenderModel model, long clientId, long creatorSenderId)
		{
			_senders.CheckCountry(creatorSenderId, model.CountryId);

			var application = new ApplicationData();

			Map(model, application);

			Add(application, clientId, creatorSenderId);
		}

		private void Add(ApplicationData application, long clientId, long senderId)
		{
			var transit = _transits.GetByClient(clientId);
			transit.Id = 0;
			var transitId = _transits.Add(transit);

			application.TransitId = transitId;
			application.ForwarderId = _forwarders.GetByCityOrAny(transit.CityId, null);
			application.StateId = _stateConfig.DefaultStateId;
			application.Class = null;
			application.StateChangeTimestamp = DateTimeProvider.Now;
			application.CreationTimestamp = DateTimeProvider.Now;
			application.SenderId = senderId;
			application.ClientId = clientId;

			_updater.Add(application);
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