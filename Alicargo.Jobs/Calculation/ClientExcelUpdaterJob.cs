using System.Linq;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services;

namespace Alicargo.Jobs.Calculation
{
	public sealed class ClientExcelUpdaterJob : IJob
	{
		private readonly ICalculationRepository _calculations;
		private readonly IClientFileRepository _files;
		private readonly IClientRepository _clients;
		private readonly IExcelGenerator _excel;
		private readonly IUnitOfWork _unitOfWork;

		public ClientExcelUpdaterJob(
			IExcelGenerator excel,
			ICalculationRepository calculations,
			IClientFileRepository files,
			IClientRepository clients,
			IUnitOfWork unitOfWork)
		{
			_excel = excel;
			_calculations = calculations;
			_files = files;
			_clients = clients;
			_unitOfWork = unitOfWork;
		}

		public void Run()
		{
			var data = _calculations.Get(CalculationState.New);

			// todo: add state fields to to the clients table to implement optimistic lock while excel file updating

			if (data.Length == 0)
			{
				return;
			}

			var clients = data.Select(x => x.Data.ClientId).Distinct().ToArray();

			foreach (var clientId in clients)
			{
				var language = _clients.GetLanguage(clientId);

				var calculations = _calculations.GetByClientId(clientId).Select(x => new CalculationExcelRow(x)).ToArray();

				using (var stream = _excel.Get(calculations, language))
				{
					_files.SetCalculationExcel(clientId, stream.ToArray());

					_unitOfWork.SaveChanges();
				}
			}

			foreach (var item in data)
			{
				_calculations.SetState(item, CalculationState.ClientUpdated);
			}
		}
	}
}