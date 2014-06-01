using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.AirWaybill;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Awb;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Services.Abstract;
using Alicargo.Services.Excel.Rows;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Excel
{
	internal sealed class ApplicationExcelRowSource : IApplicationExcelRowSource
	{
		private readonly IApplicationRepository _applications;
		private readonly IAwbRepository _awbs;
		private readonly IApplicationListItemMapper _itemMapper;
		private readonly IStateConfig _stateConfig;
		private readonly IStateFilter _stateFilter;

		public ApplicationExcelRowSource(
			IApplicationRepository applications,
			IAwbRepository awbs,
			IStateConfig stateConfig,
			IStateFilter stateFilter,
			IApplicationListItemMapper itemMapper)
		{
			_applications = applications;
			_awbs = awbs;
			_stateConfig = stateConfig;
			_stateFilter = stateFilter;
			_itemMapper = itemMapper;
		}

		public AdminApplicationExcelRow[] GetAdminApplicationExcelRow(string language)
		{
			var items = GetApplicationListItems(null, null, null, language);

			var awbs = _awbs.Get(items.Select(x => x.AirWaybillId ?? 0).ToArray());

			return items.Select(x => new AdminApplicationExcelRow(x, GetAirWaybillDisplay(awbs, x))).ToArray();
		}

		public ForwarderApplicationExcelRow[] GetForwarderApplicationExcelRow(long forwarderId, string language)
		{
			var items = GetApplicationListItems(null, null, forwarderId, language);

			var awbs = _awbs.Get(items.Select(x => x.AirWaybillId ?? 0).ToArray());

			return items.Select(x => new ForwarderApplicationExcelRow(x, GetAirWaybillDisplay(awbs, x))).ToArray();
		}

		public SenderApplicationExcelRow[] GetSenderApplicationExcelRow(long senderId, string language)
		{
			var items = GetApplicationListItems(senderId, null, null, language);

			var awbs = _awbs.Get(items.Select(x => x.AirWaybillId ?? 0).ToArray());

			return items.Select(x => new SenderApplicationExcelRow(x, GetAirWaybillDisplay(awbs, x))).ToArray();
		}

		public CarrierApplicationExcelRow[] GetCarrierApplicationExcelRow(long carrierId, string language)
		{
			var items = GetApplicationListItems(null, carrierId, null, language);

			var awbs = _awbs.Get(items.Select(x => x.AirWaybillId ?? 0).ToArray());

			return items.Select(x => new CarrierApplicationExcelRow(x, GetAirWaybillDisplay(awbs, x))).ToArray();
		}

		private static string GetAirWaybillDisplay(IEnumerable<AirWaybillData> awbs, ApplicationListItem application)
		{
			return awbs.Where(a => a.Id == application.AirWaybillId)
				.Select(AwbHelper.GetAirWaybillDisplay)
				.FirstOrDefault();
		}

		private ApplicationListItem[] GetApplicationListItems(long? senderId, long? carrierId, long? forwarderId,
			string language)
		{
			var stateIds = _stateFilter.GetStateVisibility();

			var data = _applications.List(stateIds, new[]
			{
				new Order { Desc = true, OrderType = OrderType.AirWaybill },
				new Order { Desc = false, OrderType = OrderType.Client },
				new Order { Desc = true, OrderType = OrderType.Id }
			}, null, 0, null, senderId, carrierId, forwarderId, _stateConfig.CargoReceivedStateId,
				_stateConfig.CargoReceivedDaysToShow);

			var withoutAwb = data.Where(x => !x.AirWaybillId.HasValue).OrderByDescending(x => x.Id);

			var withAwb = data.Where(x => x.AirWaybillId.HasValue);

			data = withoutAwb.Concat(withAwb).ToArray();

			return _itemMapper.Map(data, language);
		}
	}
}