using System.Collections.Generic;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Contracts.State;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Services.Application;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Tests.Services.Application
{
	[TestClass]
	public class ApplicationPresenterTest
	{
		private ApplicationData _applicationData;
		private MockContainer _context;
		private ApplicationPresenter _service;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new MockContainer();

			_service = _context.Create<ApplicationPresenter>();

			_applicationData = _context.Create<ApplicationData>();
		}

		[TestMethod]
		public void Test_GetStateAvailability_ByAdmin()
		{
			var stateAvailability = _context.CreateMany<long>(6).ToArray();
			var dictionary = _context.Create<Dictionary<long, StateData>>();

			_context.ApplicationRepository.Setup(x => x.Get(_applicationData.Id)).Returns(_applicationData);
			_context.StateFilter.Setup(x => x.GetStateAvailabilityToSet()).Returns(stateAvailability);
			_context.IdentityService.Setup(x => x.IsInRole(RoleType.Admin)).Returns(true);
			_context.IdentityService.Setup(x => x.Language).Returns(TwoLetterISOLanguageName.English);
			_context.StateRepository.Setup(x => x.Get(TwoLetterISOLanguageName.English, stateAvailability)).Returns(dictionary);

			var stateModels = _service.GetStateAvailability(_applicationData.Id);

			foreach (var model in stateModels)
			{
				dictionary[model.StateId].LocalizedName.ShouldBeEquivalentTo(model.StateName);
			}
			_context.IdentityService.Verify(x => x.IsInRole(RoleType.Admin), Times.Once());
			_context.ApplicationRepository.Verify(x => x.Get(_applicationData.Id), Times.Once());
			_context.StateFilter.Verify(x => x.GetStateAvailabilityToSet(), Times.Once());
			_context.IdentityService.Verify(x => x.Language, Times.Exactly(1));
			_context.StateRepository.Verify(x => x.Get(TwoLetterISOLanguageName.English, stateAvailability), Times.Once());
		}

		[TestMethod]
		public void Test_GetStateAvailability()
		{
			var stateAvailability = _context.CreateMany<long>(6).ToArray();
			var withLogic = stateAvailability.Take(3).ToArray();
			var dictionary = _context.Create<Dictionary<long, StateData>>();

			_context.ApplicationRepository.Setup(x => x.Get(_applicationData.Id)).Returns(_applicationData);
			_context.StateFilter.Setup(x => x.GetStateAvailabilityToSet()).Returns(stateAvailability);
			_context.IdentityService.Setup(x => x.IsInRole(RoleType.Admin)).Returns(false);
			_context.IdentityService.Setup(x => x.IsInRole(RoleType.Manager)).Returns(false);
			_context.StateFilter.Setup(x => x.FilterByBusinessLogic(_applicationData, stateAvailability))
				.Returns(withLogic);
			_context.StateRepository.Setup(x => x.Get(TwoLetterISOLanguageName.English, withLogic)).Returns(dictionary);
			_context.IdentityService.Setup(x => x.Language).Returns(TwoLetterISOLanguageName.English);

			var stateModels = _service.GetStateAvailability(_applicationData.Id);

			foreach (var model in stateModels)
			{
				dictionary[model.StateId].LocalizedName.ShouldBeEquivalentTo(model.StateName);
			}
			_context.IdentityService.Verify(x => x.IsInRole(RoleType.Admin), Times.Once());
			_context.IdentityService.Verify(x => x.IsInRole(RoleType.Manager), Times.Once());
			_context.StateFilter.Verify(x => x.FilterByBusinessLogic(_applicationData, stateAvailability), Times.Once());
			_context.ApplicationRepository.Verify(x => x.Get(_applicationData.Id), Times.Once());
			_context.StateFilter.Verify(x => x.GetStateAvailabilityToSet(), Times.Once());
			_context.StateRepository.Verify(x => x.Get(TwoLetterISOLanguageName.English, withLogic), Times.Once());
			_context.IdentityService.Verify(x => x.Language, Times.Once());
		}
	}
}