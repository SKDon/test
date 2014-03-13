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
		private readonly IApplicationFileRepository _applicationFiles;
		private readonly IApplicationRepository _applications;
		private readonly IAwbFileRepository _awbFiles;
		private readonly IClientExcelHelper _excel;
		private readonly ISerializer _serializer;

		public CommonFilesFacade(
			IAwbFileRepository awbFiles, IClientExcelHelper excel, ISerializer serializer,
			IApplicationFileRepository applicationFiles, IApplicationRepository applications)
		{
			_awbFiles = awbFiles;
			_excel = excel;
			_serializer = serializer;
			_applicationFiles = applicationFiles;
			_applications = applications;
		}

		public IReadOnlyDictionary<string, FileHolder[]> GetFiles(
			EventType type, EventDataForEntity eventData, string[] languages)
		{
			switch(type)
			{
				case EventType.SetDateOfCargoReceipt:
				case EventType.SetTransitReference:
				case EventType.ApplicationCreated:
				case EventType.SetSender:
				case EventType.SetForwarder:
				case EventType.SetCarrier:
				case EventType.SetAwb:
					return null;

				case EventType.ApplicationSetState:
					return OnApplicationSetState(eventData, languages);

				case EventType.Calculate:
				case EventType.CalculationCanceled:
					return OnCalculation(eventData, languages);

				case EventType.BalanceDecreased:
				case EventType.BalanceIncreased:
					return OnBalance(eventData, languages);

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
					return OnFileUploaded(eventData, languages);

				case EventType.SetBroker:
					return OnSetBroker(eventData, languages);

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

		private IReadOnlyDictionary<string, FileHolder[]> OnApplicationSetState(
			EventDataForEntity eventData, string[] languages)
		{
			var applicationId = eventData.EntityId;
			var application = _applications.GetExtendedData(applicationId);
			if(application == null)
			{
				throw new InvalidOperationException("Can't find application by id " + applicationId);
			}

			var allFiles = GeAllFiles(applicationId, application.AirWaybillId);

			return languages.ToDictionary(x => x, x => allFiles);
		}

		private IReadOnlyDictionary<string, FileHolder[]> OnBalance(EventDataForEntity eventData, string[] languages)
		{
			var clientId = eventData.EntityId;
			var excels = _excel.GetExcels(clientId, languages);

			return languages.ToDictionary(x => x, x => new[] { excels[x] });
		}

		private IReadOnlyDictionary<string, FileHolder[]> OnCalculation(EventDataForEntity eventData, string[] languages)
		{
			var applicationId = eventData.EntityId;
			var application = _applications.Get(applicationId);
			var excels = _excel.GetExcels(application.ClientId, languages);

			return languages.ToDictionary(x => x, x => new[] { excels[x] });
		}

		private IReadOnlyDictionary<string, FileHolder[]> OnFileUploaded(EventDataForEntity eventData, string[] languages)
		{
			var file = _serializer.Deserialize<FileHolder>(eventData.Data);

			return languages.ToDictionary(x => x, x => new[] { file });
		}

		private IReadOnlyDictionary<string, FileHolder[]> OnSetBroker(EventDataForEntity eventData, string[] languages)
		{
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
		}
	}
}