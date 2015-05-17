using Alicargo.Core.Contracts.Users;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class ClientApplicationManager : IClientApplicationManager
	{
		private readonly IApplicationRepository _applications;
		private readonly IClientRepository _clients;
		private readonly IForwarderService _forwarders;
		private readonly ITransitService _transits;
		private readonly IApplicationEditor _updater;

		public ClientApplicationManager(
			IApplicationRepository applications,
			IClientRepository clients,
			IForwarderService forwarders,
			IApplicationEditor updater,
			ITransitService transits)
		{
			_clients = clients;
			_applications = applications;
			_forwarders = forwarders;
			_updater = updater;
			_transits = transits;
		}

		public long Add(ApplicationClientModel application, TransitEditModel transit, long clientId)
		{
			var transitId = _transits.Add(transit, null);

			var forwarderId = _forwarders.GetByCityOrAny(transit.CityId, null);

			var client = _clients.Get(clientId);

			var data = new ApplicationEditData
			{
				Class = null,
				TransitId = transitId,
				Invoice = application.Invoice,
				Characteristic = application.Characteristic,
				AddressLoad = application.AddressLoad,
				WarehouseWorkingTime = application.WarehouseWorkingTime,
				Weight = application.Weight,
				Count = application.Count,
				Volume = application.Volume,
				TermsOfDelivery = application.TermsOfDelivery,
				Value = application.Currency.Value,
				CurrencyId = application.Currency.CurrencyId,
				CountryId = application.CountryId,
				FactoryName = application.FactoryName,
				FactoryPhone = application.FactoryPhone,
				FactoryEmail = application.FactoryEmail,
				FactoryContact = application.FactoryContact,
				MarkName = application.MarkName,
				MethodOfDelivery = application.MethodOfDelivery,
				IsPickup = application.IsPickup,
				AirWaybillId = null,
				DateInStock = null,
				DateOfCargoReceipt = null,
				TransitReference = null,
				ClientId = clientId,
				PickupCost = null,
				TransitCost = null,
				FactureCost = null,
				FactureCostEx = null,
				TariffPerKg = null,
				ScotchCostEdited = null,
				FactureCostEdited = null,
				FactureCostExEdited = null,
				TransitCostEdited = null,
				PickupCostEdited = null,
				SenderId = client.DefaultSenderId,
				SenderRate = null,
				ForwarderId = forwarderId,
				InsuranceRate = _applications.GetDefaultInsuranceRate()
			};

			return _updater.Add(data);
		}

		public void Update(long applicationId, ApplicationClientModel application, TransitEditModel transit)
		{
			var data = _applications.Get(applicationId);

			_transits.Update(data.TransitId, transit, null, applicationId);

			var forwarderId = _forwarders.GetByCityOrAny(transit.CityId, data.ForwarderId);

			Map(application, data, forwarderId);

			_updater.Update(applicationId, data);
		}

		public ApplicationClientModel Get(long applicationId)
		{
			var application = _applications.Get(applicationId);

			return new ApplicationClientModel
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
				CountryId = application.CountryId,
				AddressLoad = application.AddressLoad,
				Characteristic = application.Characteristic,
				FactoryContact = application.FactoryContact,
				FactoryEmail = application.FactoryEmail,
				FactoryPhone = application.FactoryPhone,
				MethodOfDelivery = application.MethodOfDelivery,
				IsPickup = application.IsPickup,
				TermsOfDelivery = application.TermsOfDelivery,
				WarehouseWorkingTime = application.WarehouseWorkingTime
			};
		}

		private static void Map(ApplicationClientModel @from, ApplicationEditData to, long forwarderId)
		{
			to.CountryId = @from.CountryId;
			to.Invoice = @from.Invoice;
			to.Characteristic = @from.Characteristic;
			to.AddressLoad = @from.AddressLoad;
			to.WarehouseWorkingTime = @from.WarehouseWorkingTime;
			to.Weight = @from.Weight;
			to.Count = @from.Count;
			to.Volume = @from.Volume;
			to.TermsOfDelivery = @from.TermsOfDelivery;
			to.Value = @from.Currency.Value;
			to.CurrencyId = @from.Currency.CurrencyId;
			to.FactoryName = @from.FactoryName;
			to.FactoryPhone = @from.FactoryPhone;
			to.FactoryEmail = @from.FactoryEmail;
			to.FactoryContact = @from.FactoryContact;
			to.IsPickup = @from.IsPickup;
			to.MarkName = @from.MarkName;
			to.MethodOfDelivery = @from.MethodOfDelivery;
			to.ForwarderId = forwarderId;
		}
	}
}