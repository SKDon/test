using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alicargo.Core.AirWaybill;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories;
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

		public BaseApplicationExcelRow[] GetAdminApplicationExcelRow(string language)
		{
			var items = GetApplicationListItems(null, language);

			var awbs = _awbs.Get(items.Select(x => x.AirWaybillId ?? 0).ToArray());

			return items.Select(x => (BaseApplicationExcelRow)new AdminApplicationExcelRow(x, GetAirWaybillDisplay(awbs, x))).ToArray();
		}

		public BaseApplicationExcelRow[] GetForwarderApplicationExcelRow(long forwarderId, string language)
		{
			var items = GetApplicationListItems(forwarderId, language);

			var awbs = _awbs.Get(items.Select(x => x.AirWaybillId ?? 0).ToArray());

			return items.Select(x => (BaseApplicationExcelRow)new ForwarderApplicationExcelRow(x, GetAirWaybillDisplay(awbs, x))).ToArray();
		}

		public BaseApplicationExcelRow[] GetSenderApplicationExcelRow(long senderId, string language)
		{
			var items = GetApplicationListItems(null, language);

			var awbs = _awbs.Get(items.Select(x => x.AirWaybillId ?? 0).ToArray());

			return items.Select(x => (BaseApplicationExcelRow)new SenderApplicationExcelRow(x, GetAirWaybillDisplay(awbs, x))).ToArray();
		}

		public BaseApplicationExcelRow[] GetCarrierApplicationExcelRow(long carrierId, string language)
		{
			throw new System.NotImplementedException();
		}

		private static string GetAirWaybillDisplay(IEnumerable<AirWaybillData> awbs, ApplicationListItem application)
		{
			return awbs.Where(a => a.Id == application.AirWaybillId)
				.Select(data => HttpUtility.HtmlDecode(AwbHelper.GetAirWaybillDisplay(data)))
				.FirstOrDefault();
		}

		private ApplicationListItem[] GetApplicationListItems(long? forwarderId, string language)
		{
			var stateIds = _stateFilter.GetStateVisibility();

			var data = _applications.List(stateIds, new[]
			{
				new Order { Desc = true, OrderType = OrderType.AirWaybill },
				new Order { Desc = false, OrderType = OrderType.ClientNic },
				new Order { Desc = true, OrderType = OrderType.Id }
			}, cargoReceivedStateId: _stateConfig.CargoReceivedStateId,
				cargoReceivedDaysToShow: _stateConfig.CargoReceivedDaysToShow,
				forwarderId: forwarderId);

			var withoutAwb = data.Where(x => !x.AirWaybillId.HasValue).OrderByDescending(x => x.Id);

			var withAwb = data.Where(x => x.AirWaybillId.HasValue);

			data = withoutAwb.Concat(withAwb).ToArray();

			return _itemMapper.Map(data, language);
		}
	}
}