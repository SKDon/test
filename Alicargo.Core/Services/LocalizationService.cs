using System;
using System.Globalization;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Core.Resources;
using Alicargo.Core.Services.Abstract;

namespace Alicargo.Core.Services
{
	public sealed class LocalizationService : ILocalizationService
	{
		private readonly IStateRepository _states;

		public LocalizationService(IStateRepository states)
		{
			_states = states;
		}

		public string GetDate(DateTimeOffset? date, string culture, TimeSpan? timeZone)
		{
			// todo: 1.5. apply timezone
			return date.HasValue ? date.Value.Date.ToString("d", CultureInfo.GetCultureInfo(culture)) : null;
		}

		public string GetMethodOfDelivery(MethodOfDelivery methodOfDelivery, string culture)
		{
			return Entities.ResourceManager.GetString("MethodOfDelivery", CultureInfo.GetCultureInfo(culture));
		}

		public string GetMethodOfTransit(MethodOfTransit methodOfTransit, string culture)
		{
			return Entities.ResourceManager.GetString("MethodOfTransit", CultureInfo.GetCultureInfo(culture));
		}

		public string GetDeliveryType(DeliveryType deliveryType, string culture)
		{
			return Entities.ResourceManager.GetString("DeliveryType", CultureInfo.GetCultureInfo(culture));
		}

		public string GetStateName(long stateId, string culture)
		{
			return _states.Get(stateId).Localization[culture];
		}
	}
}