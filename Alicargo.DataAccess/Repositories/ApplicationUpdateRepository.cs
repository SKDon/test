﻿using System;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Repositories;
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

		// todo: test
		public void SetAirWaybill(long id, long? AirWaybillId)
		{
			Update(id, application => application.AirWaybillId = AirWaybillId);
		}

		// todo: test
		public void SetDateInStock(long id, DateTimeOffset dateTimeOffset)
		{
			Update(id, application => application.DateInStock = dateTimeOffset);
		}

		public void SetTransitReference(long id, string TransitReference)
		{
			Update(id, application => application.TransitReference = TransitReference);
		}

		public void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt)
		{
			Update(id, application => application.DateOfCargoReceipt = dateOfCargoReceipt);
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
