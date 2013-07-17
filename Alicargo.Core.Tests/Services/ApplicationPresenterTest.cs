using System.Collections.Generic;
using Alicargo.Core.Contracts;
using Alicargo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace Alicargo.Tests.Services
{
	[TestClass]
	public class ApplicationPresenterTest
	{
		private TestContext _context;
		private ApplicationPresenter _service;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new TestContext();

			_service = _context.Create<ApplicationPresenter>();
		}

		[TestMethod, Ignore]
		public void Test_GetAvailableStates_CargoIsCustomsClearedStateId()
		{
			var cargoIsCustomsClearedStateId = _context.Create<long>();
			var applicationData = _context.Build<ApplicationData>()
				.With(x => x.StateId, cargoIsCustomsClearedStateId)
				.Create();
			var cargoReceivedStateId = _context.Create<long>();

			var cargoOnTransitStateId = _context.Create<long>();
			var dictionary = _context.Create<Dictionary<long, string>>();

			_context.ApplicationRepository.Setup(x => x.Get(applicationData.Id)).Returns(applicationData);
			_context.StateConfig.Setup(x => x.CargoIsCustomsClearedStateId).Returns(cargoIsCustomsClearedStateId);
			_context.StateConfig.Setup(x => x.CargoOnTransitStateId).Returns(cargoOnTransitStateId);
			_context.StateConfig.Setup(x => x.CargoReceivedStateId).Returns(cargoReceivedStateId);
			_context.StateService.Setup(x => x.GetLocalizedDictionary(new[] { cargoOnTransitStateId, cargoReceivedStateId, cargoIsCustomsClearedStateId }))
				.Returns(dictionary);

			var stateModels = _service.GetAvailableStates(applicationData.Id);

			foreach (var model in stateModels)
			{
				Assert.IsTrue(dictionary.ContainsKey(model.StateId));
			}

			_context.ApplicationRepository.Verify(x => x.Get(applicationData.Id), Times.Once());
			_context.StateConfig.Verify(x => x.CargoIsCustomsClearedStateId);
			_context.StateConfig.Verify(x => x.CargoOnTransitStateId, Times.Once());
			_context.StateConfig.Verify(x => x.CargoReceivedStateId, Times.Once());
			_context.StateService.Verify(x => x.GetLocalizedDictionary(new[] { cargoOnTransitStateId, cargoReceivedStateId, cargoIsCustomsClearedStateId }),
				Times.Once());
		}
	}
}
