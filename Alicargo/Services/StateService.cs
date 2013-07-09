﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Alicargo.Core.Contracts;
using Alicargo.Core.Enums;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;

namespace Alicargo.Services
{
	// todo: test
	public sealed class StateService : IStateService
	{
		private readonly IIdentityService _identity;
		private readonly IStateConfig _stateConfig;
		private readonly IStateRepository _stateRepository;
		private readonly ConcurrentDictionary<long, bool> _permissions = new ConcurrentDictionary<long, bool>();

		public StateService(IStateRepository stateRepository, IIdentityService identity, IStateConfig stateConfig)
		{
			_stateRepository = stateRepository;
			_identity = identity;
			_stateConfig = stateConfig;
		}

		public long[] GetAvailableStates()
		{
			if (_identity.IsInRole(RoleType.Admin))
			{
				return _stateRepository.GetAvailableStates(RoleType.Admin);
			}

			if (_identity.IsInRole(RoleType.Client))
			{
				return _stateRepository.GetAvailableStates(RoleType.Client);
			}

			if (_identity.IsInRole(RoleType.Forwarder))
			{
				return _stateRepository.GetAvailableStates(RoleType.Forwarder);
			}

			if (_identity.IsInRole(RoleType.Sender))
			{
				return _stateRepository.GetAvailableStates(RoleType.Sender);
			}

			throw new InvalidLogicException();
		}

		public long[] GetVisibleStates()
		{
			if (_identity.IsInRole(RoleType.Admin))
			{
				return _stateRepository.GetVisibleStates(RoleType.Admin);
			}

			if (_identity.IsInRole(RoleType.Client))
			{
				return _stateRepository.GetVisibleStates(RoleType.Client);
			}

			if (_identity.IsInRole(RoleType.Forwarder))
			{
				return _stateRepository.GetVisibleStates(RoleType.Forwarder);
			}

			if (_identity.IsInRole(RoleType.Sender))
			{
				return _stateRepository.GetVisibleStates(RoleType.Sender);
			}

			throw new InvalidLogicException();
		}

		public Dictionary<long, string> GetLocalizedDictionary(IEnumerable<long> stateIds = null)
		{
			var states = stateIds != null ? _stateRepository.GetAll().Where(x => stateIds.Contains(x.Id)) : _stateRepository.GetAll();

			var dictionary = states
				.Select(x => new { x.Id, Name = x.Localization[CultureInfo.CurrentUICulture.TwoLetterISOLanguageName], x.Position })
				.OrderBy(x => x.Position)
				.ToDictionary(x => x.Id, x => x.Name);

			return dictionary;
		}

		public Dictionary<long, StateData> GetDictionary()
		{
			return _stateRepository.GetAll().ToDictionary(x => x.Id, x => x);
		}

		public bool HasPermissionToSetState(long stateId)
		{
			// todo: refactor
			return _permissions.GetOrAdd(stateId, x =>
			{
				if (_stateConfig.AwbStates.Contains(stateId))
				{
					// todo: test
					if (_identity.IsInRole(RoleType.Admin) || _identity.IsInRole(RoleType.Sender) || _identity.IsInRole(RoleType.Brocker))
					{
						return true;
					}
				}

				var roles = _stateRepository.GetAvailableRoles(x);
				return roles.Any(role => _identity.IsInRole(role));
			});
		}
	}
}