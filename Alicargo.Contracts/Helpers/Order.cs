using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.Helpers;

namespace Alicargo.Contracts.Helpers
{
	public sealed class Order
	{
		public OrderType OrderType { get; set; }
		public string Field { get { return OrderType.ToString(); } }
		public bool Desc { get; set; }

		public static Order[] Get(Dictionary<string, string>[] group)
		{
			if (group == null)
			{
				return null;
			}

			// todo: move to an utility
			return group.Select(x => new Order
			{
				OrderType = (OrderType)Enum.Parse(typeof(OrderType), x["field"], true),
				Desc = x["dir"] == "desc"
			}).ToArray();
		}
	}
}