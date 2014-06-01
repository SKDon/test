using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.Exceptions;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Awb;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.Utilities.Localization;
using Alicargo.ViewModels.AirWaybill;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.AirWaybill
{
	internal sealed class AwbPresenter : IAwbPresenter
	{
		private readonly IAwbRepository _awbs;
		private readonly IAwbFileRepository _files;
		private readonly IBrokerRepository _brokers;
		private readonly IStateRepository _states;

		public AwbPresenter(
			IAwbRepository awbs,
			IAwbFileRepository files,
			IBrokerRepository brokers,
			IStateRepository states)
		{
			_awbs = awbs;
			_files = files;
			_brokers = brokers;
			_states = states;
		}

		public ListCollection<AirWaybillListItem> List(int take, int skip, long? brokerId, string language)
		{
			var data = _awbs.GetRange(take, skip, brokerId);
			var ids = data.Select(x => x.Id).ToArray();

			var aggregates = _awbs.GetAggregate(ids).ToDictionary(x => x.AirWaybillId, x => x);

			var states = _states.Get(language);
			var currentCulture = CultureProvider.GetCultureInfo();
			var awbFiles = GetNames(ids, AwbFileType.AWB);
			var packingFiles = GetNames(ids, AwbFileType.Packing);
			var drawFiles = GetNames(ids, AwbFileType.Draw);
			var gtdFiles = GetNames(ids, AwbFileType.GTD);
			var gtdAddFiles = GetNames(ids, AwbFileType.GTDAdditional);
			var invoiceFiles = GetNames(ids, AwbFileType.Invoice);

			var items = data.Select(x => new AirWaybillListItem
			{
				Id = x.Id,
				PackingFileName = GetName(packingFiles, x.Id),
				InvoiceFileName = GetName(invoiceFiles, x.Id),
				State = new ApplicationStateModel
				{
					StateName = states[x.StateId].LocalizedName,
					StateId = x.StateId
				},
				AWBFileName = GetName(awbFiles, x.Id),
				ArrivalAirport = x.ArrivalAirport,
				Bill = x.Bill,
				CreationTimestampLocalString = LocalizationHelper.GetDate(x.CreationTimestamp, currentCulture),
				DateOfArrivalLocalString = LocalizationHelper.GetDate(x.DateOfArrival, currentCulture),
				DateOfDepartureLocalString = LocalizationHelper.GetDate(x.DateOfDeparture, currentCulture),
				StateChangeTimestampLocalString = LocalizationHelper.GetDate(x.StateChangeTimestamp, currentCulture),
				DepartureAirport = x.DepartureAirport,
				GTD = x.GTD,
				GTDAdditionalFileName = GetName(gtdAddFiles, x.Id),
				GTDFileName = GetName(gtdFiles, x.Id),
				DrawFileName = GetName(drawFiles, x.Id),
				TotalCount = aggregates[x.Id].TotalCount,
				TotalWeight = aggregates[x.Id].TotalWeight,
				AdditionalCost = x.AdditionalCost,
				TotalCostOfSenderForWeight = x.TotalCostOfSenderForWeight,
				BrokerCost = x.BrokerCost,
				CustomCost = x.CustomCost,
				FlightCost = x.FlightCost,
			}).ToArray();

			var total = _awbs.Count(brokerId);

			return new ListCollection<AirWaybillListItem> { Data = items, Total = total };
		}

		private Dictionary<long, string> GetNames(long[] ids, AwbFileType type)
		{
			return _files.GetInfo(ids, type).ToDictionary(x => x.Key, x => string.Join(", ", x.Value.Select(y => y.Name)));
		}

		private static string GetName(Dictionary<long, string> names, long awbId)
		{
			return names != null && names.ContainsKey(awbId) ? names[awbId] : null;
		}

		public AwbAdminModel Get(long id)
		{
			var data = _awbs.Get(id).FirstOrDefault();

			if(data == null) throw new EntityNotFoundException("Refarence: " + id);

			return AwbMapper.GetAdminModel(data);
		}

		public AwbSenderModel GetSenderAwbModel(long id)
		{
			var data = _awbs.Get(id).First();

			return AwbMapper.GetSenderModel(data);
		}

		public AirWaybillData GetData(long id)
		{
			return _awbs.Get(id).First();
		}

		public AirWaybillAggregate GetAggregate(long id)
		{
			return _awbs.GetAggregate(new[] { id }).First();
		}

		public BrokerData GetBroker(long brokerId)
		{
			return _brokers.Get(brokerId);
		}
	}
}