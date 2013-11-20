﻿using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
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
			_context.StateService.Setup(x => x.GetStateAvailabilityToSet()).Returns(stateAvailability);
			_context.IdentityService.Setup(x => x.IsInRole(RoleType.Admin)).Returns(true);
			_context.IdentityService.Setup(x => x.TwoLetterISOLanguageName).Returns(TwoLetterISOLanguageName.English);
			_context.StateRepository.Setup(x => x.Get(stateAvailability)).Returns(dictionary);

			var stateModels = _service.GetStateAvailability(_applicationData.Id);

			foreach (var model in stateModels)
			{
				dictionary[model.StateId].Localization[TwoLetterISOLanguageName.English].ShouldBeEquivalentTo(model.StateName);
			}
			_context.IdentityService.Verify(x => x.IsInRole(RoleType.Admin), Times.Once());
			_context.ApplicationRepository.Verify(x => x.Get(_applicationData.Id), Times.Once());
			_context.StateService.Verify(x => x.GetStateAvailabilityToSet(), Times.Once());
			_context.IdentityService.Verify(x => x.TwoLetterISOLanguageName, Times.Exactly(dictionary.Count));
			_context.StateRepository.Verify(x => x.Get(stateAvailability), Times.Once());
		}

		[TestMethod]
		public void Test_GetStateAvailability()
		{
			var stateAvailability = _context.CreateMany<long>(6).ToArray();
			var withLogic = stateAvailability.Take(3).ToArray();
			var filtered = withLogic.Take(2).ToArray();
			var dictionary = _context.Create<Dictionary<long, StateData>>();
			var currentState = _context.Create<StateData>();

			_context.ApplicationRepository.Setup(x => x.Get(_applicationData.Id)).Returns(_applicationData);
			_context.StateService.Setup(x => x.GetStateAvailabilityToSet()).Returns(stateAvailability);
			_context.IdentityService.Setup(x => x.IsInRole(RoleType.Admin)).Returns(false);
			_context.StateRepository.Setup(x => x.Get(new[] { _applicationData.StateId })).Returns(new Dictionary<long, StateData>
			{
				{_applicationData.StateId, currentState}
			});
			_context.StateService.Setup(x => x.FilterByBusinessLogic(_applicationData, stateAvailability))
				.Returns(withLogic);
			_context.StateService.Setup(x => x.FilterByPosition(withLogic, currentState.Position)).Returns(filtered);
			_context.StateRepository.Setup(x => x.Get(filtered)).Returns(dictionary);
			_context.IdentityService.Setup(x => x.TwoLetterISOLanguageName).Returns(TwoLetterISOLanguageName.English);

			var stateModels = _service.GetStateAvailability(_applicationData.Id);

			foreach (var model in stateModels)
			{
				dictionary[model.StateId].Localization[TwoLetterISOLanguageName.English].ShouldBeEquivalentTo(model.StateName);
			}
			_context.IdentityService.Verify(x => x.IsInRole(RoleType.Admin), Times.Once());
			_context.StateService.Verify(x => x.FilterByBusinessLogic(_applicationData, stateAvailability), Times.Once());
			_context.StateService.Verify(x => x.FilterByPosition(withLogic, currentState.Position), Times.Once());
			_context.ApplicationRepository.Verify(x => x.Get(_applicationData.Id), Times.Once());
			_context.StateService.Verify(x => x.GetStateAvailabilityToSet(), Times.Once());
			_context.StateRepository.Verify(x => x.Get(new[] { _applicationData.StateId }), Times.Once());
			_context.StateRepository.Verify(x => x.Get(filtered), Times.Once());
			_context.IdentityService.Verify(x => x.TwoLetterISOLanguageName, Times.Exactly(dictionary.Count));
		}
	}
}