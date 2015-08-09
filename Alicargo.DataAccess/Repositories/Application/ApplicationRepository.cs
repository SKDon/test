using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Helpers;
using Alicargo.Utilities;

namespace Alicargo.DataAccess.Repositories.Application
{
	public sealed class ApplicationRepository : IApplicationRepository
	{
		private readonly AlicargoDataContext _context;
		private readonly Expression<Func<DbContext.Application, ApplicationData>> _selector;
		private readonly IApplicationRepositoryOrderer _orderer;

		public ApplicationRepository(IDbConnection connection)
		{
			_context = new AlicargoDataContext(connection);

			_orderer = new ApplicationRepositoryOrderer();

			_selector = x => new ApplicationData
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
				Class = (ClassType?)x.ClassId,
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
				CarrierName = x.Transit.Carrier.Name,
				CarrierContact = x.Transit.Carrier.Contact,
				CarrierAddress = x.Transit.Carrier.Address,
				CarrierPhone = x.Transit.Carrier.Phone,
				CarrierEmail = x.Transit.Carrier.Email,
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
				MethodOfDelivery = (MethodOfDelivery)x.MethodOfDeliveryId,
				IsPickup =  x.IsPickup,
				Value = x.Value,
				CurrencyId = (CurrencyType)x.CurrencyId,
				AirWaybillId = x.AirWaybillId,
				SenderId = x.SenderId,
				SenderContact = x.Sender.Contact,
				SenderPhone = x.Sender.Phone,
				SenderAddress = x.Sender.Address,
				SenderEmail = x.Sender.Email,
				AirWaybillDateOfArrival = x.AirWaybill.DateOfArrival,
				AirWaybillDateOfDeparture = x.AirWaybill.DateOfDeparture,
				AirWaybillGTD = x.AirWaybill.GTD,
				ClientEmails = x.Client.Emails,
				ClientUserId = x.Client.UserId,
				TariffPerKg = x.TariffPerKg,
				SenderRate = x.SenderRate,
				SenderName = x.Sender.Name,
				FactureCost = x.FactureCost,
				FactureCostEx = x.FactureCostEx,
				SenderScotchCost = x.Sender.TariffOfTapePerBox * x.Count,
				TransitCost = x.TransitCost,
				PickupCost = x.PickupCost,
				ForwarderName = x.Forwarder.Name,
				ForwarderId = x.ForwarderId,
				InsuranceRate = x.InsuranceRate,
				CalculationProfit = x.CalculationProfit,
				CalculationTotalTariffCost = x.CalculationTotalTariffCost,
				DisplayNumber = x.DisplayNumber,
				FactureCostEdited = x.FactureCostEdited,
				FactureCostExEdited = x.FactureCostExEdited,
				PickupCostEdited = x.PickupCostEdited,
				ScotchCostEdited = x.ScotchCostEdited,
				TransitCostEdited = x.TransitCostEdited,
				CountInInvoce = x.CountInInvoce,
				DocumentWeight = x.DocumentWeight,
				MRN = x.MRN
			};			
		}

		public long Count(long[] stateIds, long? clientId = null, long? senderId = null, long? carrierId = null,
			long? forwarderId = null, long? cargoReceivedStateId = null, int? cargoReceivedDaysToShow = null,
			bool? hasCalculation = null)
		{
			var applications = Where(stateIds, clientId, senderId, hasCalculation, cargoReceivedStateId, cargoReceivedDaysToShow,
				forwarderId, carrierId);

			return applications.LongCount();
		}

		public ApplicationData[] List(long[] stateIds, Order[] orders, int? take = null, int skip = 0,
			long? clientId = null, long? senderId = null, long? carrierId = null, long? forwarderId = null,
			long? cargoReceivedStateId = null, int? cargoReceivedDaysToShow = null, bool? hasCalculation = null)
		{
			var applications = Where(stateIds, clientId, senderId, hasCalculation, cargoReceivedStateId, cargoReceivedDaysToShow,
				forwarderId, carrierId);

			applications = _orderer.Order(applications, orders);

			applications = applications.Skip(skip);

			if(take.HasValue)
				applications = applications.Take(take.Value);

			return applications.Select(_selector).ToArray();
		}

		public ApplicationData[] GetByAirWaybill(params long[] ids)
		{
			return _context.Applications
				.Where(x => x.AirWaybillId.HasValue && ids.Contains(x.AirWaybillId.Value))
				.Select(_selector)
				.ToArray();
		}

		public float GetDefaultInsuranceRate()
		{
			return (float)ConfigurationManager.AppSettings["DefaultInsuranceRate"].ToDouble();
		}

		public ApplicationData Get(long id)
		{
			return _context.Applications.Where(x => x.Id == id).Select(_selector).FirstOrDefault();
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

		private IQueryable<DbContext.Application> Where(long[] stateIds, long? clientId, long? senderId, bool? hasCalculation,
			long? cargoReceivedStateId, int? cargoReceivedDaysToShow, long? forwarderId, long? carrierId)
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