using System;
using System.Collections.Generic;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Jobs.ApplicationEvents.Entities;

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
					return new[] { _serializer.Deserialize<ApplicationFileUploadedEventData>(data).File };

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
			var files = new List<FileHolder>(8);

			var invoiceFile = _files.GetInvoiceFile(applicationId);
			var deliveryBillFile = _files.GetDeliveryBillFile(applicationId);
			var cpFile = _files.GetCPFile(applicationId);
			var packingFile = _files.GetPackingFile(applicationId);
			var swiftFile = _files.GetSwiftFile(applicationId);
			var torg12File = _files.GetTorg12File(applicationId);

			if (awbId.HasValue)
			{
				var gtdFile = _awbs.GetGTDFile(awbId.Value);
				var gtdAdditionalFile = _awbs.GTDAdditionalFile(awbId.Value);

				if (gtdFile != null) files.Add(gtdFile);
				if (gtdAdditionalFile != null) files.Add(gtdAdditionalFile);
			}

			if (invoiceFile != null) files.Add(invoiceFile);
			if (deliveryBillFile != null) files.Add(deliveryBillFile);
			if (cpFile != null) files.Add(cpFile);
			if (packingFile != null) files.Add(packingFile);
			if (swiftFile != null) files.Add(swiftFile);
			if (torg12File != null) files.Add(torg12File);

			return files.ToArray();
		}
	}
}