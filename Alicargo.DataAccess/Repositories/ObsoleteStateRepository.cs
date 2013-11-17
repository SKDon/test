using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class ObsoleteStateRepository : IObsoleteStateRepository
	{
		private readonly Expression<Func<State, ObsoleteStateData>> _selector;
		private readonly AlicargoDataContext _context;

		public ObsoleteStateRepository(IUnitOfWork unitOfWork)
		{
			_context = (AlicargoDataContext)unitOfWork.Context;

			_selector = x => new ObsoleteStateData
			{
				Id = x.Id,
				Name = x.Name,
				Position = x.Position
			};
		}

		public long Count()
		{
			return _context.States.LongCount();
		}

		public ObsoleteStateData[] GetAll()
		{
			var localizations = _context.StateLocalizations.ToArray();

			var states = _context.States
				.Select(_selector)
				.OrderBy(x => x.Position)
				.ToArray();

			foreach (var state in states)
			{
				AdjustLocalization(state, localizations);
			}

			return states;
		}

		public ObsoleteStateData Get(long id)
		{
			return Get(x => x.Id == id);
		}

		public long[] GetStateAvailability(RoleType role)
		{
			return _context.StateAvailability
				.Where(x => x.RoleId == (int)role)
				.OrderBy(x => x.State.Position)
				.Select(x => x.StateId)
				.ToArray();
		}

		public RoleType[] GetAvailableRoles(long stateId)
		{
			return _context.StateAvailability
				.Where(x => x.StateId == stateId)
				.OrderBy(x => x.State.Position)
				.Select(x => x.RoleId)
				.Cast<RoleType>()
				.ToArray();
		}

		public long[] GetStateVisibility(RoleType role)
		{
			return _context.StateVisibility
				.Where(x => x.RoleId == (int)role)
				.OrderBy(x => x.State.Position)
				.Select(x => x.StateId)
				.ToArray();
		}

		#region private

		private ObsoleteStateData Get(Expression<Func<State, bool>> expression)
		{
			var state = _context.States.Where(expression).Select(_selector).First();

			var localizations = _context.StateLocalizations.ToArray();

			AdjustLocalization(state, localizations);

			return state;
		}

		private static void AdjustLocalization(ObsoleteStateData state, StateLocalization[] localizations)
		{
			state.Localization.Add(TwoLetterISOLanguageName.Russian, GetName(localizations, TwoLetterISOLanguageName.Russian, state));
			state.Localization.Add(TwoLetterISOLanguageName.Italian, GetName(localizations, TwoLetterISOLanguageName.Italian, state));
			state.Localization.Add(TwoLetterISOLanguageName.English, GetName(localizations, TwoLetterISOLanguageName.English, state));
		}

		private static string GetName(IEnumerable<StateLocalization> localizations, string twoLetterISOLanguageName,
			ObsoleteStateData state)
		{
			return localizations
				.Where(x => x.TwoLetterISOLanguageName.Equals(twoLetterISOLanguageName, StringComparison.InvariantCultureIgnoreCase)
							&& x.StateId == state.Id)
				.Select(x => x.Name)
				.FirstOrDefault() ?? state.Name;
		}

		#endregion
	}
}
