﻿using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
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
		private int? _countWithouAwb;
		private float? _weightWithouAwb;

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

			_airWaybills = _awbRepository.Get(awbIds).ToDictionary(x => x.Id, x => x);

			_awbAggregates = _awbRepository.GetAggregate(awbIds).ToDictionary(x => x.AirWaybillId, x => x);
			_countWithouAwb = _awbRepository.GetTotalCountWithouAwb();
			_weightWithouAwb = _awbRepository.GetTotalWeightWithouAwb();

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

			if (field == OrderHelper.AwbFieldName)
			{
				var id = (long)(object)grouping.Key;
				if (_awbAggregates.ContainsKey(id))
				{
					count = _awbAggregates[id].TotalCount;
					weight = _awbAggregates[id].TotalWeight;
				}
				else
				{
					count = _countWithouAwb ?? 0;
					weight = _weightWithouAwb ?? 0;
				}
			}
			else
			{
				count = grouping.Sum(y => y.Count ?? 0);
				weight = grouping.Sum(y => y.Weight ?? 0);
			}

			return new ApplicationGroup
			{
				aggregates = new ApplicationGroup.Aggregates(count, weight),
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