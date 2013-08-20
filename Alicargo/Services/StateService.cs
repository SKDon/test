using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Microsoft.Ajax.Utilities;

namespace Alicargo.Services
{
	public sealed class StateService : IStateService
	{
		private readonly IIdentityService _identity;
		private readonly IStateConfig _stateConfig;
		private readonly IStateRepository _stateRepository;
		private readonly ConcurrentDictionary<long, bool> _permissions = new ConcurrentDictionary<long, bool>();
		private readonly IAwbRepository _awbRepository;

		public StateService(IStateRepository stateRepository, IIdentityService identity, IStateConfig stateConfig, IAwbRepository awbRepository)
		{
			_stateRepository = stateRepository;
			_identity = identity;
			_stateConfig = stateConfig;
			_awbRepository = awbRepository;
		}

        // todo: 1. test
		public long[] GetAvailableStatesToSet()
		{
			if (_identity.IsInRole(RoleType.Admin))
			{
				return _stateRepository.GetAvailableStates(RoleType.Admin);
			}

			if (_identity.IsInRole(RoleType.Client))
			{
				return _stateRepository.GetAvailableStates(RoleType.Client).Except(_stateConfig.AwbStates).ToArray();
			}

			if (_identity.IsInRole(RoleType.Forwarder))
			{
				return _stateRepository.GetAvailableStates(RoleType.Forwarder);
			}

			if (_identity.IsInRole(RoleType.Sender))
			{
				return _stateRepository.GetAvailableStates(RoleType.Sender).Except(_stateConfig.AwbStates).ToArray();
			}

			// todo: 3. a brocker should not be here because he don't use states
			if (_identity.IsInRole(RoleType.Brocker))
			{
				return _stateRepository.GetAvailableStates(RoleType.Brocker);
			}

			throw new InvalidLogicException("Unsupported role");
		}

        // todo: 1. test
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

			// todo: 3. brocker should not be here because he don't use states
			if (_identity.IsInRole(RoleType.Brocker))
			{
				return _stateRepository.GetVisibleStates(RoleType.Brocker);
			}

			throw new InvalidLogicException();
		}

        // todo: 1. test
		public bool HasPermissionToSetState(long stateId)
		{
			// todo: 2. move cache to an interception
			return _permissions.GetOrAdd(stateId, x =>
			{
				var roles = _stateRepository.GetAvailableRoles(x);
				return roles.Any(role => _identity.IsInRole(role));
			});
		}

        // todo: 1. test
        // todo: 1.5. move to repository and test
		public long[] FilterByPosition(long[] states, int position)
		{
			return _stateRepository.GetAll() // todo: 3. pass state ids to Get
				.Where(x => states.Contains(x.Id) && x.Position >= position)
				.Select(x => x.Id)
				.ToArray();
		}

        // todo: 1. test
        public long[] ApplyBusinessLogicToStates(ApplicationData applicationData, long[] availableStates)
		{
			var states = availableStates.ToList();

			if (!applicationData.Weigth.HasValue || !applicationData.Count.HasValue)
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
	}
}