using System;
using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Utilities;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class FilesFacade : IFilesFacade
	{
		private readonly ISerializer _serializer;
		private readonly IAwbFileRepository _awbFiles;
		private readonly IApplicationFileRepository _applicationFiles;

		public FilesFacade(
			ISerializer serializer,
			IAwbFileRepository awbFiles,
			IApplicationFileRepository applicationFiles)
		{
			_serializer = serializer;
			_awbFiles = awbFiles;
			_applicationFiles = applicationFiles;
		}

		public FileHolder[] GetFiles(long applicationId, long? awbId, EventType type, byte[] data)
		{
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
					return GeAllFiles(applicationId, awbId);

				case EventType.Calculate:
				case EventType.CalculationCanceled:
					return null; // calculation file is generated in the MessageBuilder because it needs to know a recipient language

				case EventType.CPFileUploaded:
				case EventType.InvoiceFileUploaded:
				case EventType.PackingFileUploaded:
				case EventType.SwiftFileUploaded:
				case EventType.DeliveryBillFileUploaded:
				case EventType.Torg12FileUploaded:
					return new[] { _serializer.Deserialize<FileHolder>(data) };

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