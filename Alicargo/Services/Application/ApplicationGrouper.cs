using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.AirWaybill;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Awb;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	using TOrder = Tuple<Func<ApplicationListItem, string>, string>;

	internal sealed class ApplicationGrouper : IApplicationGrouper
	{
		private readonly IAwbRepository _awbRepository;
		private readonly IDictionary<OrderType, TOrder> _map;
		private Dictionary<long, AirWaybillData> _airWaybills;
		private Dictionary<long, AirWaybillAggregate> _awbAggregates;
		private int _countWithouAwb;
		private decimal _valueWithouAwb;
		private float _volumeWithouAwb;
		private float _weightWithouAwb;

		public ApplicationGrouper(IAwbRepository awbRepository)
		{
			_awbRepository = awbRepository;

			_map = new Dictionary<OrderType, TOrder>
			{
				{ OrderType.Country, new TOrder(item => item.CountryName, OrderHelper.CountryFieldName) },
				{ OrderType.Factory, new TOrder(item => item.FactoryName, OrderHelper.FactoryFieldName) },
				{ OrderType.Mark, new TOrder(item => item.MarkName, OrderHelper.MarkFieldName) },
				{ OrderType.Sender, new TOrder(item => item.SenderName, OrderHelper.SenderFieldName) },
				{ OrderType.Forwarder, new TOrder(item => item.ForwarderName, OrderHelper.ForwarderFieldName) },
				{ OrderType.City, new TOrder(item => item.TransitCity, OrderHelper.CityFieldName) },
				{ OrderType.Carrier, new TOrder(item => item.CarrierName, OrderHelper.CarrierFieldName) },
				{ OrderType.State, new TOrder(item => item.State.StateName, OrderHelper.StateFieldName) },
				{ OrderType.MethodOfTransit, new TOrder(item => item.TransitMethodOfTransitString, OrderHelper.MethodOfTransitFieldName) },
				{ OrderType.Client, new TOrder(item => item.ClientNic, OrderHelper.ClientFieldName) }
			};
		}

		public ApplicationGroup[] Group(ApplicationListItem[] applications, OrderType[] groups, long? clientId = null,
			long? senderId = null, long? forwarderId = null, long? carrierId = null)
		{
			var awbIds = applications.Select(x => x.AirWaybillId ?? 0).Distinct().ToArray();

			_airWaybills = _awbRepository.Get(awbIds).ToDictionary(x => x.Id, x => x);
			_awbAggregates = _awbRepository.GetAggregate(awbIds, clientId, senderId, forwarderId, carrierId)
				.ToDictionary(x => x.AirWaybillId, x => x);
			_countWithouAwb = _awbRepository.GetTotalCountWithouAwb(clientId, senderId, forwarderId, carrierId);
			_weightWithouAwb = _awbRepository.GetTotalWeightWithouAwb(clientId, senderId, forwarderId, carrierId);
			_valueWithouAwb = _awbRepository.GetTotalValueWithouAwb(clientId, senderId, forwarderId, carrierId);
			_volumeWithouAwb = _awbRepository.GetTotalVolumeWithouAwb(clientId, senderId, forwarderId, carrierId);

			return GroupImpl(applications, groups);
		}

		private ApplicationGroup[] ByAirWaybill(IEnumerable<ApplicationListItem> applications, OrderType[] groups)
		{
			return applications
				.GroupBy(x => x.AirWaybillId ?? 0)
				.Select(grouping => GetApplicationGroup(grouping, groups, OrderHelper.AwbFieldName,
					delegate(IGrouping<long, ApplicationListItem> awb)
					{
						if(_airWaybills.ContainsKey(awb.Key))
						{
							var item = _airWaybills[awb.Key];

							return string.Format("{{\"id\":\"{0}\",\"text\":\"{1}\"}}", item.Id, AwbHelper.GetAirWaybillDisplay(item));
						}

						return null;
					}))
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

			if(field == OrderHelper.AwbFieldName)
			{
				var id = (long)(object)grouping.Key;
				if(_awbAggregates.ContainsKey(id))
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

		private ApplicationGroup[] Group(IEnumerable<ApplicationListItem> applications, OrderType[] groups,
			Func<ApplicationListItem, string> selector, string fieldName)
		{
			return applications.GroupBy(selector)
				.Select(grouping => GetApplicationGroup(grouping, groups, fieldName, g => g.Key))
				.ToArray();
		}

		private ApplicationGroup[] GroupImpl(IEnumerable<ApplicationListItem> applications,
			IReadOnlyCollection<OrderType> groups)
		{
			var current = groups.First();
			var rest = groups.Except(new[] { current }).ToArray();

			return current == OrderType.AirWaybill
				? ByAirWaybill(applications, rest)
				: Group(applications, rest, _map[current].Item1, _map[current].Item2);
		}
	}
}