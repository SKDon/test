﻿using System;
using System.Globalization;
using Alicargo.Core.Enums;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;

namespace Alicargo.Services
{
	public sealed class LocalizationService : ILocalizationService
	{
		private readonly IStateRepository _stateRepository;

		public LocalizationService(IStateRepository stateRepository)
		{
			_stateRepository = stateRepository;
		}

		public string GetDate(DateTimeOffset? date, string culture, TimeSpan? timeZone)
		{
			// todo: apply timezone
			return date.HasValue ? date.Value.Date.ToString("d", CultureInfo.GetCultureInfo(culture)) : null;
		}

		public string GetMethodOfDelivery(MethodOfDelivery methodOfDelivery, string culture)
		{
			return Resources.Entities.ResourceManager.GetString("MethodOfDelivery", CultureInfo.GetCultureInfo(culture));
		}

		public string GetCurrency(decimal value, CurrencyType currency, string culture)
		{
			return value.ToString("0.00", CultureInfo.GetCultureInfo(culture)) + CurrencyName.Names[currency];
		}

		public string GetMethodOfTransit(MethodOfTransit methodOfTransit, string culture)
		{
			return Resources.Entities.ResourceManager.GetString("MethodOfTransit", CultureInfo.GetCultureInfo(culture));
		}

		public string GetDeliveryType(DeliveryType deliveryType, string culture)
		{
			return Resources.Entities.ResourceManager.GetString("DeliveryType", CultureInfo.GetCultureInfo(culture));
		}

		public string GetStateName(long stateId, string culture)
		{
			return _stateRepository.Get(stateId).Localization[culture];
		}
	}
}