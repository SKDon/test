using System.Linq;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.Excel
{
	internal sealed class ClientApplicationExcelRowSource : IApplicationExcelRowSource
	{
		private readonly IApplicationRepository _applications;
		private readonly IApplicationListItemMapper _itemMapper;
		private readonly IStateService _stateService;

		public ClientApplicationExcelRowSource(IApplicationRepository applications, IStateService stateService,
										 IApplicationListItemMapper itemMapper)
		{
			_applications = applications;
			_stateService = stateService;
			_itemMapper = itemMapper;
		}

		public ApplicationExcelRow[] GetByClient(long clientId)
		{
			var stateIds = _stateService.GetVisibleStates();

			var data = _applications.List(stateIds: stateIds, clientId: clientId,
										  orders: new[] { new Order { Desc = true, OrderType = OrderType.Id } });

			var items = _itemMapper.Map(data);

			return items.Select(x => new ApplicationExcelRow(x)).ToArray();
		}
	}
}