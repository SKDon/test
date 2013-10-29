using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
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
		private readonly IApplicationListItemMapper _itemMapper;
		private readonly IStateService _stateService;

		public ApplicationExcelRowSource(
			IApplicationRepository applications,
			IAwbRepository awbs,
			IStateService stateService,
			IApplicationListItemMapper itemMapper)
		{
			_applications = applications;
			_awbs = awbs;
			_stateService = stateService;
			_itemMapper = itemMapper;
		}

		public AdminApplicationExcelRow[] GetAdminApplicationExcelRow()
		{
			var items = GetApplicationListItems();

			var awbs = _awbs.Get(items.Select(x => x.AirWaybillId ?? 0).ToArray());

			return items.Select(x => new AdminApplicationExcelRow(x, GetAirWaybillDisplay(awbs, x))).ToArray();
		}

		private static string GetAirWaybillDisplay(IEnumerable<AirWaybillData> awbs, ApplicationListItem application)
		{
			return awbs.Where(a => a.Id == application.AirWaybillId)
					   .Select(data => HttpUtility.HtmlDecode(AwbHelper.GetAirWaybillDisplay(data)))
					   .FirstOrDefault();
		}

		public ForwarderApplicationExcelRow[] GetForwarderApplicationExcelRow()
		{
			var items = GetApplicationListItems();

			var awbs = _awbs.Get(items.Select(x => x.AirWaybillId ?? 0).ToArray());

			return items.Select(x => new ForwarderApplicationExcelRow(x, GetAirWaybillDisplay(awbs, x))).ToArray();
		}

		public SenderApplicationExcelRow[] GetSenderApplicationExcelRow()
		{
			var items = GetApplicationListItems();

			var awbs = _awbs.Get(items.Select(x => x.AirWaybillId ?? 0).ToArray());

			return items.Select(x => new SenderApplicationExcelRow(x, GetAirWaybillDisplay(awbs, x))).ToArray();
		}

		private ApplicationListItem[] GetApplicationListItems()
		{
			var stateIds = _stateService.GetVisibleStates();

			var data = _applications.List(stateIds: stateIds, orders: Order.Default);

			var withoutAwb = data.Where(x => !x.AirWaybillId.HasValue).OrderByDescending(x => x.Id);

			var withAwb = data.Where(x => x.AirWaybillId.HasValue);

			data = withoutAwb.Concat(withAwb).ToArray();

			return _itemMapper.Map(data);
		}
	}
}