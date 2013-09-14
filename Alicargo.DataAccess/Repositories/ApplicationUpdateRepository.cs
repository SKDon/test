﻿using System;
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

			application.CopyTo(swiftFile, invoiceFile, cpFile, deliveryBillFile, torg12File, packingFile, entity);

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

		public void SetWithdrawCost(long id, decimal? withdrawCost)
		{
			Update(id, application => application.WithdrawCost = withdrawCost);
		}

		public void SetFactureCost(long id, decimal? factureCost)
		{
			Update(id, application => application.FactureCost = factureCost);
		}

		public void SetScotchCost(long id, decimal? scotchCost)
		{
			Update(id, application => application.ScotchCost = scotchCost);
		}

		public void Update(ApplicationData application, byte[] swiftFile, byte[] invoiceFile,
			byte[] cpFile, byte[] deliveryBillFile, byte[] torg12File, byte[] packingFile)
		{
			Update(application.Id, entity =>
				application.CopyTo(swiftFile, invoiceFile, cpFile, deliveryBillFile, torg12File, packingFile, entity));
		}

		void Update(long id, Action<Application> action)
		{
			var application = Context.Applications.First(x => x.Id == id);
			action(application);
		}
	}
}
