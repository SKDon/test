using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services.Abstract;
using Alicargo.Services.Abstract;
using Alicargo.Services.Excel.Rows;
using Alicargo.ViewModels.Application;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Services.Excel
{
	internal sealed class ApplicationExcelRowSource : IApplicationExcelRowSource
	{
		private readonly IApplicationRepository _applications;
		private readonly IAwbRepository _awbs;
		private readonly IStateConfig _stateConfig;
		private readonly IApplicationListItemMapper _itemMapper;
		private readonly IStateService _stateService;

		public ApplicationExcelRowSource(
			IApplicationRepository applications,
			IAwbRepository awbs,
			IStateConfig stateConfig,
			IStateService stateService,
			IApplicationListItemMapper itemMapper)
		{
			_applications = applications;
			_awbs = awbs;
			_stateConfig = stateConfig;
			_stateService = stateService;
			_itemMapper = itemMapper;
		}

		public AdminApplicationExcelRow[] GetAdminApplicationExcelRow()
		{
			var items = GetApplicationListItems(false);

			var awbs = _awbs.Get(items.Select(x => x.AirWaybillId ?? 0).ToArray());

			return items.Select(x => new AdminApplicationExcelRow(x, GetAirWaybillDisplay(awbs, x))).ToArray();
		}

		public ForwarderApplicationExcelRow[] GetForwarderApplicationExcelRow()
		{
			var items = GetApplicationListItems(true);

			var awbs = _awbs.Get(items.Select(x => x.AirWaybillId ?? 0).ToArray());

			return items.Select(x => new ForwarderApplicationExcelRow(x, GetAirWaybillDisplay(awbs, x))).ToArray();
		}

		public SenderApplicationExcelRow[] GetSenderApplicationExcelRow()
		{
			var items = GetApplicationListItems(false);

			var awbs = _awbs.Get(items.Select(x => x.AirWaybillId ?? 0).ToArray());

			return items.Select(x => new SenderApplicationExcelRow(x, GetAirWaybillDisplay(awbs, x))).ToArray();
		}

		private static string GetAirWaybillDisplay(IEnumerable<AirWaybillData> awbs, ApplicationListItem application)
		{
			return awbs.Where(a => a.Id == application.AirWaybillId)
					   .Select(data => HttpUtility.HtmlDecode(AwbHelper.GetAirWaybillDisplay(data)))
					   .FirstOrDefault();
		}

		private ApplicationListItem[] GetApplicationListItems(bool isForwarder)
		{
			var stateIds = _stateService.GetStateVisibility();

			var cargoReceivedStateId = isForwarder
				? _stateConfig.CargoReceivedStateId
				: (long?)null;

			var data = _applications.List(stateIds, new[]
			{
			    new Order {Desc = true, OrderType = OrderType.AirWaybill},
			    new Order {Desc = false, OrderType = OrderType.ClientNic},
			    new Order {Desc = true, OrderType = OrderType.Id}
			}, cargoReceivedStateId: cargoReceivedStateId, cargoReceivedDaysToShow: _stateConfig.CargoReceivedDaysToShow);

			var withoutAwb = data.Where(x => !x.AirWaybillId.HasValue).OrderByDescending(x => x.Id);

			var withAwb = data.Where(x => x.AirWaybillId.HasValue);

			data = withoutAwb.Concat(withAwb).ToArray();

			return _itemMapper.Map(data);
		}
	}
}