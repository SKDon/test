using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class ApplicationRepository : IApplicationRepository
	{
		private readonly AlicargoDataContext _context;
		private readonly IApplicationRepositoryOrderer _orderer;
		private readonly Expression<Func<Application, ApplicationData>> _selector;

		public ApplicationRepository(IUnitOfWork unitOfWork, IApplicationRepositoryOrderer orderer)
		{
			_context = (AlicargoDataContext)unitOfWork.Context;

			_orderer = orderer;

			_selector = x => new ApplicationData
			{
				Id = x.Id,
				CreationTimestamp = x.CreationTimestamp,
				FactoryEmail = x.FactoryEmail,
				FactoryName = x.FactoryName,
				FactoryPhone = x.FactoryPhone,
				FactoryContact = x.FactoryContact,
				InvoiceFileName = x.InvoiceFileName,
				CPFileName = x.CPFileName,
				DeliveryBillFileName = x.DeliveryBillFileName,
				SwiftFileName = x.SwiftFileName,
				Torg12FileName = x.Torg12FileName,
				PackingFileName = x.PackingFileName,
				Weigth = x.Weight,
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

		public ApplicationData Get(long id)
		{
			return _context.Applications.Where(x => x.Id == id).Select(_selector).FirstOrDefault();
		}

		// todo: 2. test
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
							   PackingFileName = x.PackingFileName,
							   FactoryName = x.FactoryName,
							   Invoice = x.Invoice,
							   InvoiceFileName = x.InvoiceFileName,
							   MarkName = x.MarkName,
							   SwiftFileName = x.SwiftFileName,
							   Volume = x.Volume,
							   Count = x.Count,
							   AirWaybill = x.AirWaybill.Bill,
							   CPFileName = x.CPFileName,
							   Characteristic = x.Characteristic,
							   CountryId = x.CountryId,
							   CreationTimestamp = x.CreationTimestamp,
							   DateInStock = x.DateInStock,
							   DateOfCargoReceipt = x.DateOfCargoReceipt,
							   DeliveryBillFileName = x.DeliveryBillFileName,
							   FactoryContact = x.FactoryContact,
							   FactoryEmail = x.FactoryEmail,
							   FactoryPhone = x.FactoryPhone,
							   StateChangeTimestamp = x.StateChangeTimestamp,
							   StateId = x.StateId,
							   TermsOfDelivery = x.TermsOfDelivery,
							   Torg12FileName = x.Torg12FileName,
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
							   Weigth = x.Weight,
							   MethodOfDeliveryId = x.MethodOfDeliveryId,
							   Value = x.Value,
							   CurrencyId = x.CurrencyId,
							   AirWaybillDateOfArrival = x.AirWaybill.DateOfArrival,
							   AirWaybillDateOfDeparture = x.AirWaybill.DateOfDeparture,
							   AirWaybillGTD = x.AirWaybill.GTD,
							   ClientEmail = x.Client.Email,
							   ClientUserId = x.Client.UserId,
							   Weight = x.Weight,
							   AirWaybillId = x.AirWaybillId,
							   ClientNic = x.Client.Nic,
							   ClientLegalEntity = x.Client.LegalEntity
						   }).FirstOrDefault();
		}

		public long Count(IEnumerable<long> stateIds, long? clientId = null, long? senderId = null)
		{
			var applications = _context.Applications.Where(x => stateIds.Contains(x.StateId));

			if (clientId.HasValue)
			{
				applications = applications.Where(x => x.ClientId == clientId.Value);
			}

			if (senderId.HasValue)
			{
				applications = applications.Where(x => x.SenderId == senderId.Value);
			}

			return applications.LongCount();
		}

		public long GetClientId(long id)
		{
			return _context.Applications.Where(x => x.Id == id).Select(x => x.ClientId).First();
		}

		public ApplicationListItemData[] List(int? take = null, int skip = 0, long[] stateIds = null,
			Order[] orders = null, long? clientId = null, long? senderId = null)
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

			applications = _orderer.Order(applications, orders).Skip(skip);
			if (take.HasValue)
				applications = applications.Take(take.Value);

			return applications.Select(x => new ApplicationListItemData
			{
				AddressLoad = x.AddressLoad,
				Id = x.Id,
				PackingFileName = x.PackingFileName,
				FactoryName = x.FactoryName,
				Invoice = x.Invoice,
				InvoiceFileName = x.InvoiceFileName,
				MarkName = x.MarkName,
				SwiftFileName = x.SwiftFileName,
				Volume = x.Volume,
				Count = x.Count,
				AirWaybill = x.AirWaybill.Bill,
				CPFileName = x.CPFileName,
				Characteristic = x.Characteristic,
				ClientLegalEntity = x.Client.LegalEntity,
				ClientId = x.ClientId,
				ClientNic = x.Client.Nic,
				CountryId = x.CountryId,
				ClassId = x.ClassId,
				CreationTimestamp = x.CreationTimestamp,
				DateInStock = x.DateInStock,
				DateOfCargoReceipt = x.DateOfCargoReceipt,
				DeliveryBillFileName = x.DeliveryBillFileName,
				FactoryContact = x.FactoryContact,
				FactoryEmail = x.FactoryEmail,
				FactoryPhone = x.FactoryPhone,
				StateChangeTimestamp = x.StateChangeTimestamp,
				StateId = x.StateId,
				TermsOfDelivery = x.TermsOfDelivery,
				Torg12FileName = x.Torg12FileName,
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
				Weigth = x.Weight,
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
				PickupCost = x.PickupCostEdited ?? x.PickupCost,
				SenderPickupCost = x.PickupCost,
				SenderRate = x.SenderRate,
				SenderId = x.SenderId
			}).ToArray();
		}

		#region Files

		public FileHolder GetInvoiceFile(long id)
		{
			return GetFile(x => x.Id == id && x.InvoiceFileName != null && x.InvoiceFileData != null,
				application => new FileHolder
				{
					Name = application.InvoiceFileName,
					Data = application.InvoiceFileData.ToArray()
				});
		}

		public FileHolder GetSwiftFile(long id)
		{
			return GetFile(x => x.Id == id && x.SwiftFileName != null && x.SwiftFileData != null,
				application => new FileHolder
				{
					Name = application.SwiftFileName,
					Data = application.SwiftFileData.ToArray()
				});
		}

		public FileHolder GetCPFile(long id)
		{
			return GetFile(x => x.Id == id && x.CPFileName != null && x.CPFileData != null,
				application => new FileHolder
				{
					Name = application.CPFileName,
					Data = application.CPFileData.ToArray()
				});
		}

		public FileHolder GetDeliveryBillFile(long id)
		{
			return GetFile(x => x.Id == id && x.DeliveryBillFileName != null && x.DeliveryBillFileData != null,
				application => new FileHolder
				{
					Name = application.DeliveryBillFileName,
					Data = application.DeliveryBillFileData.ToArray()
				});
		}

		public FileHolder GetTorg12File(long id)
		{
			return GetFile(x => x.Id == id && x.Torg12FileName != null && x.Torg12FileData != null,
				application => new FileHolder
				{
					Name = application.Torg12FileName,
					Data = application.Torg12FileData.ToArray()
				});
		}

		public FileHolder GetPackingFile(long id)
		{
			return GetFile(x => x.Id == id && x.PackingFileName != null && x.PackingFileData != null,
				application => new FileHolder
				{
					Name = application.PackingFileName,
					Data = application.PackingFileData.ToArray()
				});
		}

		// todo: 1. test
		public IReadOnlyDictionary<long, long> GetCalculations(long[] appIds)
		{
			return _context.Calculations
						   .Where(x => appIds.Contains(x.ApplicationHistoryId))
						   .ToDictionary(x => x.ApplicationHistoryId, x => x.Id);
		}

		private FileHolder GetFile(Expression<Func<Application, bool>> where,
			Expression<Func<Application, FileHolder>> selector)
		{
			return _context.Applications.Where(where).Select(selector).FirstOrDefault();
		}

		#endregion
	}
}