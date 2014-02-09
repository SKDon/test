using System;
using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Jobs.Application.Abstract;
using Alicargo.Utilities;

namespace Alicargo.Jobs.Application.Helpers
{
	public sealed class ApplicationEventFilesFacade : IFilesFacade
	{
		private readonly ISerializer _serializer;
		private readonly IAwbFileRepository _awbFiles;
		private readonly IApplicationFileRepository _applicationFiles;
		private readonly IApplicationRepository _applications;

		public ApplicationEventFilesFacade(
			ISerializer serializer,
			IAwbFileRepository awbFiles,
			IApplicationFileRepository applicationFiles, 
			IApplicationRepository applications)
		{
			_serializer = serializer;
			_awbFiles = awbFiles;
			_applicationFiles = applicationFiles;
			_applications = applications;
		}

		public FileHolder[] GetFiles(EventType type, EventDataForEntity data)
		{
			var applicationId = data.EntityId;

			var application = _applications.GetExtendedData(data.EntityId);
			if(application == null)
			{
				throw new InvalidOperationException("Can't find application by id " + data.EntityId);
			}

			switch(type)
			{
				case EventType.SetDateOfCargoReceipt:
				case EventType.SetTransitReference:
				case EventType.ApplicationCreated:
				case EventType.SetSender:
				case EventType.SetForwarder:
				case EventType.SetCarrier:
					return null;

				case EventType.ApplicationSetState:
					return GeAllFiles(applicationId, application.AirWaybillId);

				case EventType.Calculate:
				case EventType.CalculationCanceled:
					return null; // calculation file is generated in the MessageBuilder because it needs to know a recipient language

				case EventType.CPFileUploaded:
				case EventType.InvoiceFileUploaded:
				case EventType.PackingFileUploaded:
				case EventType.SwiftFileUploaded:
				case EventType.DeliveryBillFileUploaded:
				case EventType.Torg12FileUploaded:
					return new[] { _serializer.Deserialize<FileHolder>(data.Data) };

				default:
					throw new ArgumentOutOfRangeException("type");
			}
		}

		private FileHolder[] GeAllFiles(long applicationId, long? awbId)
		{
			var files = new List<FileHolder>();

			var types = Enum.GetValues(typeof(ApplicationFileType));
			foreach(ApplicationFileType type in types)
			{
				var names = _applicationFiles.GetNames(applicationId, type);
				foreach(var name in names)
				{
					var holder = _applicationFiles.Get(name.Key);

					files.Add(holder);
				}
			}

			if(awbId.HasValue)
			{
				var gtdFile = _awbFiles.GetGTDFile(awbId.Value);
				var gtdAdditionalFile = _awbFiles.GTDAdditionalFile(awbId.Value);

				if(gtdFile != null) files.Add(gtdFile);
				if(gtdAdditionalFile != null) files.Add(gtdAdditionalFile);
			}

			return files.ToArray();
		}
	}
}