using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Excel
{
	internal sealed class ApplicationExcelRowSource : IApplicationExcelRowSource
	{
		private readonly IApplicationRepository _applications;
		private readonly IApplicationListItemMapper _itemMapper;
		private readonly IStateService _stateService;

		public ApplicationExcelRowSource(
			IApplicationRepository applications,
			IStateService stateService,
			IApplicationListItemMapper itemMapper)
		{
			_applications = applications;
			_stateService = stateService;
			_itemMapper = itemMapper;
		}

		public AdminApplicationExcelRow[] GetAdminApplicationExcelRow()
		{
			var items = GetApplicationListItems();

			return items.Select(x => new AdminApplicationExcelRow(x)).ToArray();
		}

		public ForwarderApplicationExcelRow[] GetForwarderApplicationExcelRow()
		{
			var items = GetApplicationListItems();

			return items.Select(x => new ForwarderApplicationExcelRow(x)).ToArray();
		}

		public SenderApplicationExcelRow[] GetSenderApplicationExcelRow()
		{
			var items = GetApplicationListItems();

			return items.Select(x => new SenderApplicationExcelRow(x)).ToArray();
		}

		private IEnumerable<ApplicationListItem> GetApplicationListItems()
		{
			var stateIds = _stateService.GetVisibleStates();

			var data = _applications.List(stateIds: stateIds, orders: Order.Default);

			return _itemMapper.Map(data);
		}
	}
}