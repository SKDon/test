using System.Globalization;
using System.Linq;
using Alicargo.Core.Contracts.Exceptions;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.AirWaybill
{
	internal sealed class AwbUpdateManager : IAwbUpdateManager
	{
		private readonly IAwbRepository _awbs;
		private readonly IStateConfig _config;

		public AwbUpdateManager(
			IAwbRepository awbs,
			IStateConfig config)
		{
			_awbs = awbs;
			_config = config;
		}

		public void Update(long id, AwbAdminModel model)
		{
			var data = _awbs.Get(id).First();

			AwbMapper.Map(model, data);

			_awbs.Update(data, model.GTDFile, model.GTDAdditionalFile,
				model.PackingFile, model.InvoiceFile, model.AWBFile, model.DrawFile);
		}

		public void Update(long id, AwbBrokerModel model)
		{
			var data = _awbs.Get(id).First();

			if(data.StateId == _config.CargoIsCustomsClearedStateId)
			{
				throw new UnexpectedStateException(
					data.StateId,
					"Can't update an AWB while it has the state "
					+ _config.CargoIsCustomsClearedStateId.ToString(CultureInfo.InvariantCulture));
			}

			AwbMapper.Map(model, data);

			_awbs.Update(data, model.GTDFile, model.GTDAdditionalFile,
				model.PackingFile, model.InvoiceFile, null, model.DrawFile);
		}

		public void Update(long id, AwbSenderModel model)
		{
			var data = _awbs.Get(id).First();

			AwbMapper.Map(model, data);

			_awbs.Update(data, null, null, model.PackingFile, null, model.AWBFile, null);
		}

		public void SetAdditionalCost(long awbId, decimal? additionalCost)
		{
			_awbs.SetAdditionalCost(awbId, additionalCost);
		}
	}
}