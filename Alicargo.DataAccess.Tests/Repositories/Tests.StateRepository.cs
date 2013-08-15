using System.Linq;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Enums;
using Alicargo.Core.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.DataAccess.Tests.Repositories
{
	public partial class Tests
	{
		private const long DefaultStateId = 1;

		[TestMethod]
		public void Test_StateRepository_GetAll_Count()
		{
			var all = _stateRepository.GetAll();

			var count = _stateRepository.Count();

			Assert.AreEqual(count, all.Length);
		}

		[TestMethod]
		public void Test_StateRepository_Get_GetDefaultState()
		{
			var state = _stateRepository.Get(DefaultStateId);

			Assert.AreEqual("Nuovo", state.Localization[TwoLetterISOLanguageName.Italian]);
			Assert.AreEqual("New order", state.Localization[TwoLetterISOLanguageName.English]);
			Assert.AreEqual("Новая заявка", state.Localization[TwoLetterISOLanguageName.Russian]);
			Assert.AreEqual(10, state.Position);
		}

		//static bool AreEqual(State one, State other)
		//{
		//	return one.Id == other.Id
		//		   && AreEquals(one.Localization, other.Localization)
		//		   && string.Equals(one.Name, other.Name);
		//}

		//// ReSharper disable SuggestBaseTypeForParameter
		//static bool AreEquals(IDictionary<string, string> one, IDictionary<string, string> other)// ReSharper restore SuggestBaseTypeForParameter
		//{
		//	if (one.Count != other.Count)
		//		return false;

		//	foreach (var item in one)
		//	{
		//		if (!other.ContainsKey(item.Key))
		//			return false;

		//		if (!string.Equals(other[item.Key], item.Value, StringComparison.InvariantCultureIgnoreCase))
		//			return false;
		//	}

		//	return true;
		//}

		[TestMethod]
		public void Test_StateRepository_GetAvailableStates()
		{
			var states = _stateRepository.GetAvailableStates(RoleType.Admin);

			var all = _stateRepository.GetAll().ToArray();

			Assert.AreEqual(all.Length, states.Length);

			foreach (var state in all)
			{
				Assert.IsTrue(states.Contains(state.Id));
			}
		}

		[TestMethod]
		public void Test_StateRepository_GetVisibleStates()
		{
			var states = _stateRepository.GetVisibleStates(RoleType.Admin);

			var all = _stateRepository.GetAll();

			Assert.AreEqual(all.Length, states.Length);

			foreach (var state in all)
			{
				Assert.IsTrue(states.Contains(state.Id));
			}
		}

		[TestMethod]
		public void Test_StateRepository_GetAvailableRoles()
		{
			var roles = _stateRepository.GetAvailableRoles(DefaultStateId);

			Assert.AreEqual(3, roles.Length);
			Assert.IsTrue(roles.Contains(RoleType.Admin));
			Assert.IsTrue(roles.Contains(RoleType.Sender));
			Assert.IsTrue(roles.Contains(RoleType.Client));
		}
	}
}
