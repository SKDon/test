using System;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories
{
    // todo: 1. bb test
    internal sealed class AwbRepository : BaseRepository, IAwbRepository
    {
        private readonly Expression<Func<AirWaybill, AirWaybillData>> _selector;

        public AwbRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _selector = x => new AirWaybillData
                {
                    ArrivalAirport = x.ArrivalAirport,
                    Bill = x.Bill,
                    BrockerId = x.BrockerId,
                    CreationTimestamp = x.CreationTimestamp,
                    DateOfArrival = x.DateOfArrival,
                    DateOfDeparture = x.DateOfDeparture,
                    DepartureAirport = x.DepartureAirport,
                    GTD = x.GTD,
                    GTDFileName = x.GTDFileName,
                    Id = x.Id,
                    AWBFileName = x.AWBFileName,
                    InvoiceFileName = x.InvoiceFileName,
                    PackingFileName = x.PackingFileName,
                    StateId = x.StateId,
                    StateChangeTimestamp = x.StateChangeTimestamp,
                    GTDAdditionalFileName = x.GTDAdditionalFileName
                };
        }

        public AirWaybillData[] GetAll()
        {
            return Context.AirWaybills.Select(_selector).ToArray();
        }

        public Func<long> Add(AirWaybillData data, byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile,
                              byte[] invoiceFile, byte[] awbFile)
        {
            var entity = new AirWaybill();

            data.CopyTo(entity, gtdFile, gtdAdditionalFile, packingFile, invoiceFile, awbFile);

            Context.AirWaybills.InsertOnSubmit(entity);

            return () => entity.Id;
        }

        public AirWaybillData[] Get(params long[] ids)
        {
            return Context.AirWaybills
                          .Where(x => ids.Contains(x.Id))
                          .Select(_selector)
                          .ToArray();
        }

        public long Count(long? brockerId = null)
        {
            return brockerId.HasValue
                       ? Context.AirWaybills.Where(x => x.BrockerId == brockerId.Value).LongCount()
                       : Context.AirWaybills.LongCount();
        }

        public AirWaybillData[] GetRange(long skip, int take, long? brockerId = null)
        {
            var airWaybills = Context.AirWaybills.AsQueryable();
            if (brockerId.HasValue)
            {
                airWaybills = airWaybills.Where(x => x.BrockerId == brockerId.Value);
            }
            return airWaybills.Skip((int)skip)
                              .Take(take)
                              .Select(_selector)
                              .ToArray();
        }

        public AirWaybillAggregate[] GetAggregate(params long[] ids)
        {
            var data = Context.AirWaybills
                              .Where(x => ids.Contains(x.Id))
                              .Select(x => new
                                  {
                                      x.Id,
                                      x.StateId,
                                      Data = x.Applications.Select(y => new
                                          {
                                              y.Weight,
                                              y.Value,
                                              y.Count
                                          })
                                  })
                              .ToDictionary(x => new { x.Id, x.StateId }, x => x.Data.ToArray());

            return data
                .Select(x => new AirWaybillAggregate
                    {
                        AirWaybillId = x.Key.Id,
                        TotalCount = x.Value.Sum(y => y.Count ?? 0),
                        TotalWeight = x.Value.Sum(y => y.Weight ?? 0),
                        StateId = x.Key.StateId
                    })
                .ToArray();
        }

        public string[] GetClientEmails(long id)
        {
            return Context.AirWaybills
                          .Where(x => x.Id == id)
                          .SelectMany(x => x.Applications)
                          .Select(x => x.Client.Email)
                          .ToArray();
        }

        public void Update(AirWaybillData data, byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile,
                           byte[] invoiceFile, byte[] awbFile)
        {
            var entity = Context.AirWaybills.First(x => x.Id == data.Id);

            data.CopyTo(entity, gtdFile, gtdAdditionalFile, packingFile, invoiceFile, awbFile);
        }

        public void Delete(long id)
        {
            var airWaybill = Context.AirWaybills.First(x => x.Id == id);

            Context.AirWaybills.DeleteOnSubmit(airWaybill);
        }

        public void SetState(long airWaybillId, long stateId)
        {
            var airWaybill = Context.AirWaybills.First(x => x.Id == airWaybillId);
            airWaybill.StateId = stateId;
            airWaybill.StateChangeTimestamp = DateTimeOffset.UtcNow;
        }

        #region Files // todo: 2. test

        public FileHolder GetAWBFile(long id)
        {
            return GetFile(
                x => x.Id == id && x.AWBFileData != null && x.AWBFileName != null,
                x => new FileHolder
                    {
                        FileData = x.AWBFileData.ToArray(),
                        FileName = x.AWBFileName
                    });
        }

        public FileHolder GetGTDFile(long id)
        {
            return GetFile(
                x => x.Id == id && x.GTDFileName != null && x.GTDFileData != null,
                x => new FileHolder
                    {
                        FileName = x.GTDFileName,
                        FileData = x.GTDFileData.ToArray()
                    });
        }

        public FileHolder GetPackingFile(long id)
        {
            return GetFile(
                x => x.Id == id && x.PackingFileData != null && x.PackingFileName != null,
                x => new FileHolder
                    {
                        FileData = x.PackingFileData.ToArray(),
                        FileName = x.PackingFileName
                    });
        }

        public FileHolder GTDAdditionalFile(long id)
        {
            return GetFile(
                x => x.Id == id && x.GTDAdditionalFileName != null && x.GTDAdditionalFileData != null,
                x => new FileHolder
                    {
                        FileName = x.GTDAdditionalFileName,
                        FileData = x.GTDAdditionalFileData.ToArray()
                    });
        }

        public FileHolder GetInvoiceFile(long id)
        {
            return GetFile(
                x => x.Id == id && x.InvoiceFileName != null && x.InvoiceFileData != null,
                x => new FileHolder
                    {
                        FileName = x.InvoiceFileName,
                        FileData = x.InvoiceFileData.ToArray()
                    });
        }

        private FileHolder GetFile(Expression<Func<AirWaybill, bool>> where,
                                   Expression<Func<AirWaybill, FileHolder>> selector)
        {
            return Context.AirWaybills.Where(where).Select(selector).FirstOrDefault();
        }

        #endregion
    }
}