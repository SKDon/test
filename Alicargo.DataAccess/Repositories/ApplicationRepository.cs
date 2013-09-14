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
	internal sealed class ApplicationRepository : BaseRepository, IApplicationRepository
	{
		private readonly IApplicationRepositoryOrderer _orderer;
		private readonly Expression<Func<Application, ApplicationData>> _selector;

		public ApplicationRepository(IUnitOfWork unitOfWork, IApplicationRepositoryOrderer orderer)
			: base(unitOfWork)
		{
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
				ClientId = x.ClientId,
				TransitId = x.TransitId,
				FactureCost = x.FactureCost,
				ScotchCost = x.ScotchCost,
				TariffPerKg = x.TariffPerKg,
				TransitCost = x.TransitCost,
				WithdrawCost = x.WithdrawCost,
				ForwarderCost = x.ForwarderCost
			};
		}

		public ApplicationData Get(long id)
		{
			return Context.Applications.Where(x => x.Id == id).Select(_selector).FirstOrDefault();
		}

		// todo: 2. test
		public ApplicationData[] GetByAirWaybill(params long[] ids)
		{
			return Context.Applications
						  .Where(x => x.AirWaybillId.HasValue && ids.Contains(x.AirWaybillId.Value))
						  .Select(_selector)
						  .ToArray();
		}

		public ApplicationDetailsData GetDetails(long id)
		{
			return Context.Applications.Where(x => x.Id == id)
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

		public long Count(IEnumerable<long> stateIds, long? clientUserId = null)
		{
			var applications = Context.Applications.Where(x => stateIds.Contains(x.StateId));

			if (clientUserId.HasValue)
			{
				applications = applications.Where(x => x.Client.UserId == clientUserId.Value);
			}

			return applications.LongCount();
		}

		public long GetClientId(long id)
		{
			return Context.Applications.Where(x => x.Id == id).Select(x => x.ClientId).First();
		}

		public ApplicationListItemData[] List(int take, int skip, long[] stateIds = null,
											  Order[] orders = null, long? clientUserId = null)
		{
			var applications = stateIds != null && stateIds.Length > 0
				? Context.Applications.Where(x => stateIds.Contains(x.StateId))
				: Context.Applications.AsQueryable();

			if (clientUserId.HasValue)
			{
				applications = applications.Where(x => x.Client.UserId == clientUserId.Value);
			}

			applications = _orderer.Order(applications, orders).Skip(skip).Take(take);

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
				ClientNic = x.Client.Nic,
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
				FactureCost = x.FactureCost,
				ForwarderCost = x.ForwarderCost,
				ScotchCost = x.ScotchCost,
				TariffPerKg = x.TariffPerKg,
				TransitCost = x.TransitCost,
				WithdrawCost = x.WithdrawCost
			}).ToArray();
		}

		#region Files

		public FileHolder GetInvoiceFile(long id)
		{
			return GetFile(x => x.Id == id && x.InvoiceFileName != null && x.InvoiceFileData != null,
						   application => new FileHolder
						   {
							   FileName = application.InvoiceFileName,
							   FileData = application.InvoiceFileData.ToArray()
						   });
		}

		public FileHolder GetSwiftFile(long id)
		{
			return GetFile(x => x.Id == id && x.SwiftFileName != null && x.SwiftFileData != null,
						   application => new FileHolder
						   {
							   FileName = application.SwiftFileName,
							   FileData = application.SwiftFileData.ToArray()
						   });
		}

		public FileHolder GetCPFile(long id)
		{
			return GetFile(x => x.Id == id && x.CPFileName != null && x.CPFileData != null,
						   application => new FileHolder
						   {
							   FileName = application.CPFileName,
							   FileData = application.CPFileData.ToArray()
						   });
		}

		public FileHolder GetDeliveryBillFile(long id)
		{
			return GetFile(x => x.Id == id && x.DeliveryBillFileName != null && x.DeliveryBillFileData != null,
						   application => new FileHolder
						   {
							   FileName = application.DeliveryBillFileName,
							   FileData = application.DeliveryBillFileData.ToArray()
						   });
		}

		public FileHolder GetTorg12File(long id)
		{
			return GetFile(x => x.Id == id && x.Torg12FileName != null && x.Torg12FileData != null,
						   application => new FileHolder
						   {
							   FileName = application.Torg12FileName,
							   FileData = application.Torg12FileData.ToArray()
						   });
		}

		public FileHolder GetPackingFile(long id)
		{
			return GetFile(x => x.Id == id && x.PackingFileName != null && x.PackingFileData != null,
						   application => new FileHolder
						   {
							   FileName = application.PackingFileName,
							   FileData = application.PackingFileData.ToArray()
						   });
		}

		private FileHolder GetFile(Expression<Func<Application, bool>> where,
								   Expression<Func<Application, FileHolder>> selector)
		{
			return Context.Applications.Where(where).Select(selector).FirstOrDefault();
		}

		#endregion
	}
}