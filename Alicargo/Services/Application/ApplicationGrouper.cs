using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Services.Application
{
	using OrderFunction = Func<IEnumerable<ApplicationListItem>, OrderType[], ApplicationGroup[]>;

	internal sealed class ApplicationGrouper : IApplicationGrouper
	{
		private readonly IAwbRepository _awbRepository;
		private readonly IDictionary<OrderType, OrderFunction> _map;
		private Dictionary<long, AirWaybillData> _airWaybills;
		private Dictionary<long, AirWaybillAggregate> _awbAggregates;
		private int _countWithouAwb;
		private float _weightWithouAwb;
		private decimal _valueWithouAwb;
		private float _volumeWithouAwb;

		public ApplicationGrouper(IAwbRepository awbRepository)
		{
			_awbRepository = awbRepository;

			_map = new Dictionary<OrderType, OrderFunction>
			{
				{OrderType.AirWaybill, ByAirWaybill},
				{OrderType.State, ByState},
				{OrderType.ClientNic, ByClientNic},
				{OrderType.LegalEntity, ByLegalEntity}
			};
		}

		public ApplicationGroup[] Group(ApplicationListItem[] applications, OrderType[] groups)
		{
			var awbIds = applications.Select(x => x.AirWaybillId ?? 0)
				.Distinct()
				.ToArray();

			// todo: 2. need to pass current sender or forwarder or client id to the functions to get aggregate information correctry
			_airWaybills = _awbRepository.Get(awbIds).ToDictionary(x => x.Id, x => x);
			_awbAggregates = _awbRepository.GetAggregate(awbIds).ToDictionary(x => x.AirWaybillId, x => x);
			_countWithouAwb = _awbRepository.GetTotalCountWithouAwb() ?? 0;
			_weightWithouAwb = _awbRepository.GetTotalWeightWithouAwb() ?? 0;
			_valueWithouAwb = _awbRepository.GetTotalValueWithouAwb();
			_volumeWithouAwb = _awbRepository.GetTotalVolumeWithouAwb();

			return GroupImpl(applications, groups);
		}

		private ApplicationGroup[] GroupImpl(IEnumerable<ApplicationListItem> applications,
			IReadOnlyCollection<OrderType> groups)
		{
			var current = groups.First();
			var rest = groups.Except(new[] { current }).ToArray();

			return _map[current](applications, rest);
		}

		private ApplicationGroup[] ByClientNic(IEnumerable<ApplicationListItem> applications, OrderType[] groups)
		{
			return applications.GroupBy(x => x.ClientNic)
				.Select(grouping =>
					GetApplicationGroup(grouping, groups, OrderHelper.ClientNicFieldName,
						g => g.Key))
				.ToArray();
		}

		private ApplicationGroup[] ByLegalEntity(IEnumerable<ApplicationListItem> applications, OrderType[] groups)
		{
			return applications.GroupBy(x => x.ClientLegalEntity)
				.Select(grouping =>
					GetApplicationGroup(grouping, groups, OrderHelper.LegalEntityFieldName,
						g => g.Key))
				.ToArray();
		}

		private ApplicationGroup[] ByState(IEnumerable<ApplicationListItem> applications, OrderType[] groups)
		{
			return applications.GroupBy(x => x.State.StateName)
				.Select(grouping =>
					GetApplicationGroup(grouping, groups, OrderHelper.StateFieldName,
						g => g.Key))
				.ToArray();
		}

		private ApplicationGroup[] ByAirWaybill(IEnumerable<ApplicationListItem> applications, OrderType[] groups)
		{
			return applications
				.GroupBy(x => x.AirWaybillId ?? 0)
				.Select(grouping => GetApplicationGroup(grouping, groups, OrderHelper.AwbFieldName,
					awb => _airWaybills.ContainsKey(awb.Key)
						? AwbHelper.GetAirWaybillDisplay(_airWaybills[awb.Key])
						: ""))
				.ToArray();
		}

		private ApplicationGroup GetApplicationGroup<T>(
			IGrouping<T, ApplicationListItem> grouping,
			IReadOnlyCollection<OrderType> orders,
			string field,
			Func<IGrouping<T, ApplicationListItem>, string> getValue)
		{
			int count;
			float weight;
			decimal value;
			float volume;

			if (field == OrderHelper.AwbFieldName)
			{
				var id = (long)(object)grouping.Key;
				if (_awbAggregates.ContainsKey(id))
				{
					count = _awbAggregates[id].TotalCount;
					weight = _awbAggregates[id].TotalWeight;
					value = _awbAggregates[id].TotalValue;
					volume = _awbAggregates[id].TotalVolume;
				}
				else
				{
					count = _countWithouAwb;
					weight = _weightWithouAwb;
					value = _valueWithouAwb;
					volume = _volumeWithouAwb;
				}
			}
			else
			{
				count = grouping.Sum(y => y.Count ?? 0);
				weight = grouping.Sum(y => y.Weight ?? 0);
				value = grouping.Sum(y => y.Value);
				volume = grouping.Sum(y => y.Volume);
			}

			return new ApplicationGroup
			{
				aggregates = new ApplicationGroup.Aggregates(count, weight, value, volume),
				field = field,
				value = getValue(grouping),
				hasSubgroups = orders.Count > 0,
				items = orders.Count > 0
					? GroupImpl(grouping.ToArray(), orders).Cast<object>().ToArray()
					: grouping.Cast<object>().ToArray()
			};
		}
	}
}