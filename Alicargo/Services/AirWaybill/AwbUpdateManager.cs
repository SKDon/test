using System.Globalization;
using System.Linq;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.AirWaybill
{
	internal sealed class AwbUpdateManager : IAwbUpdateManager
	{
		private readonly IAwbRepository _awbRepository;
		private readonly IStateConfig _stateConfig;

		public AwbUpdateManager(
			IAwbRepository awbRepository,
			IStateConfig stateConfig)
		{
			_awbRepository = awbRepository;
			_stateConfig = stateConfig;
		}

		public void Update(long id, AwbAdminModel model)
		{
			var data = _awbRepository.Get(id).First();

			AwbMapper.Map(model, data);

			_awbRepository.Update(data, model.GTDFile, model.GTDAdditionalFile,
				model.PackingFile, model.InvoiceFile, model.AWBFile, model.DrawFile);
		}

		public void Update(long id, AwbBrokerModel model)
		{
			var data = _awbRepository.Get(id).First();

			if(data.StateId == _stateConfig.CargoIsCustomsClearedStateId)
			{
				throw new UnexpectedStateException(
					data.StateId,
					"Can't update an AWB while it has the state "
					+ _stateConfig.CargoIsCustomsClearedStateId.ToString(CultureInfo.InvariantCulture));
			}

			AwbMapper.Map(model, data);

			_awbRepository.Update(data, model.GTDFile, model.GTDAdditionalFile,
				model.PackingFile, model.InvoiceFile, null, model.DrawFile);
		}

		public void Update(long id, AwbSenderModel model)
		{
			var data = _awbRepository.Get(id).First();

			AwbMapper.Map(model, data);

			_awbRepository.Update(data, null, null, model.PackingFile, null, model.AWBFile, null);
		}

		public void SetAdditionalCost(long awbId, decimal? additionalCost)
		{
			_awbRepository.SetAdditionalCost(awbId, additionalCost);
		}
	}
}