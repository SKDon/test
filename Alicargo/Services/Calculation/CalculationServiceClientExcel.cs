using System;
using System.Linq;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.Calculation
{
	[Obsolete]
	internal sealed class CalculationServiceClientExcel : ICalculationService
	{
		private readonly ICalculationService _service;
		private readonly IClientRepository _clients;
		private readonly IApplicationRepository _applications;
		private readonly IExcelGenerator _excel;
		private readonly IStateService _stateService;
		private readonly IUnitOfWork _unitOfWork;

		public CalculationServiceClientExcel(
			ICalculationService service,
			IClientRepository clients,
			IApplicationRepository applications,
			IExcelGenerator excel,
			IStateService stateService,
			IUnitOfWork unitOfWork)
		{
			_service = service;
			_clients = clients;
			_applications = applications;
			_excel = excel;
			_stateService = stateService;
			_unitOfWork = unitOfWork;
		}

		public void Calculate(long applicationId)
		{
			_service.Calculate(applicationId);

			var states = _stateService.GetVisibleStates();

			var clientId = _applications.GetClientId(applicationId);

			var applications = _applications.List(stateIds: states, orders: new[]
			{
				new Order
				{
					OrderType = OrderType.Id,
					Desc = true
				}
			}, clientId: clientId).Select(x => new ClientCalculationExcelRow(x)).ToArray();

			using (var stream = _excel.Get(applications))
			{
				_clients.SetCalculationExcel(clientId, stream.ToArray());

				_unitOfWork.SaveChanges();
			}
		}
	}
}