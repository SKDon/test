using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Calculation;
using Alicargo.Core.Helpers;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.Helpers
{
	public sealed class ClientExcelHelper : IClientExcelHelper
	{
		private readonly IClientRepository _clients;
		private readonly IClientCalculationPresenter _presenter;
		private readonly IExcelClientCalculation _excel;

		public ClientExcelHelper(
			IClientRepository clients,
			IClientCalculationPresenter presenter,
			IExcelClientCalculation excel)
		{
			_clients = clients;
			_presenter = presenter;
			_excel = excel;
		}

		public IReadOnlyDictionary<string, FileHolder> GetExcels(long clientId, string[] languages)
		{
			var clientData = _clients.Get(clientId);
			var list = _presenter.List(clientId, int.MaxValue, 0);
			var date = LocalizationHelper.GetDate(DateTimeOffset.UtcNow, CultureInfo.GetCultureInfo(clientData.Language));
			var name = "calculation_" + date + "_" + clientData.Nic + ".xlsx";

			var files = languages
				.Distinct()
				.ToDictionary(
					x => x,
					language =>
					{
						using(var stream = _excel.Get(list.Groups, language))
						{
							return new FileHolder
							{
								Data = stream.ToArray(),
								Name = name
							};
						}
					});

			return files;
		}
	}
}