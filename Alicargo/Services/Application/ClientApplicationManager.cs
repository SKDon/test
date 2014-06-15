using Alicargo.Core.Contracts.Users;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class ClientApplicationManager : IClientApplicationManager
	{
		private readonly IApplicationRepository _applications;
		private readonly IForwarderService _forwarders;
		private readonly ITransitService _transits;
		private readonly IApplicationEditor _updater;

		public ClientApplicationManager(
			IApplicationRepository applications,
			IForwarderService forwarders,
			IApplicationEditor updater,
			ITransitService transits)
		{
			_applications = applications;
			_forwarders = forwarders;
			_updater = updater;
			_transits = transits;
		}

		public long Add(ApplicationClientModel application, TransitEditModel transit, long clientId)
		{
			var transitId = _transits.Add(transit, null);

			var forwarderId = _forwarders.GetByCityOrAny(transit.CityId, null);

			var data = GetNewApplicationData(application, clientId, transitId, forwarderId);

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
				TermsOfDelivery = application.TermsOfDelivery,
				WarehouseWorkingTime = application.WarehouseWorkingTime
			};
		}

		private ApplicationEditData GetNewApplicationData(
			ApplicationClientModel model, long clientId, long transitId,
			long forwarderId)
		{
			return new ApplicationEditData
			{
				Class = null,
				TransitId = transitId,
				Invoice = model.Invoice,
				Characteristic = model.Characteristic,
				AddressLoad = model.AddressLoad,
				WarehouseWorkingTime = model.WarehouseWorkingTime,
				Weight = model.Weight,
				Count = model.Count,
				Volume = model.Volume,
				TermsOfDelivery = model.TermsOfDelivery,
				Value = model.Currency.Value,
				CurrencyId = model.Currency.CurrencyId,
				CountryId = model.CountryId,
				FactoryName = model.FactoryName,
				FactoryPhone = model.FactoryPhone,
				FactoryEmail = model.FactoryEmail,
				FactoryContact = model.FactoryContact,
				MarkName = model.MarkName,
				MethodOfDelivery = model.MethodOfDelivery,
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
				SenderId = null,
				SenderRate = null,
				ForwarderId = forwarderId,
				InsuranceRate = _applications.GetDefaultInsuranceRate()
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
			to.MarkName = @from.MarkName;
			to.MethodOfDelivery = @from.MethodOfDelivery;
			to.ForwarderId = forwarderId;
		}
	}
}