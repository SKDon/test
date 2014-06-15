using Alicargo.Core.Contracts.Users;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class SenderApplicationManager : ISenderApplicationManager
	{
		private readonly IApplicationRepository _applications;
		private readonly IApplicationEditor _editor;
		private readonly IForwarderService _forwarders;
		private readonly ISenderService _senders;
		private readonly ITransitRepository _transits;

		public SenderApplicationManager(
			IApplicationRepository applications,
			ISenderService senders,
			IApplicationEditor editor,
			IForwarderService forwarders,
			ITransitRepository transits)
		{
			_applications = applications;
			_senders = senders;
			_editor = editor;
			_forwarders = forwarders;
			_transits = transits;
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
				FactureCostEx = application.FactureCostEx,
				PickupCost = application.PickupCost,
				CountryId = application.CountryId,
				AddressLoad = application.AddressLoad,
				FactoryContact = application.FactoryContact,
				FactoryEmail = application.FactoryEmail,
				FactoryPhone = application.FactoryPhone,
				WarehouseWorkingTime = application.WarehouseWorkingTime
			};
		}

		public void Update(long id, ApplicationSenderModel model)
		{
			var applicationData = _applications.Get(id);

			if(applicationData.SenderId.HasValue)
			{
				_senders.CheckCountry(applicationData.SenderId.Value, model.CountryId);
			}

			Map(model, applicationData);

			_editor.Update(id, applicationData);
		}

		public void Add(ApplicationSenderModel model, long clientId, long creatorSenderId)
		{
			_senders.CheckCountry(creatorSenderId, model.CountryId);

			var application = new ApplicationEditData
			{
				InsuranceRate = _applications.GetDefaultInsuranceRate()
			};

			Map(model, application);

			Add(application, clientId, creatorSenderId);
		}

		private void Add(ApplicationEditData application, long clientId, long senderId)
		{
			var transit = _transits.GetByClient(clientId);
			transit.Id = 0;
			var transitId = _transits.Add(transit);

			application.TransitId = transitId;
			application.ForwarderId = _forwarders.GetByCityOrAny(transit.CityId, null);
			application.Class = null;
			application.SenderId = senderId;
			application.ClientId = clientId;

			_editor.Add(application);
		}

		private static void Map(ApplicationSenderModel from, ApplicationEditData to)
		{
			to.Count = from.Count;
			to.FactoryName = from.FactoryName;
			to.Weight = from.Weight;
			to.Invoice = from.Invoice;
			to.MarkName = from.MarkName;
			to.Value = from.Currency.Value;
			to.CurrencyId = from.Currency.CurrencyId;
			to.Volume = from.Volume;
			to.FactureCost = from.FactureCost;
			to.FactureCostEx = from.FactureCostEx;
			to.PickupCost = from.PickupCost;
			to.CountryId = from.CountryId;
			to.AddressLoad = from.AddressLoad;
			to.FactoryContact = from.FactoryContact;
			to.FactoryEmail = from.FactoryEmail;
			to.FactoryPhone = from.FactoryPhone;
			to.WarehouseWorkingTime = from.WarehouseWorkingTime;
		}
	}
}