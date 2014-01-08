using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Calculation;
using Alicargo.Core.Helpers;
using Alicargo.Jobs.Helpers.Abstract;
using Alicargo.Utilities;

namespace Alicargo.Jobs.Helpers
{
	internal sealed class ClientExcelHelper : IClientExcelHelper
	{
		private readonly IClientBalanceRepository _balance;
		private readonly IClientRepository _clients;
		private readonly IExcelClientCalculation _excel;
		private readonly IClientCalculationPresenter _presenter;

		public ClientExcelHelper(
			IClientRepository clients,
			IClientBalanceRepository balance,
			IClientCalculationPresenter presenter,
			IExcelClientCalculation excel)
		{
			_clients = clients;
			_balance = balance;
			_presenter = presenter;
			_excel = excel;
		}

		public IReadOnlyDictionary<string, FileHolder> GetExcels(long clientId, string[] languages)
		{
			var clientData = _clients.Get(clientId);
			var list = _presenter.List(clientId, int.MaxValue, 0);
			var balance = _balance.GetBalance(clientId);
			var name = GetName(clientData);

			var files = languages
				.Distinct()
				.ToDictionary(
					x => x,
					language =>
					{
						using(var stream = _excel.Get(list.Groups, balance, language))
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

		internal static string GetName(ClientData clientData)
		{
			var date = LocalizationHelper.GetDate(DateTimeProvider.Now, CultureInfo.GetCultureInfo(clientData.Language));
			var name = "calculation_" + date + "_" + clientData.Nic + ".xlsx";
			name = Regex.Replace(
				name, "[" + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "\\s]", "_");

			return name;
		}
	}
}