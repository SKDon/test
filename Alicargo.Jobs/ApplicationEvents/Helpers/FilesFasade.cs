using System;
using System.Collections.Generic;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.ApplicationEvents.Abstract;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class FilesFasade : IFilesFasade
	{
		private readonly ISerializer _serializer;
		private readonly IAwbRepository _awbs;
		private readonly IApplicationFileRepository _files;

		public FilesFasade(
			ISerializer serializer, 
			IAwbRepository awbs, 
			IApplicationFileRepository files)
		{
			_serializer = serializer;
			_awbs = awbs;
			_files = files;
		}

		public FileHolder[] GetFiles(long applicationId, long? awbId, ApplicationEventType type, byte[] data)
		{
			switch (type)
			{
				case ApplicationEventType.Created:
					return null;

				case ApplicationEventType.SetState:
					return GeAllFiles(applicationId, awbId);

				case ApplicationEventType.CPFileUploaded:
				case ApplicationEventType.InvoiceFileUploaded:
				case ApplicationEventType.PackingFileUploaded:
				case ApplicationEventType.SwiftFileUploaded:
				case ApplicationEventType.DeliveryBillFileUploaded:
				case ApplicationEventType.Torg12FileUploaded:
					return new[] { _serializer.Deserialize<FileHolder>(data) };

				case ApplicationEventType.SetDateOfCargoReceipt:
					return null;

				case ApplicationEventType.SetTransitReference:
					return null;

				default:
					throw new ArgumentOutOfRangeException("type");
			}
		}

		private FileHolder[] GeAllFiles(long applicationId, long? awbId)
		{
			var files = new List<FileHolder>();

			var types = Enum.GetValues(typeof (ApplicationFileType));
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