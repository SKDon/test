using System.Linq;
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
	internal sealed class ClientApplicationManager : IClientApplicationManager
	{
		private readonly IApplicationRepository _applications;
		private readonly IStateConfig _config;
		private readonly IForwarderService _forwarders;
		private readonly ISenderRepository _senders;
		private readonly ITransitService _transits;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IApplicationUpdateRepository _updater;

		public ClientApplicationManager(
			IApplicationRepository applications,
			IForwarderService forwarders,
			IApplicationUpdateRepository updater,
			ISenderRepository senders,
			IStateConfig config,
			ITransitService transits,
			IUnitOfWork unitOfWork)
		{
			_applications = applications;
			_forwarders = forwarders;
			_updater = updater;
			_senders = senders;
			_config = config;
			_transits = transits;
			_unitOfWork = unitOfWork;
		}

		public long Add(
			ApplicationClientModel model,
			CarrierSelectModel carrierModel,
			TransitEditModel transitModel,
			long clientId)
		{
			var transitId = _transits.AddTransit(transitModel, carrierModel);

			var forwarder = _forwarders.GetByCityOrDefault(transitModel.CityId);

			var senderId = GetSenderId(model.CountryId);

			var data = GetNewApplicationData(model, clientId, transitId, senderId, forwarder.Id);

			var id = _updater.Add(data);

			_unitOfWork.SaveChanges();

			return id();
		}

		public void Update(
			long applicationId,
			ApplicationClientModel model,
			CarrierSelectModel carrierModel,
			TransitEditModel transitModel)
		{
			var data = _applications.Get(applicationId);

			_transits.Update(data.TransitId, transitModel, carrierModel);

			var forwarder = _forwarders.GetByCityOrDefault(transitModel.CityId);

			Map(model, data, forwarder.Id);

			_updater.Update(data);

			_unitOfWork.SaveChanges();
		}

		public ApplicationClientModel Get(long id)
		{
			var application = _applications.Get(id);

			var model = GetModel(application);

			return model;
		}

		private static ApplicationClientModel GetModel(ApplicationData application)
		{
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

		private ApplicationData GetNewApplicationData(ApplicationClientModel model, long clientId, long transitId,
			long senderId, long forwarderId)
		{
			return new ApplicationData
			{
				CreationTimestamp = DateTimeProvider.Now,
				StateChangeTimestamp = DateTimeProvider.Now,
				StateId = _config.DefaultStateId,
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
				Id = 0,
				AirWaybillId = null,
				DateInStock = null,
				DateOfCargoReceipt = null,
				TransitReference = null,
				ClientId = clientId,
				PickupCost = null,
				TransitCost = null,
				FactureCost = null,
				TariffPerKg = null,
				ScotchCostEdited = null,
				FactureCostEdited = null,
				TransitCostEdited = null,
				PickupCostEdited = null,
				SenderId = senderId,
				SenderRate = null,
				ForwarderId = forwarderId
			};
		}

		private long GetSenderId(long countryId)
		{
			var id = _senders.GetByCountry(countryId).FirstOrDefault();

			return id == 0 ? _senders.GetAll().First().EntityId : id;
		}

		private static void Map(ApplicationClientModel @from, ApplicationData to, long forwarderId)
		{
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
			to.CountryId = @from.CountryId;
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