using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Enums;
using Alicargo.Core.Localization;
using Resources;

namespace Alicargo.Core.Models
{
	public sealed class Transit : TransitData
	{
		public Transit() { }

		public Transit(TransitData transit, string carrierName)
			: base(transit)
		{
			CarrierName = carrierName;
		}

		[Required]
		[DisplayNameLocalized(typeof(Entities), "MethodOfTransit")]
		public MethodOfTransit MethodOfTransit
		{
			get { return (MethodOfTransit)MethodOfTransitId; }
			set { MethodOfTransitId = (int)value; }
		}

		public string MethodOfTransitString { get { return MethodOfTransit.ToLocalString(); } }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "DeliveryType")]
		public DeliveryType DeliveryType
		{
			get { return (DeliveryType)DeliveryTypeId; }
			set { DeliveryTypeId = (int)value; }
		}

		public string DeliveryTypeString { get { return DeliveryType.ToLocalString(); } }

		public string CarrierName { get; set; }
	}
}