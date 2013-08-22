using System;
using System.Globalization;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;
using Microsoft.Ajax.Utilities;

namespace Alicargo.Services.AirWaybill
{
    internal sealed class AwbUpdateManager : IAwbUpdateManager
    {
        private readonly IApplicationManager _applicationManager;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IAwbRepository _awbRepository;
        private readonly IStateConfig _stateConfig;
        private readonly IUnitOfWork _unitOfWork;

        public AwbUpdateManager(
            IAwbRepository awbRepository,
            IApplicationRepository applicationRepository,
            IUnitOfWork unitOfWork,
            IApplicationManager applicationManager,
            IStateConfig stateConfig)
        {
            _awbRepository = awbRepository;
            _applicationRepository = applicationRepository;
            _unitOfWork = unitOfWork;
            _applicationManager = applicationManager;
            _stateConfig = stateConfig;
        }

        public void Update(long id, AirWaybillEditModel model)
        {
            using (var ts = _unitOfWork.StartTransaction())
            {
                var data = _awbRepository.Get(id).First();

                ProcessGtd(data, model.GTD);

                // todo: 2. Test
                data.PackingFileName = model.PackingFileName;
                data.InvoiceFileName = model.InvoiceFileName;
                data.AWBFileName = model.AWBFileName;
                data.ArrivalAirport = model.ArrivalAirport;
                data.Bill = model.Bill;
                data.DepartureAirport = model.DepartureAirport;
                data.GTD = model.GTD;
                data.GTDFileName = model.GTDFileName;
                data.BrockerId = model.BrockerId;
                data.GTDAdditionalFileName = model.GTDAdditionalFileName;
                data.DateOfArrival = DateTimeOffset.Parse(model.DateOfArrivalLocalString);
                data.DateOfDeparture = DateTimeOffset.Parse(model.DateOfDepartureLocalString);

                // todo: 1. test
                // todo: 2. use update file methods
                _awbRepository.Update(data, model.GTDFile, model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile,
                                      model.AWBFile);

                _unitOfWork.SaveChanges();

                ts.Complete();
            }
        }

        public void Update(long id, BrockerAWBModel model)
        {
            using (var ts = _unitOfWork.StartTransaction())
            {
                var data = _awbRepository.Get(id).First();

                // todo: 1. test
                if (data.StateId == _stateConfig.CargoIsCustomsClearedStateId)
                {
                    throw new UnexpectedStateException(
                        data.StateId,
                        "Can't update an AWB while it has the state "
                        + _stateConfig.CargoIsCustomsClearedStateId.ToString(CultureInfo.InvariantCulture));
                }

                ProcessGtd(data, model.GTD);

                // todo: 2. Test
                data.PackingFileName = model.PackingFileName;
                data.InvoiceFileName = model.InvoiceFileName;
                data.GTD = model.GTD;
                data.GTDFileName = model.GTDFileName;
                data.GTDAdditionalFileName = model.GTDAdditionalFileName;

                // todo: 1. test
                // todo: 2. use update file methods
                _awbRepository.Update(data, model.GTDFile, model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile,
                                      null);

                _unitOfWork.SaveChanges();

                ts.Complete();
            }
        }

        public void SetState(long airWaybillId, long stateId)
        {
            var data = _awbRepository.Get(airWaybillId).First();

            SetStateImpl(airWaybillId, stateId, data.StateId);
        }

        private void ProcessGtd(AirWaybillData data, string newGtd)
        {
            // todo: 1. test
            if (!IsReadyForCargoAtCustomsStateId(data.GTD, newGtd)) return;
            if (data.StateId == _stateConfig.CargoIsCustomsClearedStateId) return;

            SetStateImpl(data.Id, _stateConfig.CargoAtCustomsStateId, data.StateId);
            data.StateId = _stateConfig.CargoAtCustomsStateId;
        }

        private static bool IsReadyForCargoAtCustomsStateId(string oldGtd, string newGtd)
        {
            return oldGtd.IsNullOrWhiteSpace() && !newGtd.IsNullOrWhiteSpace();
        }

        private void SetStateImpl(long airWaybillId, long newStateId, long currentStateId)
        {
            var applications = _applicationRepository.GetByAirWaybill(airWaybillId);

            using (var ts = _unitOfWork.StartTransaction())
            {
                _awbRepository.SetState(airWaybillId, newStateId);

                // todo: 1. test sets
                foreach (var application in applications)
                {
                    if (currentStateId == application.StateId)
                    {
                        _applicationManager.SetState(application.Id, newStateId);
                    }
                }

                _unitOfWork.SaveChanges();

                ts.Complete();
            }
        }
    }
}