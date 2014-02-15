using System.Collections.Generic;
using System.Linq;

namespace Alicargo.DataAccess.Contracts.Helpers
{
	public static class OrderHelper
	{
		public const string StateFieldName = "State";
		public const string AwbFieldName = "AirWaybill";
		public const string IdFieldName = "Id";
		public const string ClientFieldName = "ClientNic";
		public const string CountryFieldName = "CountryName";
		public const string FactoryFieldName = "FactoryName";
		public const string MarkFieldName = "MarkName";
		public const string SenderFieldName = "SenderName";
		public const string ForwarderFieldName = "ForwarderName";
		public const string CityFieldName = "TransitCity";
		public const string CarrierFieldName = "CarrierName";

		private static readonly IDictionary<string, OrderType> Map = new Dictionary<string, OrderType>
		{
			{ ClientFieldName, OrderType.Client },
			{ FactoryFieldName, OrderType.Factory },
			{ MarkFieldName, OrderType.Mark },
			{ SenderFieldName, OrderType.Sender },
			{ ForwarderFieldName, OrderType.Forwarder },
			{ CityFieldName, OrderType.City },
			{ CarrierFieldName, OrderType.Carrier },
			{ StateFieldName, OrderType.State },
			{ CountryFieldName, OrderType.Country },
			{ AwbFieldName, OrderType.AirWaybill }
		};

		public static Order[] Get(Dictionary<string, string>[] group)
		{
			if(@group == null)
			{
				return null;
			}

			return @group.Select(x => new Order
			{
				OrderType = Map[x["field"]],
				Desc = x["dir"] == "desc"
			}).ToArray();
		}
	}
}