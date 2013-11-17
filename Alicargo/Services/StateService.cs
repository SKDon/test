﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services.Abstract;
using Alicargo.Services.Abstract;
using Microsoft.Ajax.Utilities;

namespace Alicargo.Services
{
	internal sealed class StateService : IStateService
	{
		private readonly IIdentityService _identity;
		private readonly IStateConfig _stateConfig;
		private readonly IObsoleteStateRepository _stateRepository;
		private readonly ConcurrentDictionary<long, bool> _permissions = new ConcurrentDictionary<long, bool>();
		private readonly IAwbRepository _awbRepository;

		public StateService(IObsoleteStateRepository stateRepository, IIdentityService identity, IStateConfig stateConfig, IAwbRepository awbRepository)
		{
			_stateRepository = stateRepository;
			_identity = identity;
			_stateConfig = stateConfig;
			_awbRepository = awbRepository;
		}

		public long[] GetStateAvailabilityToSet()
		{
			if (_identity.IsInRole(RoleType.Admin))
			{
				return _stateRepository.GetStateAvailability(RoleType.Admin);
			}

			if (_identity.IsInRole(RoleType.Client))
			{
				return _stateRepository.GetStateAvailability(RoleType.Client).Except(_stateConfig.AwbStates).ToArray();
			}

			if (_identity.IsInRole(RoleType.Forwarder))
			{
				return _stateRepository.GetStateAvailability(RoleType.Forwarder);
			}

			if (_identity.IsInRole(RoleType.Sender))
			{
				return _stateRepository.GetStateAvailability(RoleType.Sender).Except(_stateConfig.AwbStates).ToArray();
			}

			// todo: 3. a Broker should not be here because he don't use states
			if (_identity.IsInRole(RoleType.Broker))
			{
				return _stateRepository.GetStateAvailability(RoleType.Broker);
			}

			throw new InvalidLogicException("Unsupported role");
		}

		public long[] GetStateVisibility()
		{
			if (_identity.IsInRole(RoleType.Admin))
			{
				return _stateRepository.GetStateVisibility(RoleType.Admin);
			}

			if (_identity.IsInRole(RoleType.Client))
			{
				return _stateRepository.GetStateVisibility(RoleType.Client);
			}

			if (_identity.IsInRole(RoleType.Forwarder))
			{
				return _stateRepository.GetStateVisibility(RoleType.Forwarder);
			}

			if (_identity.IsInRole(RoleType.Sender))
			{
				return _stateRepository.GetStateVisibility(RoleType.Sender);
			}

			// todo: 3. Broker should not be here because he don't use states
			if (_identity.IsInRole(RoleType.Broker))
			{
				return _stateRepository.GetStateVisibility(RoleType.Broker);
			}

			throw new InvalidLogicException();
		}

		public bool HasPermissionToSetState(long stateId)
		{
			// todo: 2. move cache to an interception
			return _permissions.GetOrAdd(stateId, x =>
			{
				var roles = _stateRepository.GetAvailableRoles(x);
				return roles.Any(role => _identity.IsInRole(role));
			});
		}

		public long[] FilterByPosition(long[] states, int position)
		{
			return _stateRepository.GetAll() // todo: 3. pass state ids to Get
				.Where(x => states.Contains(x.Id) && x.Position >= position)
				.Select(x => x.Id)
				.ToArray();
		}

		public long[] ApplyBusinessLogicToStates(ApplicationData applicationData, long[] stateAvailability)
		{
			var states = stateAvailability.ToList();

			if (!applicationData.Weight.HasValue || !applicationData.Count.HasValue)
			{
				states.Remove(_stateConfig.CargoInStockStateId);
			}

			#region AWB

			if (!applicationData.AirWaybillId.HasValue)
			{
				states.Remove(_stateConfig.CargoIsFlewStateId);
			}

			if (applicationData.AirWaybillId.HasValue)
			{
				var airWaybillData = _awbRepository.Get(applicationData.AirWaybillId.Value).First();
				if (airWaybillData.GTD.IsNullOrWhiteSpace())
				{
					states.Remove(_stateConfig.CargoAtCustomsStateId);
				}
			}
			else
			{
				states.Remove(_stateConfig.CargoAtCustomsStateId);
			}

			#endregion

			return states.ToArray();
		}

		public Dictionary<long, string> GetLocalizedDictionary(long[] stateIds = null)
		{
			var states = stateIds != null
				? _stateRepository.GetAll().Where(x => stateIds.Contains(x.Id)).ToArray()
				: _stateRepository.GetAll();

			if (stateIds != null && stateIds.Distinct().Count() != states.Length)
			{
				// todo: 3. Test
				throw new InvalidDataException("Can't find a state");
			}

			var dictionary = states
				.Select(x => new { x.Id, Name = x.Localization[CultureInfo.CurrentUICulture.TwoLetterISOLanguageName], x.Position })
				.OrderBy(x => x.Position)
				.ToDictionary(x => x.Id, x => x.Name);

			return dictionary;
		}

		public Dictionary<long, ObsoleteStateData> GetDictionary()
		{
			return _stateRepository.GetAll().ToDictionary(x => x.Id, x => x);
		}
	}
}