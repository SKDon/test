using System.Linq;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.Exceptions;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Utilities;

namespace Alicargo.Core.State
{
	public sealed class ApplicationStateManager : IApplicationStateManager
	{
		private readonly IStateConfig _config;
		private readonly IApplicationEditor _editor;
		private readonly IIdentityService _identity;
		private readonly IStateSettingsRepository _settings;

		public ApplicationStateManager(
			IStateConfig config,
			IApplicationEditor editor,
			IIdentityService identity,
			IStateSettingsRepository settings)
		{
			_config = config;
			_editor = editor;
			_identity = identity;
			_settings = settings;
		}

		public void SetState(long applicationId, long stateId)
		{
			if(!HasPermissionToSetState(stateId))
				throw new AccessForbiddenException("User don't have access to the state " + stateId);

			// todo: 2. test logic with states (260)
			if(stateId == _config.CargoInStockStateId)
			{
				_editor.SetDateInStock(applicationId, DateTimeProvider.Now);
			}

			_editor.SetState(applicationId, stateId);
		}

		private bool HasPermissionToSetState(long stateId)
		{
			return _settings.GetStateAvailabilities()
				.Any(x => x.StateId == stateId && _identity.IsInRole(x.Role));
		}
	}
}