using System;
using Alicargo.Core.Enums;

namespace Alicargo.Services.Abstract
{
	public interface ILocalizationService
	{
		string GetDate(DateTimeOffset? date, string culture, TimeSpan? timeZone = null);
		string GetMethodOfDelivery(MethodOfDelivery methodOfDelivery, string culture);
		string GetMethodOfTransit(MethodOfTransit methodOfTransit, string culture);
		string GetDeliveryType(DeliveryType deliveryType, string culture);
		string GetStateName(long stateId, string culture);
	}
}
