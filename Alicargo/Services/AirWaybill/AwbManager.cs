using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.AirWaybill
{
    internal sealed class AwbManager : IAwbManager
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IApplicationUpdateRepository _applicationUpdater;
        private readonly IAwbRepository _awbRepository;
        private readonly IApplicationAwbManager _applicationAwbManager;
        private readonly IStateConfig _stateConfig;
        private readonly IUnitOfWork _unitOfWork;

        public AwbManager(
            IAwbRepository awbRepository,
            IApplicationAwbManager applicationAwbManager,
            IApplicationRepository applicationRepository,
            IUnitOfWork unitOfWork,
            IStateConfig stateConfig,
            IApplicationUpdateRepository applicationUpdater)
        {
            _awbRepository = awbRepository;
            _applicationAwbManager = applicationAwbManager;
            _applicationRepository = applicationRepository;
            _unitOfWork = unitOfWork;
            _stateConfig = stateConfig;
            _applicationUpdater = applicationUpdater;
        }

        public long Create(long applicationId, AirWaybillEditModel model)
        {
            if (model.GTD != null || model.GTDFileName != null || model.GTDAdditionalFileName != null)
            {
                // todo: 1. Test
                throw new InvalidLogicException("GTD data should be defined by update");
            }

            using (var ts = _unitOfWork.StartTransaction())
            {
                // todo: 2. Test
                var data = new AirWaybillData
                    {
                        StateId = _stateConfig.CargoIsFlewStateId,
                        CreationTimestamp = DateTimeOffset.UtcNow,
                        StateChangeTimestamp = DateTimeOffset.UtcNow,
                        Id = 0,
                        PackingFileName = model.PackingFileName,
                        InvoiceFileName = model.InvoiceFileName,
                        AWBFileName = model.AWBFileName,
                        ArrivalAirport = model.ArrivalAirport,
                        Bill = model.Bill,
                        DepartureAirport = model.DepartureAirport,
                        BrockerId = model.BrockerId,
                        DateOfArrival = DateTimeOffset.Parse(model.DateOfArrivalLocalString),
                        DateOfDeparture = DateTimeOffset.Parse(model.DateOfDepartureLocalString),
                        GTD = null,
                        GTDAdditionalFileName = null,
                        GTDFileName = null
                    };

                var id = _awbRepository.Add(data, model.GTDFile, model.GTDAdditionalFile, model.PackingFile,
                                            model.InvoiceFile, model.AWBFile);

                _unitOfWork.SaveChanges();

                _applicationAwbManager.SetAwb(applicationId, id());

                ts.Complete();

                return id();
            }
        }

        public void Delete(long id)
        {
            using (var ts = _unitOfWork.StartTransaction())
            {
                var applicationDatas = _applicationRepository.GetByAirWaybill(id);

                // todo: 1. test sets
                foreach (var app in applicationDatas)
                {
                    _applicationUpdater.SetAirWaybill(app.Id, null);
                }

                _awbRepository.Delete(id);

                _unitOfWork.SaveChanges();

                ts.Complete();
            }
        }
    }
}