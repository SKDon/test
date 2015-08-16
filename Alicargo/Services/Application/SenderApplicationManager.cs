using Alicargo.Core.Contracts.Users;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Contracts.User;
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
		private readonly ITransitRepository _transits;

		public SenderApplicationManager(
			IApplicationRepository applications,
			IApplicationEditor editor,
			IForwarderService forwarders,
			ITransitRepository transits)
		{
			_applications = applications;
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
				CountryId = application.CountryId,
				AddressLoad = application.AddressLoad,
				FactoryContact = application.FactoryContact,
				FactoryEmail = application.FactoryEmail,
				FactoryPhone = application.FactoryPhone,
				WarehouseWorkingTime = application.WarehouseWorkingTime,
				MRN = application.MRN,
				CountInInvoce = application.CountInInvoce,
				DocumentWeight = application.DocumentWeight,
				FactureCost = application.FactureCost,
				FactureCostEx = application.FactureCostEx,
				PickupCost = application.PickupCost,
				TransitCost = application.TransitCost,
				TariffPerKg = application.TariffPerKg
			};
		}

		public void Update(long id, ApplicationSenderModel model)
		{
			var applicationData = _applications.Get(id);

			Map(model, applicationData);

			_editor.Update(id, applicationData);
		}

		public void Add(ApplicationSenderModel model, ClientData client, long creatorSenderId)
		{
			var application = new ApplicationEditData
			{
				InsuranceRate = client.InsuranceRate ?? _applications.GetDefaultInsuranceRate(),
				ScotchCostEdited = client.ScotchCostEdited
			};

			Map(model, application);

			Add(application, client.ClientId, creatorSenderId);
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
			to.TransitCost = from.TransitCost;
			to.CountryId = from.CountryId;
			to.AddressLoad = from.AddressLoad;
			to.FactoryContact = from.FactoryContact;
			to.FactoryEmail = from.FactoryEmail;
			to.FactoryPhone = from.FactoryPhone;
			to.WarehouseWorkingTime = from.WarehouseWorkingTime;
			to.CountInInvoce = from.CountInInvoce;
			to.DocumentWeight = from.DocumentWeight;
			to.MRN = from.MRN;
			to.TariffPerKg = from.TariffPerKg;
		}
	}
}