using Alicargo.Core.Contracts.AirWaybill;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;

namespace Alicargo.Core.AirWaybill
{
	public sealed class AwbManager : IAwbManager
	{
		private readonly IApplicationAwbManager _applicationAwbManager;
		private readonly IApplicationRepository _applicationRepository;
		private readonly IApplicationUpdateRepository _applicationUpdater;
		private readonly IAwbRepository _awbRepository;
		private readonly IUnitOfWork _unitOfWork;

		public AwbManager(
			IAwbRepository awbRepository,
			IApplicationAwbManager applicationAwbManager,
			IApplicationRepository applicationRepository,
			IUnitOfWork unitOfWork,
			IApplicationUpdateRepository applicationUpdater)
		{
			_awbRepository = awbRepository;
			_applicationAwbManager = applicationAwbManager;
			_applicationRepository = applicationRepository;
			_unitOfWork = unitOfWork;
			_applicationUpdater = applicationUpdater;
		}

		public long Create(long? applicationId, AirWaybillData data, byte[] gtdFile,
			byte[] gtdAdditionalFile, byte[] packingFile, byte[] invoiceFile, byte[] awbFile)
		{
			if(data.GTD != null)
			{
				throw new InvalidLogicException("GTD data should be defined by update");
			}

			var id = _awbRepository.Add(data, gtdFile, gtdAdditionalFile, packingFile,
				invoiceFile, awbFile);

			_unitOfWork.SaveChanges();

			if(applicationId.HasValue)
			{
				_applicationAwbManager.SetAwb(applicationId.Value, id());
			}

			return id();
		}

		public void Delete(long awbId)
		{
			var applicationDatas = _applicationRepository.GetByAirWaybill(awbId);

			foreach(var app in applicationDatas)
			{
				_applicationUpdater.SetAirWaybill(app.Id, null);
			}

			_awbRepository.Delete(awbId);

			_unitOfWork.SaveChanges();
		}
	}
}