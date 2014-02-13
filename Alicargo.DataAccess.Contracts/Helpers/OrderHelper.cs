using System.Collections.Generic;
using System.Linq;

namespace Alicargo.DataAccess.Contracts.Helpers
{
	public static class OrderHelper
	{
		public const string LegalEntityFieldName = "ClientLegalEntity";
		public const string StateFieldName = "State";
		public const string AwbFieldName = "AirWaybill";
		public const string IdFieldName = "Id";
		public const string ClientNicFieldName = "ClientNic";

		private static readonly IDictionary<string, OrderType> Map = new Dictionary<string, OrderType>
		{
			{ LegalEntityFieldName, OrderType.LegalEntity },
			{ ClientNicFieldName, OrderType.ClientNic },
			{ StateFieldName, OrderType.State },
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