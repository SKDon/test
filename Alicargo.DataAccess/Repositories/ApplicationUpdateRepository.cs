using System;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class ApplicationUpdateRepository : BaseRepository, IApplicationUpdateRepository
	{
		public ApplicationUpdateRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

		public Func<long> Add(ApplicationData application, byte[] swiftFile, byte[] invoiceFile,
							  byte[] cpFile, byte[] deliveryBillFile, byte[] torg12File, byte[] packingFile)
		{
			var entity = new Application();

			CopyTo(application, swiftFile, invoiceFile, cpFile, deliveryBillFile, torg12File, packingFile, entity);

			Context.Applications.InsertOnSubmit(entity);

			return () => entity.Id;
		}

		public void Delete(long id)
		{
			var application = Context.Applications.First(x => x.Id == id);
			Context.Applications.DeleteOnSubmit(application);
		}

		public void SetState(long id, long stateId)
		{
			Update(id, application =>
			{
				application.StateId = stateId;
				application.StateChangeTimestamp = DateTimeOffset.UtcNow;
			});
		}

		// todo: 3. bb test
		public void SetAirWaybill(long id, long? airWaybillId)
		{
			Update(id, application => application.AirWaybillId = airWaybillId);
		}

		// todo: 3. bb test
		public void SetDateInStock(long id, DateTimeOffset dateTimeOffset)
		{
			Update(id, application => application.DateInStock = dateTimeOffset);
		}

		public void SetTransitReference(long id, string transitReference)
		{
			Update(id, application => application.TransitReference = transitReference);
		}

		public void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt)
		{
			Update(id, application => application.DateOfCargoReceipt = dateOfCargoReceipt);
		}

		public void SetTransitCost(long id, decimal? transitCost)
		{
			Update(id, application => application.TransitCost = transitCost);
		}

		public void SetTariffPerKg(long id, decimal? tariffPerKg)
		{
			Update(id, application => application.TariffPerKg = tariffPerKg);
		}

		public void SetWithdrawCostEdited(long id, decimal? withdrawCost)
		{
			Update(id, application => application.WithdrawCostEdited = withdrawCost);
		}

		public void SetFactureCostEdited(long id, decimal? factureCost)
		{
			Update(id, application => application.FactureCostEdited = factureCost);
		}

		public void SetScotchCostEdited(long id, decimal? scotchCost)
		{
			Update(id, application => application.ScotchCostEdited = scotchCost);
		}

		public void SetSenderRate(long id, decimal? senderRate)
		{
			Update(id, application => application.SenderRate = senderRate);
		}

		public void Update(ApplicationData application, byte[] swiftFile, byte[] invoiceFile,
						   byte[] cpFile, byte[] deliveryBillFile, byte[] torg12File, byte[] packingFile)
		{
			Update(application.Id, entity =>
									   CopyTo(application, swiftFile, invoiceFile, cpFile, deliveryBillFile, torg12File, packingFile, entity));
		}

		private void Update(long id, Action<Application> action)
		{
			var application = Context.Applications.First(x => x.Id == id);
			action(application);
		}

		private static void CopyTo(ApplicationData from, byte[] swiftFile, byte[] invoiceFile,
								   byte[] cpFile, byte[] deliveryBillFile, byte[] torg12File, byte[] packingFile, Application to)
		{
			if (to.Id == 0)
			{
				to.Id = @from.Id;
				to.CreationTimestamp = @from.CreationTimestamp;
				to.StateChangeTimestamp = @from.StateChangeTimestamp;
				to.StateId = @from.StateId;
			}

			to.Characteristic = @from.Characteristic;
			to.AddressLoad = @from.AddressLoad;
			to.WarehouseWorkingTime = @from.WarehouseWorkingTime;
			to.Weight = @from.Weigth;
			to.Count = @from.Count;
			to.Volume = @from.Volume;
			to.TermsOfDelivery = @from.TermsOfDelivery;
			to.Value = @from.Value;
			to.CurrencyId = @from.CurrencyId;
			to.MethodOfDeliveryId = @from.MethodOfDeliveryId;
			to.DateInStock = @from.DateInStock;
			to.DateOfCargoReceipt = @from.DateOfCargoReceipt;
			to.TransitReference = @from.TransitReference;

			to.ClientId = @from.ClientId;
			to.TransitId = @from.TransitId;
			to.AirWaybillId = @from.AirWaybillId;
			to.CountryId = @from.CountryId;
			to.SenderId = from.SenderId;

			to.FactoryName = @from.FactoryName;
			to.FactoryPhone = @from.FactoryPhone;
			to.FactoryEmail = @from.FactoryEmail;
			to.FactoryContact = @from.FactoryContact;
			to.MarkName = @from.MarkName;
			to.Invoice = @from.Invoice;

			to.FactureCost = @from.FactureCost;
			to.ScotchCost = @from.ScotchCost;
			to.TariffPerKg = @from.TariffPerKg;
			to.TransitCost = @from.TransitCost;
			to.WithdrawCost = @from.WithdrawCost;
			to.ForwarderCost = @from.ForwarderCost;
			to.FactureCostEdited = @from.FactureCostEdited;
			to.WithdrawCostEdited = @from.WithdrawCostEdited;
			to.ScotchCostEdited = @from.ScotchCostEdited;

			// todo: 3.0. separate repository for files
			FileDataHelper.SetFile(invoiceFile, @from.InvoiceFileName,
								   bytes => to.InvoiceFileData = bytes, s => to.InvoiceFileName = s);

			FileDataHelper.SetFile(packingFile, @from.PackingFileName,
								   bytes => to.PackingFileData = bytes, s => to.PackingFileName = s);

			FileDataHelper.SetFile(cpFile, @from.CPFileName,
								   bytes => to.CPFileData = bytes, s => to.CPFileName = s);

			FileDataHelper.SetFile(deliveryBillFile, @from.DeliveryBillFileName,
								   bytes => to.DeliveryBillFileData = bytes, s => to.DeliveryBillFileName = s);

			FileDataHelper.SetFile(torg12File, @from.Torg12FileName,
								   bytes => to.Torg12FileData = bytes, s => to.Torg12FileName = s);

			FileDataHelper.SetFile(swiftFile, @from.SwiftFileName,
								   bytes => to.SwiftFileData = bytes, s => to.SwiftFileName = s);
		}
	}
}