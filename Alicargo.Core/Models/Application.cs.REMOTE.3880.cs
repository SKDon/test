using System;
using System.Data.Linq;
using Alicargo.Core.Enums;

namespace Alicargo.Core.Models
{
	public sealed class Application
	{
		public long Id { get; set; }
		
		public long ClientUserId { get; set; }
		public string LegalEntity { get; set; }

		public DateTimeOffset? CreationTimestamp { get; set; }
		public string ReferenceBill { get; set; }
		public string StateName { get; set; }
		public long StateId { get; set; }
		public string StateNext { get; set; }
		public bool StateIsClose { get; set; }

		public DateTimeOffset StateChangeTimestamp { get; set; }
		public string FactoryName { get; set; }
		public string FactoryEmail { get; set; }
		public string FactoryPhone { get; set; }
		public string MarkName { get; set; }
		public string Invoice { get; set; }
		public string FileName { get; set; }
		public string Characteristic { get; set; }
		public string AddressLoad { get; set; }
		public string WarehouseWorkingTime { get; set; }
		public float Gross { get; set; }
		public int Count { get; set; }
		public string Volume { get; set; }
		public string TermsOfDelivery { get; set; }
		public decimal Value { get; set; }
		public MethodOfDelivery MethodOfDelivery { get; set; }
		public Binary FileData { get; set; }

		public  Transit Transit { get; set; }
	}
}