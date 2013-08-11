using Alicargo.Contracts.Contracts;
using Alicargo.Services.Application;
// ReSharper disable ImplicitlyCapturedClosure
using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.Enums;
using Alicargo.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Core.Tests.Services
{
	[TestClass]
	public class ApplicationPresenterTest
	{
		private TestHelpers.TestContext _context;
		private ApplicationPresenter _service;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new TestHelpers.TestContext();

			_service = _context.Create<ApplicationPresenter>();
		}

		[TestMethod]
		public void Test_GetAvailableStates_ByAdmin()
		{
			var applicationData = _context.Create<ApplicationData>();
			var availableStates = _context.CreateMany<long>().ToArray();
			var dictionary = _context.Create<Dictionary<long, string>>();

			_context.ApplicationRepository.Setup(x => x.Get(applicationData.Id)).Returns(applicationData);
			_context.StateService.Setup(x => x.GetAvailableStatesToSet()).Returns(availableStates);
			_context.IdentityService.Setup(x => x.IsInRole(RoleType.Admin)).Returns(true);
			_context.StateService.Setup(x => x.GetLocalizedDictionary(availableStates))
				.Returns(dictionary);

			var stateModels = _service.GetAvailableStates(applicationData.Id);
			foreach (var model in stateModels)
			{
				dictionary[model.StateId].ShouldBeEquivalentTo(model.StateName);
			}

			_context.IdentityService.Verify(x => x.IsInRole(RoleType.Admin), Times.Once());
			_context.StateService.Verify(x => x.GetLocalizedDictionary(availableStates), Times.Once());
		}

		[TestMethod]
		public void Test_GetAvailableStates()
		{
			var applicationData = _context.Create<ApplicationData>();
			var availableStates = _context.CreateMany<long>(6).ToArray();
			var withLogic = availableStates.Take(3).ToArray();
			var filtered = withLogic.Take(2).ToArray();
			var dictionary = _context.Create<Dictionary<long, string>>();
			var currentState = _context.Create<StateData>();

			_context.ApplicationRepository.Setup(x => x.Get(applicationData.Id)).Returns(applicationData);
			_context.StateService.Setup(x => x.GetAvailableStatesToSet()).Returns(availableStates);
			_context.IdentityService.Setup(x => x.IsInRole(RoleType.Admin)).Returns(false);
			_context.StateRepository.Setup(x => x.Get(applicationData.StateId)).Returns(currentState);
			_context.StateService.Setup(x => x.ApplyBusinessLogicToStates(applicationData, availableStates))
				.Returns(withLogic);
			_context.StateService.Setup(x => x.GetLocalizedDictionary(It.IsAny<IEnumerable<long>>())).Returns(dictionary);
			_context.StateService.Setup(x => x.FilterByPosition(withLogic, currentState.Position)).Returns(filtered);

			var stateModels = _service.GetAvailableStates(applicationData.Id);
			foreach (var model in stateModels)
			{
				dictionary[model.StateId].ShouldBeEquivalentTo(model.StateName);
			}

			_context.IdentityService.Verify(x => x.IsInRole(RoleType.Admin), Times.Once());
			_context.StateService.Verify(x => x.GetLocalizedDictionary(filtered), Times.Once());
			_context.StateService.Verify(x => x.ApplyBusinessLogicToStates(applicationData, availableStates), Times.Once());
			_context.StateService.Verify(x => x.FilterByPosition(withLogic, currentState.Position), Times.Once());
		}
	}
}
// ReSharper restore ImplicitlyCapturedClosure