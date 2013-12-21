using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class ApplicationRepository : IApplicationRepository
	{
		private readonly AlicargoDataContext _context;
		private readonly IApplicationRepositoryOrderer _orderer;
		private readonly Expression<Func<Application, ApplicationData>> _selector;

		public ApplicationRepository(IUnitOfWork unitOfWork)
		{
			_context = (AlicargoDataContext)unitOfWork.Context;

			_orderer = new ApplicationRepositoryOrderer();

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
				MethodOfDeliveryId = x.MethodOfDeliveryId,
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
				ClassId = x.ClassId,
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
				SenderId = x.SenderId
			};
		}

		public long Count(long[] stateIds, long? clientId = null, long? senderId = null, bool? hasCalculation = null,
			long? cargoReceivedStateId = null, int? cargoReceivedDaysToShow = null)
		{
			var applications = Where(stateIds, clientId, senderId, hasCalculation, cargoReceivedStateId, cargoReceivedDaysToShow);

			return applications.LongCount();
		}

		public ApplicationListItemData[] List(long[] stateIds, Order[] orders, int? take = null,
			int skip = 0, long? clientId = null, long? senderId = null, bool? hasCalculation = null,
			long? cargoReceivedStateId = null, int? cargoReceivedDaysToShow = null)
		{
			var applications = Where(stateIds, clientId, senderId, hasCalculation, cargoReceivedStateId, cargoReceivedDaysToShow);

			applications = _orderer.Order(applications, orders);

			applications = applications.Skip(skip);

			if (take.HasValue)
				applications = applications.Take(take.Value);

			return applications.Select(x => new ApplicationListItemData
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
				TransitCity = x.Transit.City,
				TransitDeliveryTypeId = x.Transit.DeliveryTypeId,
				TransitMethodOfTransitId = x.Transit.MethodOfTransitId,
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
				FactureCost = x.FactureCostEdited ?? x.FactureCost,
				SenderFactureCost = x.FactureCost,
				ScotchCost = x.ScotchCostEdited ?? (x.Sender.TariffOfTapePerBox * x.Count),
				SenderScotchCost = x.Sender.TariffOfTapePerBox * x.Count,
				TariffPerKg = x.TariffPerKg,
				TransitCost = x.TransitCostEdited ?? x.TransitCost,
				ForwarderTransitCost = x.TransitCost,
				PickupCost = x.PickupCostEdited ?? x.PickupCost,
				SenderPickupCost = x.PickupCost,
				SenderRate = x.SenderRate,
				SenderId = x.SenderId
			}).ToArray();
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

		public ApplicationDetailsData GetDetails(long id)
		{
			return _context.Applications.Where(x => x.Id == id)
				.Select(x => new ApplicationDetailsData
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
					CreationTimestamp = x.CreationTimestamp,
					DateInStock = x.DateInStock,
					DateOfCargoReceipt = x.DateOfCargoReceipt,
					FactoryContact = x.FactoryContact,
					FactoryEmail = x.FactoryEmail,
					FactoryPhone = x.FactoryPhone,
					StateChangeTimestamp = x.StateChangeTimestamp,
					StateId = x.StateId,
					TermsOfDelivery = x.TermsOfDelivery,
					TransitAddress = x.Transit.Address,
					TransitCarrierName = x.Transit.Carrier.Name,
					TransitCity = x.Transit.City,
					TransitDeliveryTypeId = x.Transit.DeliveryTypeId,
					TransitMethodOfTransitId = x.Transit.MethodOfTransitId,
					TransitPhone = x.Transit.Phone,
					TransitRecipientName = x.Transit.RecipientName,
					TransitReference = x.TransitReference,
					TransitWarehouseWorkingTime = x.Transit.WarehouseWorkingTime,
					WarehouseWorkingTime = x.WarehouseWorkingTime,
					Weight = x.Weight,
					MethodOfDeliveryId = x.MethodOfDeliveryId,
					Value = x.Value,
					CurrencyId = x.CurrencyId,
					AirWaybillDateOfArrival = x.AirWaybill.DateOfArrival,
					AirWaybillDateOfDeparture = x.AirWaybill.DateOfDeparture,
					AirWaybillGTD = x.AirWaybill.GTD,
					ClientEmail = x.Client.Emails,
					ClientUserId = x.Client.UserId,
					AirWaybillId = x.AirWaybillId,
					ClientNic = x.Client.Nic,
					ClientLegalEntity = x.Client.LegalEntity,
					ClientId = x.ClientId,
					CountryName = new[]
					{
						new KeyValuePair<string, string>(TwoLetterISOLanguageName.English, x.Country.Name_En),
						new KeyValuePair<string, string>(TwoLetterISOLanguageName.Italian, x.Country.Name_En),
						new KeyValuePair<string, string>(TwoLetterISOLanguageName.Russian, x.Country.Name_Ru)
					},
					SenderId = x.SenderId
				}).FirstOrDefault();
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

		private IQueryable<Application> Where(long[] stateIds, long? clientId, long? senderId, bool? hasCalculation,
			long? cargoReceivedStateId, int? cargoReceivedDaysToShow)
		{
			var applications = stateIds != null && stateIds.Length > 0
				? _context.Applications.Where(x => stateIds.Contains(x.StateId))
				: _context.Applications.AsQueryable();

			if (clientId.HasValue)
			{
				applications = applications.Where(x => x.ClientId == clientId.Value);
			}

			if (senderId.HasValue)
			{
				applications = applications.Where(x => x.SenderId == senderId.Value);
			}

			if (hasCalculation.HasValue && hasCalculation.Value)
			{
				applications =
					applications.Where(
						x => _context.Calculations.Any(c => c.ApplicationHistoryId == x.Id && c.ClientId == x.ClientId));
			}

			if (hasCalculation.HasValue && !hasCalculation.Value)
			{
				applications =
					applications.Where(
						x => _context.Calculations.All(c => c.ApplicationHistoryId != x.Id && c.ClientId == x.ClientId));
			}

			if (cargoReceivedStateId.HasValue && cargoReceivedDaysToShow.HasValue)
			{
				var offset = DateTimeOffset.UtcNow.AddDays(-cargoReceivedDaysToShow.Value);

				applications = applications.Where(x => x.StateId != cargoReceivedStateId.Value || x.StateChangeTimestamp > offset);
			}

			return applications;
		}
	}
}