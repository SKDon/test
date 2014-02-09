using Alicargo.Core.Contracts.AirWaybill;
using Alicargo.Core.Contracts.Event;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories.Application;

namespace Alicargo.Core.AirWaybill
{
	public sealed class AwbManager : IAwbManager
	{
		private readonly IApplicationAwbManager _applicationAwbManager;
		private readonly IApplicationRepository _applications;
		private readonly IApplicationEditor _editor;
		private readonly IAwbRepository _awbs;
		private readonly IEventFacade _events;

		public AwbManager(
			IAwbRepository awbs,
			IEventFacade events,
			IApplicationAwbManager applicationAwbManager,
			IApplicationRepository applications,
			IApplicationEditor editor)
		{
			_awbs = awbs;
			_events = events;
			_applicationAwbManager = applicationAwbManager;
			_applications = applications;
			_editor = editor;
		}

		public long Create(long? applicationId, AirWaybillData data, byte[] gtdFile,
			byte[] gtdAdditionalFile, byte[] packingFile, byte[] invoiceFile, byte[] awbFile, byte[] drawFile)
		{
			if(data.GTD != null)
			{
				throw new InvalidLogicException("GTD data should be defined by update");
			}

			var id = _awbs.Add(data, gtdFile, gtdAdditionalFile, packingFile,
				invoiceFile, awbFile, drawFile);

			if(applicationId.HasValue)
			{
				_applicationAwbManager.SetAwb(applicationId.Value, id);
			}

			_events.Add(id, EventType.AwbCreated, EventState.Emailing, data);

			return id;
		}

		public void Delete(long awbId)
		{
			var applicationDatas = _applications.GetByAirWaybill(awbId);

			foreach(var app in applicationDatas)
			{
				_editor.SetAirWaybill(app.Id, null);
			}

			_awbs.Delete(awbId);
		}
	}
}