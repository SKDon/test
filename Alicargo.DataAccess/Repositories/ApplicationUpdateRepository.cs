using System;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class ApplicationUpdateRepository : IApplicationUpdateRepository
	{
		private readonly AlicargoDataContext _context;

		public ApplicationUpdateRepository(IUnitOfWork unitOfWork)
		{
			_context = (AlicargoDataContext) unitOfWork.Context;
		}

		public Func<long> Add(ApplicationData application)
		{
			var entity = new Application();

			CopyTo(application, entity);

			_context.Applications.InsertOnSubmit(entity);

			return () => entity.Id;
		}

		public void Delete(long id)
		{
			var application = _context.Applications.First(x => x.Id == id);
			_context.Applications.DeleteOnSubmit(application);
		}

		public void SetState(long id, long stateId)
		{
			Update(id, application =>
			{
				application.StateId = stateId;
				application.StateChangeTimestamp = DateTimeOffset.UtcNow;
			});
		}

		public void SetAirWaybill(long id, long? airWaybillId)
		{
			Update(id, application => application.AirWaybillId = airWaybillId);
		}

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

		public void SetPickupCostEdited(long id, decimal? pickupCost)
		{
			Update(id, application => application.PickupCostEdited = pickupCost);
		}

		public void SetFactureCostEdited(long id, decimal? factureCost)
		{
			Update(id, application => application.FactureCostEdited = factureCost);
		}

		public void SetTransitCostEdited(long id, decimal? transitCost)
		{
			Update(id, application => application.TransitCostEdited = transitCost);
		}

		public void SetScotchCostEdited(long id, decimal? scotchCost)
		{
			Update(id, application => application.ScotchCostEdited = scotchCost);
		}

		public void SetSenderRate(long id, decimal? senderRate)
		{
			Update(id, application => application.SenderRate = senderRate);
		}

		public void SetClass(long id, int? classId)
		{
			Update(id, application => application.ClassId = classId);
		}

		public void Update(ApplicationData application)
		{
			Update(application.Id, entity =>
				CopyTo(application, entity));
		}

		private void Update(long id, Action<Application> action)
		{
			var application = _context.Applications.First(x => x.Id == id);
			action(application);
		}

		private static void CopyTo(ApplicationData from, Application to)
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
			to.Weight = @from.Weight;
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
			to.ClassId = from.ClassId;
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
			to.TariffPerKg = @from.TariffPerKg;
			to.TransitCost = @from.TransitCost;
			to.PickupCost = @from.PickupCost;
			to.FactureCostEdited = @from.FactureCostEdited;
			to.TransitCostEdited = @from.TransitCostEdited;
			to.PickupCostEdited = @from.PickupCostEdited;
			to.ScotchCostEdited = @from.ScotchCostEdited;
			to.SenderRate = @from.SenderRate;			
		}
	}
}