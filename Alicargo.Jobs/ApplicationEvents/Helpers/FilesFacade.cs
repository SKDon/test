using System;
using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Utilities;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class FilesFacade : IFilesFacade
	{
		private readonly ISerializer _serializer;
		private readonly IAwbRepository _awbs;
		private readonly IApplicationFileRepository _files;

		public FilesFacade(
			ISerializer serializer,
			IAwbRepository awbs,
			IApplicationFileRepository files)
		{
			_serializer = serializer;
			_awbs = awbs;
			_files = files;
		}

		public FileHolder[] GetFiles(long applicationId, long? awbId, EventType type, byte[] data)
		{
			switch (type)
			{
				case EventType.ApplicationCreated:
					return null;

				case EventType.ApplicationSetState:
					return GeAllFiles(applicationId, awbId);

				case EventType.Calculate:
				case EventType.CalculationCanceled:
					return null; // a file is generated in the MessageBuilder because it needs to know a recipient language

				case EventType.CPFileUploaded:
				case EventType.InvoiceFileUploaded:
				case EventType.PackingFileUploaded:
				case EventType.SwiftFileUploaded:
				case EventType.DeliveryBillFileUploaded:
				case EventType.Torg12FileUploaded:
					return new[] { _serializer.Deserialize<FileHolder>(data) };

				case EventType.SetDateOfCargoReceipt:
					return null;

				case EventType.SetTransitReference:
					return null;

				default:
					throw new ArgumentOutOfRangeException("type");
			}
		}

		private FileHolder[] GeAllFiles(long applicationId, long? awbId)
		{
			var files = new List<FileHolder>();

			var types = Enum.GetValues(typeof(ApplicationFileType));
			foreach (ApplicationFileType type in types)
			{
				var names = _files.GetNames(applicationId, type);
				foreach (var name in names)
				{
					var holder = _files.Get(name.Key);

					files.Add(holder);
				}
			}

			if (awbId.HasValue)
			{
				var gtdFile = _awbs.GetGTDFile(awbId.Value);
				var gtdAdditionalFile = _awbs.GTDAdditionalFile(awbId.Value);

				if (gtdFile != null) files.Add(gtdFile);
				if (gtdAdditionalFile != null) files.Add(gtdAdditionalFile);
			}

			return files.ToArray();
		}
	}
}