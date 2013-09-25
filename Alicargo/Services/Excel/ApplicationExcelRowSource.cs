using System;
using System.Linq;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.Excel
{
	internal sealed class ApplicationExcelRowSource : IApplicationExcelRowSource
	{
		private readonly IApplicationRepository _applications;
		private readonly IApplicationListItemMapper _itemMapper;
		private readonly IIdentityService _identity;
		private readonly IStateService _stateService;

		public ApplicationExcelRowSource(IApplicationRepository applications, IStateService stateService,
										 IApplicationListItemMapper itemMapper, IIdentityService identity)
		{
			_applications = applications;
			_stateService = stateService;
			_itemMapper = itemMapper;
			_identity = identity;
		}

		public ApplicationExcelRow[] Get()
		{
			var stateIds = _stateService.GetVisibleStates();

			var data = _applications.List(stateIds: stateIds, orders: new[] { new Order { Desc = true, OrderType = OrderType.Id } });

			var items = _itemMapper.Map(data);

			if (_identity.IsInRole(RoleType.Admin))
			{
				return items.Select(x => (ApplicationExcelRow)new AdminApplicationExcelRow(x)).ToArray();
			}
			if (_identity.IsInRole(RoleType.Sender))
			{
				return items.Select(x => (ApplicationExcelRow)new SenderApplicationExcelRow(x)).ToArray();

			}
			if (_identity.IsInRole(RoleType.Forwarder))
			{
				return items.Select(x => (ApplicationExcelRow)new ForwarderApplicationExcelRow(x)).ToArray();
			}

			throw new NotSupportedException("For other roles the method is not supported");
		}
	}
}