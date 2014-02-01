using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Helpers;
using Alicargo.Utilities;

namespace Alicargo.DataAccess.Repositories.Application
{
	public sealed class ApplicationRepository : IApplicationRepository
	{
		private readonly AlicargoDataContext _context;
		private readonly Expression<Func<DbContext.Application, ApplicationExtendedData>> _extendedSelector;
		private readonly IApplicationRepositoryOrderer _orderer;
		private readonly Expression<Func<DbContext.Application, ApplicationData>> _selector;

		public ApplicationRepository(IUnitOfWork unitOfWork)
		{
			_context = (AlicargoDataContext)unitOfWork.Context;

			_orderer = new ApplicationRepositoryOrderer();

			_extendedSelector = x => new ApplicationExtendedData
			{
				AddressLoad = x.AddressLoad,
				Id = x.Id,
				FactoryName = x.FactoryName,
				Invoice = x.Invoice,
				MarkName = x.MarkName,
				Volume = x.Volume,
				Count = x.Count,
				AirWaybill = x.AirWaybill.Bill,
				Characteristic = x.Characteristic,
				ClientLegalEntity = x.Client.LegalEntity,
				ClientId = x.ClientId,
				ClientNic = x.Client.Nic,
				CountryId = x.CountryId,
				ClassId = x.ClassId,
				CreationTimestamp = x.CreationTimestamp,
				DateInStock = x.DateInStock,
				DateOfCargoReceipt = x.DateOfCargoReceipt,
				FactoryContact = x.FactoryContact,
				FactoryEmail = x.FactoryEmail,
				FactoryPhone = x.FactoryPhone,
				StateChangeTimestamp = x.StateChangeTimestamp,
				StateId = x.StateId,
				TermsOfDelivery = x.TermsOfDelivery,
				TransitId = x.TransitId,
				TransitAddress = x.Transit.Address,
				TransitCarrierName = x.Transit.Carrier.Name,
				CarrierId = x.Transit.CarrierId,
				TransitCityId = x.Transit.CityId,
				TransitDeliveryType = (DeliveryType)x.Transit.DeliveryTypeId,
				TransitMethodOfTransit = (MethodOfTransit)x.Transit.MethodOfTransitId,
				TransitPhone = x.Transit.Phone,
				TransitRecipientName = x.Transit.RecipientName,
				TransitReference = x.TransitReference,
				TransitWarehouseWorkingTime = x.Transit.WarehouseWorkingTime,
				WarehouseWorkingTime = x.WarehouseWorkingTime,
				Weight = x.Weight,
				MethodOfDeliveryId = x.MethodOfDeliveryId,
				Value = x.Value,
				CurrencyId = x.CurrencyId,
				AirWaybillId = x.AirWaybillId,
				SenderId = x.SenderId,
				AirWaybillDateOfArrival = x.AirWaybill.DateOfArrival,
				AirWaybillDateOfDeparture = x.AirWaybill.DateOfDeparture,
				AirWaybillGTD = x.AirWaybill.GTD,
				ClientEmail = x.Client.Emails,
				ClientUserId = x.Client.UserId,
				TariffPerKg = x.TariffPerKg,
				SenderRate = x.SenderRate,
				SenderName = x.Sender.Name,
				FactureCost = x.FactureCostEdited ?? x.FactureCost,
				SenderFactureCost = x.FactureCost,
				SenderScotchCost = x.Sender.TariffOfTapePerBox * x.Count,
				ScotchCost = x.ScotchCostEdited ?? (x.Sender.TariffOfTapePerBox * x.Count),
				TransitCost = x.TransitCostEdited ?? x.TransitCost,
				ForwarderTransitCost = x.TransitCost,
				PickupCost = x.PickupCostEdited ?? x.PickupCost,
				SenderPickupCost = x.PickupCost,
				ForwarderName = x.Forwarder.Name,
				ForwarderId = x.ForwarderId
			};

			_selector = x => new ApplicationData
			{
				Id = x.Id,
				CreationTimestamp = x.CreationTimestamp,
				FactoryEmail = x.FactoryEmail,
				FactoryName = x.FactoryName,
				FactoryPhone = x.FactoryPhone,
				FactoryContact = x.FactoryContact,
				Weight = x.Weight,
				AddressLoad = x.AddressLoad,
				Characteristic = x.Characteristic,
				Count = x.Count,
				Invoice = x.Invoice,
				MethodOfDelivery = (MethodOfDelivery)x.MethodOfDeliveryId,
				TermsOfDelivery = x.TermsOfDelivery,
				Value = x.Value,
				CurrencyId = x.CurrencyId,
				Volume = x.Volume,
				WarehouseWorkingTime = x.WarehouseWorkingTime,
				DateInStock = x.DateInStock,
				DateOfCargoReceipt = x.DateOfCargoReceipt,
				TransitReference = x.TransitReference,
				MarkName = x.MarkName,
				AirWaybillId = x.AirWaybillId,
				CountryId = x.CountryId,
				StateChangeTimestamp = x.StateChangeTimestamp,
				StateId = x.StateId,
				Class = (ClassType?)x.ClassId,
				ClientId = x.ClientId,
				TransitId = x.TransitId,
				FactureCost = x.FactureCost,
				TariffPerKg = x.TariffPerKg,
				SenderRate = x.SenderRate,
				TransitCost = x.TransitCost,
				PickupCost = x.PickupCost,
				FactureCostEdited = x.FactureCostEdited,
				TransitCostEdited = x.TransitCostEdited,
				ScotchCostEdited = x.ScotchCostEdited,
				PickupCostEdited = x.PickupCostEdited,
				SenderId = x.SenderId,
				ForwarderId = x.ForwarderId
			};
		}

		public long Count(long[] stateIds, long? clientId = null, long? senderId = null, bool? hasCalculation = null,
			long? cargoReceivedStateId = null, int? cargoReceivedDaysToShow = null, long? forwarderId = null,
			long? carrierId = null)
		{
			var applications = Where(stateIds, clientId, senderId, hasCalculation, cargoReceivedStateId, cargoReceivedDaysToShow,
				forwarderId, carrierId);

			return applications.LongCount();
		}

		public ApplicationExtendedData[] List(long[] stateIds, Order[] orders, int? take = null, int skip = 0,
			long? clientId = null, long? senderId = null, bool? hasCalculation = null, long? cargoReceivedStateId = null,
			int? cargoReceivedDaysToShow = null, long? forwarderId = null, long? carrierId = null)
		{
			var applications = Where(stateIds, clientId, senderId, hasCalculation, cargoReceivedStateId, cargoReceivedDaysToShow,
				forwarderId, carrierId);

			applications = _orderer.Order(applications, orders);

			applications = applications.Skip(skip);

			if(take.HasValue)
				applications = applications.Take(take.Value);

			return applications.Select(_extendedSelector).ToArray();
		}

		public ApplicationData Get(long id)
		{
			return _context.Applications.Where(x => x.Id == id).Select(_selector).FirstOrDefault();
		}

		public ApplicationData[] GetByAirWaybill(params long[] ids)
		{
			return _context.Applications
				.Where(x => x.AirWaybillId.HasValue && ids.Contains(x.AirWaybillId.Value))
				.Select(_selector)
				.ToArray();
		}

		public ApplicationExtendedData GetExtendedData(long id)
		{
			return _context.Applications.Where(x => x.Id == id).Select(_extendedSelector).FirstOrDefault();
		}

		public long GetClientId(long id)
		{
			return _context.Applications.Where(x => x.Id == id).Select(x => x.ClientId).First();
		}

		public IReadOnlyDictionary<long, long> GetCalculations(long[] appIds)
		{
			return _context.Calculations
				.Where(x => appIds.Contains(x.ApplicationHistoryId))
				.ToDictionary(x => x.ApplicationHistoryId, x => x.Id);
		}

		private IQueryable<DbContext.Application> Where(long[] stateIds, long? clientId, long? senderId, bool? hasCalculation, long? cargoReceivedStateId, int? cargoReceivedDaysToShow, long? forwarderId, long? carrierId)
		{
			var applications = stateIds != null
				? _context.Applications.Where(x => stateIds.Contains(x.StateId))
				: _context.Applications.AsQueryable();

			if(clientId.HasValue)
			{
				applications = applications.Where(x => x.ClientId == clientId.Value);
			}

			if(senderId.HasValue)
			{
				applications = applications.Where(x => x.SenderId == senderId.Value);
			}

			if(hasCalculation.HasValue && hasCalculation.Value)
			{
				applications = applications.Where(
					x => _context.Calculations.Any(c => c.ApplicationHistoryId == x.Id && c.ClientId == x.ClientId));
			}

			if(hasCalculation.HasValue && !hasCalculation.Value)
			{
				applications = applications.Where(
					x => _context.Calculations.All(c => c.ApplicationHistoryId != x.Id && c.ClientId == x.ClientId));
			}

			if(carrierId.HasValue)
			{
				applications = applications.Where(x => x.Transit.CarrierId == carrierId.Value);
			}

			if(forwarderId.HasValue)
			{
				applications = applications.Where(x => x.ForwarderId == forwarderId.Value);

				if(cargoReceivedStateId.HasValue && cargoReceivedDaysToShow.HasValue)
				{
					var offset = DateTimeProvider.Now.AddDays(-cargoReceivedDaysToShow.Value);

					applications = applications.Where(x => x.StateId != cargoReceivedStateId.Value || x.StateChangeTimestamp > offset);
				}
			}

			return applications;
		}
	}
}