using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Jobs.Helpers.Abstract;
using Alicargo.Utilities;

namespace Alicargo.Jobs.Helpers
{
	public sealed class CommonFilesFacade : ICommonFilesFacade
	{
		private readonly IAwbFileRepository _awbFiles;
		private readonly IClientExcelHelper _excel;
		private readonly ISerializer _serializer;

		public CommonFilesFacade(IAwbFileRepository awbFiles, IClientExcelHelper excel, ISerializer serializer)
		{
			_awbFiles = awbFiles;
			_excel = excel;
			_serializer = serializer;
		}

		public IReadOnlyDictionary<string, FileHolder[]> GetFiles(
			EventType type, EventDataForEntity eventData, string[] languages)
		{
			switch(type)
			{
				case EventType.BalanceDecreased:
				case EventType.BalanceIncreased:
					var excels = _excel.GetExcels(eventData.EntityId, languages);
					return excels.ToDictionary(x => x.Key, x => new[] { excels[x.Key] });

				case EventType.CPFileUploaded:
				case EventType.InvoiceFileUploaded:
				case EventType.PackingFileUploaded:
				case EventType.SwiftFileUploaded:
				case EventType.DeliveryBillFileUploaded:
				case EventType.Torg12FileUploaded:
				case EventType.GTDFileUploaded:
				case EventType.GTDAdditionalFileUploaded:
				case EventType.AwbPackingFileUploaded:
				case EventType.AwbInvoiceFileUploaded:
				case EventType.AWBFileUploaded:
				case EventType.DrawFileUploaded:
					var file = _serializer.Deserialize<FileHolder>(eventData.Data);
					return languages.ToDictionary(x => x, x => new[] { file });

				case EventType.SetBroker:
					var queue = new Queue<FileHolder>();
					var packing = _awbFiles.GetPackingFile(eventData.EntityId);
					if(packing != null)
					{
						queue.Enqueue(packing);
					}

					var awbFile = _awbFiles.GetAWBFile(eventData.EntityId);
					if(awbFile != null)
					{
						queue.Enqueue(awbFile);
					}

					var files = queue.ToArray();
					return languages.ToDictionary(x => x, x => files);

				default:
					throw new ArgumentOutOfRangeException("type");
			}
		}
	}
}